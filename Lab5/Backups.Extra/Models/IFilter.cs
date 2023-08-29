using Backups.Models;

namespace Backups.Extra.Models;

public interface IFilter
{
    IEnumerable<RestorePoint> GetPoints(IEnumerable<RestorePoint> points);
}