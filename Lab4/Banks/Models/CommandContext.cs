using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class CommandContext
{
    public CommandContext(IAccount from, IAccount to, decimal value)
    {
        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        From = from;
        To = to;
        Value = value;
    }

    public IAccount From { get; }
    public IAccount To { get; }
    public decimal Value { get; }
}