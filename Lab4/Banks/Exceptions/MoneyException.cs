namespace Banks.Exceptions;

public class MoneyException : Exception
{
    private MoneyException(string message)
        : base(message) { }

    public static MoneyException LessThanZero()
    {
        return new MoneyException("The amount of money is less than zero");
    }
}