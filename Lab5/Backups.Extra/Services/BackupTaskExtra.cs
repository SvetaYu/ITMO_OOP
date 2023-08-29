using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Models;
using Backups.Models;
using Backups.Services;
using Newtonsoft.Json;

namespace Backups.Extra.Services;

public class BackupTaskExtra : IBackupTask
{
    [JsonProperty("task")]
    private readonly BackupTask _task;
    [JsonProperty("logger")]
    private ILogger _logger;
    [JsonProperty("backup")]
    private IBackupExtra _backup;

    public BackupTaskExtra(BackupTask task, ILogger logger, IBackupExtra backup)
    {
        _task = task;
        _logger = logger;
        _backup = backup;
    }

    [JsonIgnore]
    public IArchiver Archiver => _task.Archiver;
    [JsonIgnore]
    public IEnumerable<BackupObject> Objects => _task.Objects;
    [JsonIgnore]
    public IAlgorithm Algorithm => _task.Algorithm;
    [JsonIgnore]
    public IRepository Repository => _task.Repository;
    [JsonIgnore]
    public IReadOnlyCollection<RestorePoint> RestorePoints => _task.RestorePoints;
    [JsonIgnore]
    public string Name => _task.Name;

    public void DoTask()
    {
        _logger.Log("start task");
        _task.DoTask();
        _logger.Log("finish task");
    }

    public void ChangeAlgorithm(IAlgorithm algorithm)
    {
        string oldAlgorithmName = nameof(_task.Algorithm);
        _task.ChangeAlgorithm(algorithm);
        _logger.Log($"change algorithm from {oldAlgorithmName} to {nameof(algorithm)}");
    }

    public void RemoveObject(BackupObject backupObject)
    {
        _task.RemoveObject(backupObject);
        _logger.Log($"remove object: {backupObject.Name}");
    }

    public void AddObject(BackupObject backupObject)
    {
        _task.AddObject(backupObject);
        _logger.Log($"add object: {backupObject.Name}");
    }

    public void ControlOfTheNumberOfRestorePoints(IControlRestorePointsAlgorithm controlRestorePointsAlgorithm, IFilter filter)
    {
        _backup.ControlOfTheNumberOfRestorePoints(controlRestorePointsAlgorithm, filter, this);
    }
}