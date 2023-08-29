namespace Isu.Extra.Exceptions;

public class TimetableException : Exception
{
    private TimetableException(string massage)
        : base(massage) { }

    public static TimetableException TimetableIntersection()
    {
        return new TimetableException("Timetable Intersection");
    }
}