using Newtonsoft.Json;

namespace Backups.Models;

public class Backup : IBackup
{
    [JsonProperty]
    private readonly List<RestorePoint> _points = new List<RestorePoint>();

    [JsonIgnore]
    public IReadOnlyCollection<RestorePoint> Points => _points;

    public void AddRestorePoint(RestorePoint point)
    {
        ArgumentNullException.ThrowIfNull(point);
        _points.Add(point);
    }

    public void RemoveRestorePoint(RestorePoint point)
    {
        ArgumentNullException.ThrowIfNull(point);
        _points.Remove(point);
    }
}