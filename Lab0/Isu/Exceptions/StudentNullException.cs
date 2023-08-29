namespace Isu.Exceptions;

public class StudentNullException : ArgumentNullException
{
    public StudentNullException() { }

    public StudentNullException(string paramName)
        : base(paramName) { }

    public StudentNullException(string message, Exception inner)
        : base(message, inner) { }

    public StudentNullException(string paramName, string message)
        : base(message, paramName) { }
}