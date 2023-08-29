namespace Isu.Extra.Exceptions;

public class IsuServiceExtraException : Exception
{
    private IsuServiceExtraException(string massage)
        : base(massage)
    {
    }

    public static IsuServiceExtraException AlreadyEnrolled()
    {
        return new IsuServiceExtraException("The student is already enrolled in the ognp-course");
    }

    public static IsuServiceExtraException SameFaculty()
    {
        return new IsuServiceExtraException("You can't enroll on your faculty's ognp-course");
    }

    public static IsuServiceExtraException IntersectTimeTables()
    {
        return new IsuServiceExtraException("The course timetable intersect with the main timetable");
    }

    public static IsuServiceExtraException CourseAlreadyExists(char faculty)
    {
        return new IsuServiceExtraException($"Course with faculty {faculty} already exists");
    }

    public static IsuServiceExtraException CourseDoesNotBelongToFlow()
    {
        return new IsuServiceExtraException(nameof(CourseDoesNotBelongToFlow));
    }
}