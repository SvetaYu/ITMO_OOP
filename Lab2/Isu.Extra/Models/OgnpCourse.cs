using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class OgnpCourse
{
    public const int CourseCapacity = 2;
    private readonly List<Flow> _flows;

    internal OgnpCourse(char faculty)
    {
        CountOfFlows = 0;
        Faculty = faculty;
        _flows = new List<Flow>();
    }

    public int CountOfFlows { get; }
    public char Faculty { get; }

    internal Flow AddFlow(Flow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);
        if (CountOfFlows == CourseCapacity)
        {
            throw OgnpCourseException.CourseOverFlow();
        }

        _flows.Add(flow);
        return _flows.Last();
    }
}