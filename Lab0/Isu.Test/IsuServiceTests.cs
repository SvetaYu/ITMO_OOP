using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    private IsuService isu = new IsuService();
    private Group m3108;
    private Group m32081;

    public IsuServiceTests()
    {
        m3108 = isu.AddGroup(new GroupName("M3108", new GroupNameValidatorItmo()));
        m32081 = isu.AddGroup(new GroupName("M32081", new GroupNameValidatorItmo()));
    }

    [Theory]
    [InlineData("Yudina Svetlana")]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string studentName)
    {
        Student student = isu.AddStudent(m3108, studentName);
        Assert.Contains(student, m3108.Students);
        Assert.Equal(m3108.GroupName, student.Group);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        for (int i = 0; i < IsuService.GroupsCapacity; ++i)
        {
            isu.AddStudent(m3108, "Name");
        }

        Assert.Throws<GroupOverflowException>(() => isu.AddStudent(m3108, "Name"));
    }

    [Theory]
    [InlineData("M3208")]
    [InlineData("M31081")]
    [InlineData("M32083")]
    [InlineData("m32081")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        Assert.Throws<GroupNameException>(() => isu.AddGroup(new GroupName(groupName, new GroupNameValidatorItmo())));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Student student = isu.AddStudent(m3108, "Yudina Svetlana");
        isu.ChangeStudentGroup(student, m32081);
        Assert.Contains(student, m32081.Students);
        Assert.DoesNotContain(student, m3108.Students);
    }
}