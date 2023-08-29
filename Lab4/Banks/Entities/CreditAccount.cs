using Banks.Exceptions;

namespace Banks.Entities;

public class CreditAccount : IAccount
{
    internal CreditAccount(Bank bank, Client client, decimal limit)
    {
        Client = client;
        Bank = bank;
        Commission = bank.Config.CreditAccountCommission;
        Amount = 0;
        Limit = limit;
        MaxAmountAvailableToUnconfirmedClients = bank.Config.MaxAmountAvailableToUnconfirmedClients;
        Id = Guid.NewGuid();
    }

    public Client Client { get; }
    public Bank Bank { get; }
    public decimal Amount { get; private set; }
    public Guid Id { get; }
    public decimal MaxAmountAvailableToUnconfirmedClients { get; }
    public decimal Commission { get; }
    public decimal Limit { get; }

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

        if (Amount - value < -Limit)
        {
            throw AccountException.NotEnoughMoney();
        }

        Amount -= value;
    }

    public void AccrueInterest(DateOnly newDate) { }

    public decimal CommissionDeduction()
    {
        if (Amount >= 0) return 0;
        if (Amount - Commission < -Limit)
        {
            throw AccountException.NotEnoughMoney();
        }

        Amount -= Commission;
        return Commission;
    }
}