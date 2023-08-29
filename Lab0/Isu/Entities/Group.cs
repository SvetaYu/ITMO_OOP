using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private readonly List<Student> _students = new ();

    internal Group(GroupName groupName)
    {
        GroupName = groupName ?? throw new GroupNameNullException();
        Course = new CourseNumber(int.Parse(Convert.ToString(groupName.Name[2])));
    }

    public GroupName GroupName { get; }
    public IReadOnlyCollection<Student> Students => _students;
    public CourseNumber Course { get; }

    public static bool operator ==(Group lhs, Group rhs)
    {
        return Equals(lhs, rhs);
    }

    public static bool operator !=(Group lhs, Group rhs)
    {
        return !Equals(lhs, rhs);
    }

    public override string ToString()
    {
        string answer = GroupName.Name + ':';
        return Students.Aggregate(answer, (current, student) => current + ("\n" + student.ToString()));
    }

    public override bool Equals(object obj)
    {
        if (obj is Group group)
        {
            return Equals(group);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }

    public bool Equals(Group other)
    {
        return other != null && this.GroupName == other.GroupName;
    }

    internal Student AddStudent(Student student)
    {
        if (student is null)
        {
            throw new StudentNullException();
        }

        _students.Add(student);
        return student;
    }

    internal void RemoveStudent(Student student)
    {
        if (student is null)
        {
            throw new StudentNullException();
        }

        _students.Remove(student);
    }
}