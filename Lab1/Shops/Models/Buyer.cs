using Shops.Exceptions;

namespace Shops.Models;

public class Buyer
{
    private Account _account;

    public Buyer(string name, decimal money)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _account = new Account(money);
    }

    public string Name { get; }
    public decimal Money => _account.Money;

    internal void TransferMoney(decimal value)
    {
        if (value < 0)
        {
            throw BuyerException.InvalidTransferAmount();
        }

        _account.ReduceMoney(value);
    }
}