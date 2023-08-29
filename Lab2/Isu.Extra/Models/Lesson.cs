namespace Isu.Extra.Models;

public class Lesson
{
    internal Lesson(string name, TimeOnly start, DayOfWeek dayOfWeek, IHaveTimeTable group, Teacher teacher, int classNumber, int duration = 90)
    {
        Start = start;
        End = start.AddMinutes(duration);
        ClassNumber = classNumber;
        DayOfWeek = dayOfWeek;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        Group = group ?? throw new ArgumentNullException(nameof(group));
    }

    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    public IHaveTimeTable Group { get; }
    public Teacher Teacher { get; }
    public string Name { get; }
    public int ClassNumber { get; }
    public DayOfWeek DayOfWeek { get; }

    public bool IntersectsWith(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        return IntersectsTimeWith(lesson) && lesson.DayOfWeek.Equals(DayOfWeek);
    }

    private bool IntersectsTimeWith(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        return !(lesson.Start > End || lesson.End < Start);
    }
}