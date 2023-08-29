using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Timetable
{
    private readonly List<Lesson> _lessons = new List<Lesson>();
    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    internal Lesson AddLesson(Lesson lesson)
    {
        if (IntersectionCheck(lesson))
        {
            throw TimetableException.TimetableIntersection();
        }

        _lessons.Add(lesson);
        return _lessons.Last();
    }

    internal Timetable MergeWith(Timetable timetable)
    {
        _lessons.AddRange(timetable._lessons);
        return this;
    }

    internal bool IntersectionCheck(Lesson lesson)
    {
        return _lessons.Any(lesson.IntersectsWith);
    }

    internal bool IntersectionCheck(Timetable timetable)
    {
        return _lessons.Any(timetable.IntersectionCheck);
    }

    internal void RemoveLesson(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        _lessons.Remove(lesson);
    }
}