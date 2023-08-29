namespace Isu.Extra.Exceptions;

public class StudentExtraException : Exception
{
    private StudentExtraException(string massage)
        : base(massage) { }

    public static StudentExtraException NoCourseEnrollment()
    {
        return new StudentExtraException("Student not enrolled in ognp-course");
    }

    public static StudentExtraException GroupFromAnotherCourse()
    {
        return new StudentExtraException("This group from another ognp-course");
    }

    public static StudentExtraException FlowRepeat()
    {
        return new StudentExtraException("this student is already enrolled in this flow");
    }

    public static StudentExtraException CourseAlreadyExists()
    {
        return new StudentExtraException("Course Already Exists");
    }
}