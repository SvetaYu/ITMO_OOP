namespace Isu.Exceptions;

public class GroupNameValidatorNullException : ArgumentNullException
{
    public GroupNameValidatorNullException() { }

    public GroupNameValidatorNullException(string paramName)
        : base(paramName) { }

    public GroupNameValidatorNullException(string message, Exception inner)
        : base(message, inner) { }

    public GroupNameValidatorNullException(string paramName, string message)
        : base(message, paramName) { }
}