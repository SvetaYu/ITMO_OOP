using Backups.Entities;
using Backups.Models;

namespace Backups.Services;

public interface IBackupTask
{
    IArchiver Archiver { get; }
    IEnumerable<BackupObject> Objects { get; }
    IAlgorithm Algorithm { get; }
    IRepository Repository { get; }
    IReadOnlyCollection<RestorePoint> RestorePoints { get; }
    string Name { get; }
    void DoTask();
    void ChangeAlgorithm(IAlgorithm algorithm);
    void RemoveObject(BackupObject backupObject);
    void AddObject(BackupObject backupObject);
}