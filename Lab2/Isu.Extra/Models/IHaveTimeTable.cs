using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public interface IHaveTimeTable
{
    Timetable Timetable { get; }
}