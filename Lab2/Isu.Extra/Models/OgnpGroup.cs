using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Models;

namespace Isu.Extra.Models;

public class OgnpGroup : IHaveTimeTable, IEquatable<OgnpGroup>
{
    public const int GroupsCapacity = 20;
    private List<StudentExtra> _students = new List<StudentExtra>();

    internal OgnpGroup(GroupName name, OgnpCourse course, Flow flow)
    {
        Timetable = new Timetable();
        Course = course ?? throw new ArgumentNullException(nameof(course));
        Flow = flow ?? throw new ArgumentNullException(nameof(flow));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CountOfStudents = 0;
    }

    public int CountOfStudents { get; }
    public Timetable Timetable { get; }
    public GroupName Name { get; }
    public OgnpCourse Course { get; }
    public Flow Flow { get; }
    public IReadOnlyCollection<StudentExtra> Students => _students;

    public override bool Equals(object obj)
    {
        if (obj is OgnpGroup group)
        {
            return Equals(group);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public bool Equals(OgnpGroup other)
    {
        return other is not null && other.Name.Equals(Name);
    }

    internal void AddStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (CountOfStudents == GroupsCapacity)
        {
            throw OgnpGroupException.OgnpGroupOverFlow();
        }

        _students.Add(student);
    }

    internal void RemoveStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _students.Remove(student);
    }
}