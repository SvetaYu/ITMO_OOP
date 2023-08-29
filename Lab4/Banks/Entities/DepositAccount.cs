using Banks.Exceptions;

namespace Banks.Entities;

public class DepositAccount : IAccount
{
    private decimal _accruals = 0;

    public DepositAccount(int periodInMonth, Bank bank, Client client, decimal amount, decimal interest, DateOnly today)
    {
        Interest = interest;
        Bank = bank;
        Client = client;
        EndDate = today.AddMonths(periodInMonth);
        Amount = amount;
        Id = Guid.NewGuid();
        Finished = false;
        MaxAmountAvailableToUnconfirmedClients = bank.Config.MaxAmountAvailableToUnconfirmedClients;
    }

    public Client Client { get; }
    public Bank Bank { get; }
    public decimal Amount { get; private set; }
    public Guid Id { get; }
    public decimal MaxAmountAvailableToUnconfirmedClients { get; }
    public decimal Interest { get; }
    public DateOnly EndDate { get; }
    public bool Finished { get; private set; }

    public void TopUp(decimal value)
    {
        if (!Finished)
        {
         throw AccountException.InvalidOperation();
        }

        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        Amount += value;
    }

    public void Withdraw(decimal value)
    {
        if (!Finished)
        {
            throw AccountException.InvalidOperation();
        }

        if (!Client.IsVerified && value > MaxAmountAvailableToUnconfirmedClients)
        {
            throw AccountException.UnverifiedClient();
        }

        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        if (Amount - value < 0)
        {
            throw AccountException.NotEnoughMoney();
        }

        Amount -= value;
    }

    public void AccrueInterest(DateOnly newDate)
    {
        if (Finished) return;
        _accruals += Amount * (Interest / 36500);
        if (newDate == EndDate)
        {
            Amount += _accruals;
            Finished = true;
        }
    }

    public decimal CommissionDeduction()
    {
        return 0;
    }
}