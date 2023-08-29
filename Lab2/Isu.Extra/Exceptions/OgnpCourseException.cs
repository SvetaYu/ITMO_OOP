namespace Isu.Extra.Exceptions;

public class OgnpCourseException : Exception
{
    private OgnpCourseException(string massage)
        : base(massage) { }

    public static OgnpCourseException CourseOverFlow()
    {
        return new OgnpCourseException("Course overflow");
    }
}