using Shops.Models;

namespace Shops.Exceptions;

public class BuyerException : Exception
{
    private BuyerException(string message)
        : base(message) { }

    public static BuyerException InvalidBuyerException()
    {
        return new BuyerException("invalid Buyers name");
    }

    public static BuyerException InvalidTransferAmount()
    {
        return new BuyerException("transfer amount must not be less than zero");
    }
}