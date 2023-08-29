using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class StudentExtra : IEquatable<StudentExtra>, IEquatable<Student>
{
    private List<OgnpGroup> _ognpGroups = new List<OgnpGroup>();

    internal StudentExtra(Student student, GroupExtra group)
    {
        Student = student ?? throw new ArgumentNullException(nameof(student));
        Group = group ?? throw new ArgumentNullException(nameof(group));
        OgnpCourse = null;
    }

    public Student Student { get; }
    public GroupExtra Group { get; private set; }
    public IReadOnlyCollection<OgnpGroup> OgnpGroups => _ognpGroups;
    public OgnpCourse OgnpCourse { get; private set; }

    public char GetFaculty()
    {
        return Student.Group.Name[0];
    }

    public override bool Equals(object obj)
    {
        if (obj is Student student)
        {
            return Equals(student);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Student.Id.GetHashCode();
    }

    public bool Equals(StudentExtra other)
    {
        return other is not null && this.Student.Id.Equals(other.Student.Id);
    }

    public bool Equals(Student other)
    {
        return other is not null && this.Student.Equals(other);
    }

    internal Timetable GetTimeTable()
    {
        var timeTable = new Timetable();
        timeTable.MergeWith(Group.Timetable);
        foreach (OgnpGroup group in _ognpGroups)
        {
            timeTable.MergeWith(group.Timetable);
        }

        return timeTable;
    }

    internal void ChangeGroup(GroupExtra newGroup)
    {
        Group = newGroup ?? throw new ArgumentNullException(nameof(newGroup));
    }

    internal void EnrollInOgnpCourse(OgnpCourse course, OgnpGroup group)
    {
        ArgumentNullException.ThrowIfNull(course);
        ArgumentNullException.ThrowIfNull(group);
        if (OgnpCourse is not null)
            throw StudentExtraException.CourseAlreadyExists();
        OgnpCourse = course;
        AddOgnpGroup(group);
    }

    internal void UnsubscribeFromOgnpCourse()
    {
        foreach (OgnpGroup group in _ognpGroups)
        {
            group.RemoveStudent(this);
        }

        _ognpGroups.Clear();
        OgnpCourse = null;
    }

    internal void AddOgnpGroup(OgnpGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);
        if (OgnpCourse is null)
        {
            throw StudentExtraException.NoCourseEnrollment();
        }

        if (group.Course != OgnpCourse)
        {
            throw StudentExtraException.GroupFromAnotherCourse();
        }

        if (OgnpGroups.Any(curGroup => curGroup.Flow.Equals(group.Flow)))
        {
            throw StudentExtraException.FlowRepeat();
        }

        _ognpGroups.Add(group);
    }

    internal void RemoveOgnpGroup(OgnpGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);
        _ognpGroups.Remove(group);
    }
}