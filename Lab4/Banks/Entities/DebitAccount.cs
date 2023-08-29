using Banks.Exceptions;

namespace Banks.Entities;

public class DebitAccount : IAccount
{
    private decimal _accruals = 0;

    public DebitAccount(Bank bank, Client client, decimal amount, DateOnly today)
    {
        Interest = bank.Config.DebitAccountInterest;
        Bank = bank;
        Client = client;
        Amount = amount;
        DateOfInterestAccrual = today.Day;
        Id = Guid.NewGuid();
        MaxAmountAvailableToUnconfirmedClients = bank.Config.MaxAmountAvailableToUnconfirmedClients;
    }

    public Client Client { get; }
    public Bank Bank { get; }
    public decimal Amount { get; private set; }
    public Guid Id { get; }
    public decimal MaxAmountAvailableToUnconfirmedClients { get; }
    public decimal Interest { get; }
    public int DateOfInterestAccrual { get; }

    public void TopUp(decimal value)
    {
        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        Amount += value;
    }

    public void Withdraw(decimal value)
    {
        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        if (!Client.IsVerified && value > MaxAmountAvailableToUnconfirmedClients)
        {
            throw AccountException.UnverifiedClient();
        }

        if (Amount - value < 0)
        {
            throw AccountException.NotEnoughMoney();
        }

        Amount -= value;
    }

    public void AccrueInterest(DateOnly newDate)
    {
        _accruals += Amount * (Interest / 36500);
        if (newDate.Day == DateOfInterestAccrual)
        {
            Amount += _accruals;
            _accruals = 0;
        }
    }

    public decimal CommissionDeduction()
    {
        return 0;
    }
}