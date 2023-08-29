namespace Isu.Exceptions;

public class GroupNameNullException : ArgumentNullException
{
    public GroupNameNullException() { }

    public GroupNameNullException(string paramName)
        : base(paramName) { }

    public GroupNameNullException(string message, Exception inner)
        : base(message, inner) { }

    public GroupNameNullException(string paramName, string message)
        : base(message, paramName) { }
}