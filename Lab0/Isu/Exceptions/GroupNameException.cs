namespace Isu.Exceptions;

public class GroupNameException : ArgumentException
{
    public GroupNameException() { }
    public GroupNameException(string message, string paramName)
        : base(message, paramName) { }
    public GroupNameException(string message, string paramName, Exception inner)
        : base(message, paramName, inner) { }
    public GroupNameException(string message)
        : base(message) { }
    public GroupNameException(string message, Exception inner)
        : base(message, inner) { }
}