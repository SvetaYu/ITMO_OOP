using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber : IEquatable<CourseNumber>
{
    public const int MaxCourse = 4;
    public const int MinCourse = 1;

    public CourseNumber(int number)
    {
        if (number > MaxCourse || number < MinCourse)
        {
            throw new CourseNumberException("invalid course number");
        }

        Number = number;
    }

    public int Number { get; }

    public override string ToString()
    {
        return Convert.ToString(Number);
    }

    public override bool Equals(object obj)
    {
        if (obj is CourseNumber course)
        {
            return Equals(course);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Number.GetHashCode();
    }

    public bool Equals(CourseNumber other)
    {
        return other != null && this.Number == other.Number;
    }
}