namespace Banks.Exceptions;

public class CommandException : Exception
{
    private CommandException(string message)
        : base(message) { }

    public static CommandException InvalidOperation()
    {
        return new CommandException("Unavailable operation");
    }

    public static CommandException OperationFailed()
    {
        return new CommandException("operation failed");
    }
}