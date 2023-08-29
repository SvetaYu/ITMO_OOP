using Shops.Exceptions;

namespace Shops.Models;

public class Account
{
    public Account(decimal money)
    {
        if (money < 0)
        {
            throw AccountException.InvalidAccountValue();
        }

        Money = money;
    }

    public decimal Money { get; private set; }

    internal void IncreaseMoney(decimal value)
    {
        if (Money < value)
        {
            throw AccountException.InvalidOperationWithMoney();
        }

        Money -= value;
    }

    internal void ReduceMoney(decimal value)
    {
        if (Money < value)
        {
            throw AccountException.InvalidOperationWithMoney();
        }

        Money -= value;
    }
}