using Backups.Extra.Models;
using Backups.Models;
using Backups.Services;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class BackupExtra : IBackup, IBackupExtra
{
    [JsonProperty("backup")]
    private readonly IBackup _backup;
    [JsonProperty("logger")]
    private readonly ILogger _logger;
    public BackupExtra(IBackup backup, ILogger logger)
    {
        _backup = backup;
        _logger = logger;
    }

    [JsonIgnore]
    public IReadOnlyCollection<RestorePoint> Points => _backup.Points;
    public void AddRestorePoint(RestorePoint point)
    {
        _backup.AddRestorePoint(point);
        _logger.Log($"add point: {point.Time}");
    }

    public void RemoveRestorePoint(RestorePoint point)
    {
        _backup.RemoveRestorePoint(point);
        _logger.Log($"remove point: {point.Time}");
    }

    public void ControlOfTheNumberOfRestorePoints(IControlRestorePointsAlgorithm algorithm, IFilter filter, IBackupTask task)
    {
        IEnumerable<RestorePoint> points = filter.GetPoints(Points);
        Console.WriteLine(Points.Count);
        Console.WriteLine(task.RestorePoints.Count);
        algorithm.Execute(points.ToList(), task, this);
    }
}