namespace Isu.Exceptions;

public class GroupOverflowException : InvalidOperationException
{
    public GroupOverflowException() { }
    public GroupOverflowException(string message)
        : base(message) { }
    public GroupOverflowException(string message, Exception inner)
        : base(message, inner) { }
}