using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public interface IIsuServiceExtra
{
    void EnrollStudentInOgnpCourse(StudentExtra student, OgnpCourse course, OgnpGroup group);
    void EnrollInOgnpGroup(StudentExtra student, OgnpGroup group);
    IReadOnlyCollection<StudentExtra> FindStudentsWithoutOgnp();
    void RemoveLesson(Lesson lesson);
    void UnsubscribeFromOgnpCourse(StudentExtra student);
    void UnsubscribeFromOgnpGroup(StudentExtra student, OgnpGroup group);
    OgnpCourse AddOgnpCourse(char faculty);
    Flow AddOgnpFlow(string name, OgnpCourse course);
    OgnpGroup AddOgnpGroup(Flow flow);
    Timetable GetTimeTable(StudentExtra student);
    void AddLesson(string name, TimeOnly start, DayOfWeek dayOfWeek, IHaveTimeTable group, Teacher teacher, int classNumber, int duration = 90);
}