namespace Isu.Exceptions;

public class GroupNullException : ArgumentNullException
{
    public GroupNullException() { }

    public GroupNullException(string paramName)
        : base(paramName) { }

    public GroupNullException(string message, Exception inner)
        : base(message, inner) { }

    public GroupNullException(string paramName, string message)
        : base(message, paramName) { }
}