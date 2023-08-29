namespace Isu.Exceptions;

public class StudentNameNullException : ArgumentNullException
{
    public StudentNameNullException() { }

    public StudentNameNullException(string paramName)
        : base(paramName) { }

    public StudentNameNullException(string message, Exception inner)
        : base(message, inner) { }

    public StudentNameNullException(string paramName, string message)
        : base(message, paramName) { }
}