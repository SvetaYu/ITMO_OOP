using Isu.Extra.Exceptions;
using Isu.Models;

namespace Isu.Extra.Models;

public class Flow
{
    public const int FlowCapacity = 5;
    private readonly List<OgnpGroup> _groups = new ();

    internal Flow(string name, OgnpCourse course)
    {
        Course = course ?? throw new ArgumentNullException(nameof(course));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        GroupsCount = 0;
    }

    public int GroupsCount { get; private set; }
    public string Name { get; }
    public OgnpCourse Course { get; }

    public IReadOnlyCollection<OgnpGroup> Groups => _groups;
    internal OgnpGroup AddGroup()
    {
        if (GroupsCount == FlowCapacity)
        {
            throw FlowException.FlowOverFlow();
        }

        ++GroupsCount;
        _groups.Add(new OgnpGroup(GenerateGroupName(), Course, this));
        return _groups.Last();
    }

    private GroupName GenerateGroupName()
    {
        string name = Name + Convert.ToString(GroupsCount);
        return new GroupName(name, new OgnpGroupNameValidator());
    }
}