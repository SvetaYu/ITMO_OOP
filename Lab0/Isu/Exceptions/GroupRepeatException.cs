namespace Isu.Exceptions;

public class GroupRepeatException : InvalidOperationException
{
    public GroupRepeatException() { }
    public GroupRepeatException(string message)
        : base(message) { }
    public GroupRepeatException(string message, Exception inner)
        : base(message, inner) { }
}