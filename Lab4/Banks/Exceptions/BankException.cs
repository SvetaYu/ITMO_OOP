namespace Banks.Exceptions;

public class BankException : Exception
{
    private BankException(string message)
        : base(message) { }

    public static BankException InvalidName()
    {
        return new BankException("invalid name");
    }
}