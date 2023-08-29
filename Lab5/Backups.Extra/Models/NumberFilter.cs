using Backups.Models;

namespace Backups.Extra.Models;

public class NumberFilter : IFilter
{
    private readonly int _number;

    public NumberFilter(int number)
    {
        _number = number;
    }

    public IEnumerable<RestorePoint> GetPoints(IEnumerable<RestorePoint> points)
    {
        return points.OrderByDescending(point => point.Time).Skip(_number);
    }
}