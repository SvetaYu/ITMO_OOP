using Banks.Exceptions;

namespace Banks.Models;

public class DepositAccountInterest
{
    public DepositAccountInterest(decimal minAmount, decimal interest)
    {
        if (minAmount < 0)
        {
            throw MoneyException.LessThanZero();
        }

        if (interest is < 0 or > 100)
        {
            throw DepositAccountInterestException.InvalidInterest();
        }

        MinAmount = minAmount;
        Interest = interest;
    }

    public decimal MinAmount { get; }
    public decimal Interest { get; }
}