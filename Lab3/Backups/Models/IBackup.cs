namespace Backups.Models;

public interface IBackup
{
    IReadOnlyCollection<RestorePoint> Points { get; }

    void AddRestorePoint(RestorePoint point);

    void RemoveRestorePoint(RestorePoint point);
}