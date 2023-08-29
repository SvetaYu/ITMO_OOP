namespace Isu.Exceptions;

public class CourseNumberNullException : ArgumentNullException
{
    public CourseNumberNullException() { }

    public CourseNumberNullException(string paramName)
        : base(paramName) { }

    public CourseNumberNullException(string message, Exception inner)
        : base(message, inner) { }

    public CourseNumberNullException(string paramName, string message)
        : base(message, paramName) { }
}