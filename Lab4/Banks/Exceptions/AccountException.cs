namespace Banks.Exceptions;

public class AccountException : Exception
{
    private AccountException(string message)
        : base(message) { }

    public static AccountException NotEnoughMoney()
    {
        return new AccountException("Not enough money");
    }

    public static AccountException InvalidOperation()
    {
        return new AccountException("Unavailable operation");
    }

    public static AccountException UnverifiedClient()
    {
        return new AccountException("the transaction cannot be executed because the client is not verified");
    }
}