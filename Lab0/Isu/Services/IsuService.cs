using System.Collections.ObjectModel;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    public const int GroupsCapacity = 30;
    private List<Group> _groups = new List<Group>();
    private int _countOfStudents;

    public Group AddGroup(GroupName name)
    {
        if (name is null)
        {
            throw new GroupNameNullException();
        }

        if (FindGroup(name) is not null)
        {
            throw new GroupRepeatException("this group already exists");
        }

        _groups.Add(new Group(name));
        return _groups.Last();
    }

    public Student AddStudent(Group group, string name)
    {
        if (group is null)
        {
            throw new GroupNullException();
        }

        if (name is null)
        {
            throw new StudentNameNullException();
        }

        if (group.Students.Count >= GroupsCapacity)
        {
            throw new GroupOverflowException("there are already enough students in this group");
        }

        ++_countOfStudents;
        return new Student(name, _countOfStudents, group);

        // group.AddStudent(new Student(name, _countOfStudents, group));
        // return group.Students.Last();
    }

    public void RemoveStudent(Student student)
    {
        if (student is null)
        {
            throw new StudentNullException();
        }

        FindGroup(student.Group)?.RemoveStudent(student);
    }

    public void RemoveGroup(Group group)
    {
        if (group is null)
        {
            throw new GroupNullException();
        }

        _groups.Remove(group);
    }

    public Student FindStudent(int id)
    {
        return _groups.SelectMany(group => group.Students).FirstOrDefault(student => student.Id == id);
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new ObjectNotFoundException("student with this id not found");
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        if (groupName is null)
        {
            return new List<Student>();
        }

        return FindGroup(groupName)?.Students ?? new List<Student>();
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        if (courseNumber is null)
        {
            return new List<Student>();
        }

        return FindGroups(courseNumber)?.SelectMany(group => group.Students).ToList() ?? new List<Student>();
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber is null)
        {
            return new List<Group>();
        }

        return new ReadOnlyCollection<Group>(_groups.Where(group => group.Course == courseNumber).ToList());
    }

    public Group FindGroup(GroupName groupName)
    {
        if (groupName is null)
        {
            return null;
        }

        return _groups.Find(group => group.GroupName == groupName);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student is null)
        {
            throw new StudentNullException();
        }

        if (newGroup is null)
        {
            throw new GroupNullException();
        }

        if (newGroup.Students.Count >= GroupsCapacity)
        {
            throw new GroupOverflowException("there are already enough students in this group");
        }

        // new Student(student.Name, student.Id, newGroup);
        newGroup.AddStudent(student);
        RemoveStudent(student);
        student.ChangeGroupName(newGroup.GroupName);

        // student.Group = newGroup.GroupName;
    }
}