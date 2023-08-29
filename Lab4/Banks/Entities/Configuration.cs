using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class Configuration
{
    private List<DepositAccountInterest> _depositAccountInterests;

    public Configuration(decimal debitAccountInterest, decimal creditAccountCommission, IEnumerable<DepositAccountInterest> depositAccountInterests, decimal maxAmountAvailableToUnconfirmedClients)
    {
        if (debitAccountInterest is < 0 or > 100)
        {
            throw CommandException.InvalidOperation();
        }

        if (maxAmountAvailableToUnconfirmedClients < 0 || creditAccountCommission < 0)
        {
            throw MoneyException.LessThanZero();
        }

        ArgumentNullException.ThrowIfNull(depositAccountInterests);
        DebitAccountInterest = debitAccountInterest;
        _depositAccountInterests = depositAccountInterests.OrderBy(interest => interest.MinAmount).ToList();
        CreditAccountCommission = creditAccountCommission;
        MaxAmountAvailableToUnconfirmedClients = maxAmountAvailableToUnconfirmedClients;
    }

    public event EventHandler Changed;
    public decimal DebitAccountInterest { get; private set; }
    public decimal CreditAccountCommission { get; private set; }
    public decimal MaxAmountAvailableToUnconfirmedClients { get; private set; }
    public IReadOnlyCollection<DepositAccountInterest> DepositAccountInterests => _depositAccountInterests;

    public void ChangeDebitAccountInterest(decimal interest)
    {
        if (interest is < 0 or > 100)
        {
            throw CommandException.InvalidOperation();
        }

        DebitAccountInterest = interest;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeDepositAccountInterest(IEnumerable<DepositAccountInterest> interests)
    {
        ArgumentNullException.ThrowIfNull(interests);
        _depositAccountInterests = interests.OrderBy(interest => interest.MinAmount).ToList();
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeCreditAccountInterest(decimal commission)
    {
        if (commission < 0)
        {
            throw MoneyException.LessThanZero();
        }

        CreditAccountCommission = commission;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeMaxAmountAvailableToUnconfirmedClients(decimal amount)
    {
        if (amount < 0)
        {
            throw MoneyException.LessThanZero();
        }

        MaxAmountAvailableToUnconfirmedClients = amount;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public decimal GetDepositIntersect(decimal amount)
    {
        return DepositAccountInterests.Last(interest => interest.MinAmount <= amount).Interest;
    }
}