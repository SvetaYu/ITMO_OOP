using Banks.Entities;
using Banks.Exceptions;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class BanksTests
{
    private CentralBank cb;
    private Client _user1;
    private Client _user2;
    private Guid debitAccountUser1Id;
    private Guid creditAccountUser2Id;
    private Guid depositAccountUser1Id;
    private TimeManager time;
    private Bank sber;
    private Configuration sberConfig;

    public BanksTests()
    {
        time = new TimeManager(new DateOnly(2022, 12, 1));
        cb = new CentralBank(time);
        _user1 = Client.Builder.SetName("Sveta").SetSurname("Yudina").SetPassport(123456).SetAddress("Beloryskaya 6").Build();
        _user2 = Client.Builder.SetName("Roma").SetSurname("Makarevich").SetAddress("vzskj 15").SetPassport(123123)
            .Build();
        var interests = new[] { new DepositAccountInterest(0, 1), new DepositAccountInterest(10000, 3), new DepositAccountInterest(50000, 4) };
        sberConfig = new Configuration(3, 50, interests, 1000);
        sber = cb.CreateBank("sber", sberConfig);
        debitAccountUser1Id = cb.OpenDebitAccount(sber, _user1, 50000);
        depositAccountUser1Id = cb.OpenDepositAccount(sber, _user1, 25000, 6);
        creditAccountUser2Id = cb.OpenCreditAccount(sber, _user2, 100000);
    }

    [Fact]
    public void TransferMoney()
    {
        cb.TransferMoney(creditAccountUser2Id, debitAccountUser1Id, 10000);
        Assert.Equal(-10050, cb.ShowBalance(creditAccountUser2Id));
        Assert.Equal(60000, cb.ShowBalance(debitAccountUser1Id));
    }

    [Fact]
    public void WithdrawalWithCommission()
    {
        var tr1 = cb.WithdrawalMoney(creditAccountUser2Id, 100);
        Assert.Equal(-150, cb.ShowBalance(creditAccountUser2Id));
    }

    [Fact]
    public void TransferMoneyAndCancellationOfTheOperation()
    {
        Guid transaction = cb.TransferMoney(creditAccountUser2Id, debitAccountUser1Id, 10000);

        cb.CancelTransaction(transaction);

        Assert.Equal(0, cb.ShowBalance(creditAccountUser2Id));
        Assert.Equal(50000, cb.ShowBalance(debitAccountUser1Id));
    }

    [Fact]
    public void AccrualOfInterest()
    {
        for (int i = 0; i < 20; ++i)
        {
          time.AddDay();
        }

        cb.TopUpAccount(debitAccountUser1Id, 10000);
        for (int i = 0; i < 11; ++i)
        {
            time.AddDay();
        }

        Assert.Equal(0, cb.ShowBalance(creditAccountUser2Id));
        Assert.Equal(50000 + ((sberConfig.DebitAccountInterest / 36500) * 50000 * 20) + 10000 + ((sberConfig.DebitAccountInterest / 36500) * 60000 * 11), cb.ShowBalance(debitAccountUser1Id));
    }
}