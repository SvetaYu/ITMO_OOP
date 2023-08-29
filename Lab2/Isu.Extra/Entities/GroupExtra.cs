using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class GroupExtra : IHaveTimeTable, IEquatable<GroupExtra>
{
    private readonly List<StudentExtra> _students = new List<StudentExtra>();

    internal GroupExtra(Group group)
    {
        Group = group ?? throw new ArgumentNullException(nameof(group));
        Timetable = new Timetable();
    }

    public Group Group { get; }
    public Timetable Timetable { get; }
    public IReadOnlyCollection<StudentExtra> Students => _students;

    public override bool Equals(object obj)
    {
        if (obj is GroupExtra group)
        {
            return Equals(group);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Group.GetHashCode();
    }

    public bool Equals(GroupExtra other)
    {
        return other is not null && Group == other.Group;
    }

    public bool Equals(Group other)
    {
        return other is not null && Group == other;
    }

    internal void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _students.Add(new StudentExtra(student, this));
    }

    internal void RemoveStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _students.Remove(student);
    }
}