using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    internal Student(string name, int id, Group group)
    {
        Name = name ?? throw new StudentNameNullException();
        Group = group?.GroupName ?? throw new GroupNullException();
        Id = id;
        Course = group.Course;
        group.AddStudent(this);
    }

    public string Name { get; }
    public int Id { get; }
    public GroupName Group { get; private set; }
    public CourseNumber Course { get; }

    public override string ToString()
    {
        return $"{Name} {Group.Name} id: {Convert.ToString(Id)}";
    }

    internal void ChangeGroupName(GroupName newGroup)
    {
        Group = newGroup;
    }
}