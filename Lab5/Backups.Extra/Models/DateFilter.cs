using Backups.Models;

namespace Backups.Extra.Models;

public class DateFilter : IFilter
{
    private readonly TimeSpan _timeSpan;
    public DateFilter(TimeSpan timeSpan)
    {
        _timeSpan = timeSpan;
    }

    public IEnumerable<RestorePoint> GetPoints(IEnumerable<RestorePoint> points)
    {
        DateTime now = DateTime.Now;
        return points.Where(point => now - point.Time > _timeSpan);
    }
}