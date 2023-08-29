using Isu.Exceptions;
using Isu.Services;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    public GroupName(string name, IGroupNameValidator validator)
    {
        if (name is null)
        {
            throw new GroupNameNullException();
        }

        if (validator is null)
        {
            throw new GroupNameValidatorNullException();
        }

        if (!validator.Validate(name))
        {
            throw new GroupNameException("invalid group name");
        }

        Name = name;
    }

    public string Name { get; }

    public static bool operator ==(GroupName lhs, GroupName rhs)
    {
        return Equals(lhs, rhs);
    }

    public static bool operator !=(GroupName lhs, GroupName rhs)
    {
        return !Equals(lhs, rhs);
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object obj)
    {
        if (obj is GroupName groupName)
        {
            return Equals(groupName);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public bool Equals(GroupName other)
    {
        return other != null && Name == other.Name;
    }
}