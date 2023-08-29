namespace Isu.Exceptions;

public class CourseNumberException : ArgumentException
{
    public CourseNumberException() { }
    public CourseNumberException(string message, string paramName)
        : base(message, paramName) { }
    public CourseNumberException(string message, string paramName, Exception inner)
        : base(message, paramName, inner) { }
    public CourseNumberException(string message)
        : base(message) { }
    public CourseNumberException(string message, Exception inner)
        : base(message, inner) { }
}