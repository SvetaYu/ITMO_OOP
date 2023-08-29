using System;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuServiceExtraTests
{
    private IsuServiceExtra _isu = new IsuServiceExtra();
    private GroupExtra _m32081;
    private StudentExtra _sveta;
    private OgnpCourse _ognp1;
    private OgnpCourse _ognp2;
    private OgnpGroup cyb1;
    private OgnpGroup cyb2;
    private OgnpGroup cyb3;
    private OgnpGroup software1;
    private OgnpGroup mark1;

    public IsuServiceExtraTests()
    {
        _m32081 = _isu.GetGroupExtra(_isu.AddGroup(new GroupName("M32081", new GroupNameValidatorItmo())));
        _sveta = _isu.GetStudentExtra(_isu.AddStudent(_m32081.Group, "Sveta"));
        _isu.AddLesson("OOP", new TimeOnly(17, 00), DayOfWeek.Saturday, _m32081, new Teacher("Makarevich R.D."), 147);
        _ognp1 = _isu.AddOgnpCourse('K');
        var flow1 = _isu.AddOgnpFlow("cyber security", _ognp1);
        var flow2 = _isu.AddOgnpFlow("software development", _ognp1);
        cyb1 = _isu.AddOgnpGroup(flow1);
        cyb2 = _isu.AddOgnpGroup(flow1);
        cyb3 = _isu.AddOgnpGroup(flow1);
        software1 = _isu.AddOgnpGroup(flow2);
        _isu.AddLesson("cyber security", new TimeOnly(13, 30), DayOfWeek.Thursday, cyb1, new Teacher("Ivanov I.I."), 303);
        _isu.AddLesson("cyber security", new TimeOnly(17, 00), DayOfWeek.Saturday, cyb2, new Teacher("Petrov P.P."), 305);
        _ognp2 = _isu.AddOgnpCourse('P');
        flow1 = _isu.AddOgnpFlow("Marketing", _ognp2);
        mark1 = _isu.AddOgnpGroup(flow1);
        _isu.AddLesson("cyber security", new TimeOnly(17, 00), DayOfWeek.Friday, cyb2, new Teacher("Petrov P.P."), 305);
    }

    [Fact]
    public void EnrollStudentInOgnpCourse()
    {
        _isu.EnrollStudentInOgnpCourse(_sveta, _ognp1, cyb1);
        Assert.Contains(_sveta, cyb1.Students);
        Assert.Equal(_sveta.OgnpCourse, _ognp1);
        Assert.Contains(cyb1, _sveta.OgnpGroups);
    }

    [Fact]
    public void UnsubscribeFromOgnpCourse()
    {
        _isu.UnsubscribeFromOgnpCourse(_sveta);
        Assert.DoesNotContain(_sveta, cyb1.Students);
        Assert.Null(_sveta.OgnpCourse);
        Assert.DoesNotContain(cyb1, _sveta.OgnpGroups);
    }

    [Fact]
    public void EnrollStudentInOgnpCourseWithTimeTablesIntersection()
    {
        Assert.Throws<IsuServiceExtraException>(() => _isu.EnrollStudentInOgnpCourse(_sveta, _ognp1, cyb2));
    }

    [Fact]
    public void EnrollInOgnpGroup()
    {
        _isu.EnrollStudentInOgnpCourse(_sveta, _ognp1, cyb1);
        _isu.EnrollInOgnpGroup(_sveta, software1);
        Assert.Contains(software1, _sveta.OgnpGroups);
        Assert.Contains(_sveta, software1.Students);
    }

    [Fact]
    public void EnrollInOgnpGroupWithAnotherFaculty()
    {
        _isu.EnrollStudentInOgnpCourse(_sveta, _ognp1, cyb1);
        Assert.Throws<StudentExtraException>(() => _isu.EnrollInOgnpGroup(_sveta, mark1));
    }

    [Fact]
    public void GetStudentsTimeTable()
    {
        _isu.EnrollStudentInOgnpCourse(_sveta, _ognp1, cyb1);
        _isu.EnrollInOgnpGroup(_sveta, software1);
        var timetable = _isu.GetTimeTable(_sveta);
        foreach (var lesson in _sveta.Group.Timetable.Lessons)
        {
            Assert.Contains(lesson, timetable.Lessons);
        }

        foreach (var group in _sveta.OgnpGroups)
        {
            foreach (var lesson in group.Timetable.Lessons)
            {
                Assert.Contains(lesson, timetable.Lessons);
            }
        }
    }

    [Fact]
    public void EnrollInOgnpGroupsFromTheSameFlow()
    {
        _isu.EnrollStudentInOgnpCourse(_sveta, _ognp1, cyb1);
        Assert.Throws<StudentExtraException>(() => _isu.EnrollInOgnpGroup(_sveta, cyb3));
    }
}