namespace Shops.Exceptions;

public class AccountException : Exception
{
    private AccountException(string message)
        : base(message)
    {
    }

    public static AccountException InvalidAccountValue()
    {
        return new AccountException("invalid account value");
    }

    public static AccountException InvalidOperationWithMoney()
    {
        return new AccountException("insufficient funds for the operation");
    }
}