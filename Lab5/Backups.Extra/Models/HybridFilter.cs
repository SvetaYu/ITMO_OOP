using Backups.Models;

namespace Backups.Extra.Models;

public class HybridFilter : IFilter
{
    private LogicalOperation _logicalOperation;
    private List<IFilter> _filters;

    public HybridFilter(IEnumerable<IFilter> filters, LogicalOperation logicalOperation)
    {
        _logicalOperation = logicalOperation;
        _filters = filters.ToList();
    }

    public IEnumerable<RestorePoint> GetPoints(IEnumerable<RestorePoint> points)
    {
        IEnumerable<RestorePoint> answer = new List<RestorePoint>();
        return _filters.Select(filter => filter.GetPoints(points)).Aggregate(answer, (current, filteredPoints) => _logicalOperation == LogicalOperation.And ? current.Intersect(filteredPoints) : current.Union(filteredPoints));
    }
}