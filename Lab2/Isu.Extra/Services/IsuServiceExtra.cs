using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuServiceExtra : IIsuService, IIsuServiceExtra
{
    private IIsuService _isu = new IsuService();
    private List<GroupExtra> _groups = new List<GroupExtra>();
    private List<OgnpCourse> _ognp = new List<OgnpCourse>();

    public void EnrollStudentInOgnpCourse(StudentExtra student, OgnpCourse course, OgnpGroup group)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(course);
        ArgumentNullException.ThrowIfNull(group);
        if (group.Course != course)
        {
            throw new Exception();
        }

        if (student.OgnpCourse is not null)
        {
            throw IsuServiceExtraException.AlreadyEnrolled();
        }

        if (course.Faculty == student.GetFaculty())
        {
            throw IsuServiceExtraException.SameFaculty();
        }

        if (IntersectTimeTables(student.Group.Timetable, group.Timetable))
        {
            throw IsuServiceExtraException.IntersectTimeTables();
        }

        group.AddStudent(student);
        student.EnrollInOgnpCourse(course, group);
    }

    public bool IntersectTimeTables(Timetable timeTable1, Timetable timeTable2)
    {
        ArgumentNullException.ThrowIfNull(timeTable2);
        ArgumentNullException.ThrowIfNull(timeTable1);
        return timeTable1.IntersectionCheck(timeTable2);
    }

    public void EnrollInOgnpGroup(StudentExtra student, OgnpGroup group)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(group);
        if (IntersectTimeTables(student.Group.Timetable, group.Timetable))
        {
            throw IsuServiceExtraException.IntersectTimeTables();
        }

        student.AddOgnpGroup(group);
        group.AddStudent(student);
    }

    public IReadOnlyCollection<StudentExtra> FindStudentsWithoutOgnp()
    {
        return _groups.SelectMany(group => group.Students).Where(student => student.OgnpCourse is null).ToList();
    }

    public void AddLesson(string name, TimeOnly start, DayOfWeek dayOfWeek, IHaveTimeTable group, Teacher teacher, int classNumber, int duration = 90)
    {
        var lesson = new Lesson(name, start, dayOfWeek, group, teacher, classNumber, duration);
        group.Timetable.AddLesson(lesson);
    }

    public void RemoveLesson(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        lesson.Group.Timetable.RemoveLesson(lesson);
    }

    public void UnsubscribeFromOgnpCourse(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        student.UnsubscribeFromOgnpCourse();
    }

    public void UnsubscribeFromOgnpGroup(StudentExtra student, OgnpGroup group)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(group);
        student.RemoveOgnpGroup(group);
    }

    public OgnpCourse AddOgnpCourse(char faculty)
    {
        if (FindOgnpCourse(faculty) is not null)
        {
            throw IsuServiceExtraException.CourseAlreadyExists(faculty);
        }

        _ognp.Add(new OgnpCourse(faculty));
        return _ognp.Last();
    }

    public OgnpCourse FindOgnpCourse(char faculty)
    {
        return _ognp.FirstOrDefault(course => course.Faculty == faculty);
    }

    public Flow AddOgnpFlow(string name, OgnpCourse course)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(course);
        return course.AddFlow(new Flow(name, course));
    }

    public OgnpGroup AddOgnpGroup(Flow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);
        return flow.AddGroup();
    }

    public Timetable GetTimeTable(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        return student.GetTimeTable();
    }

    public StudentExtra GetStudentExtra(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        return _groups.SelectMany(group => group.Students).First(extra => extra.Equals(student));
    }

    public GroupExtra GetGroupExtra(Group group)
    {
        ArgumentNullException.ThrowIfNull(group);
        return _groups.First(extra => extra.Equals(group));
    }

    public Group AddGroup(GroupName name)
    {
        Group group = _isu.AddGroup(name);
        _groups.Add(new GroupExtra(group));
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        Student student = _isu.AddStudent(group, name);
        GetGroupExtra(group).AddStudent(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        return _isu.GetStudent(id);
    }

    public Student FindStudent(int id)
    {
        return _isu.FindStudent(id);
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        return _isu.FindStudents(groupName);
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        return _isu.FindStudents(courseNumber);
    }

    public Group FindGroup(GroupName groupName)
    {
        return _isu.FindGroup(groupName);
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        return _isu.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        ChangeStudentGroupExtra(student, newGroup);
        _isu.ChangeStudentGroup(student, newGroup);
    }

    private void ChangeStudentGroupExtra(Student student, Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(newGroup);
        var studentExtra = GetStudentExtra(student);
        studentExtra.Group.RemoveStudent(studentExtra);
        var newGroupExtra = GetGroupExtra(newGroup);
        newGroupExtra.AddStudent(student);
        studentExtra.ChangeGroup(newGroupExtra);
    }
}