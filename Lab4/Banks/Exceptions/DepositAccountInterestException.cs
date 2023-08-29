namespace Banks.Exceptions;

public class DepositAccountInterestException : Exception
{
    private DepositAccountInterestException(string message)
        : base(message) { }
    public static DepositAccountInterestException InvalidInterest()
    {
        return new DepositAccountInterestException("invalid interest");
    }
}