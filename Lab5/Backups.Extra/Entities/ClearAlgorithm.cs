using Backups.Models;
using Backups.Services;

namespace Backups.Extra.Entities;

public class ClearAlgorithm : IControlRestorePointsAlgorithm
{
    public void Execute(IEnumerable<RestorePoint> points, IBackupTask task, IBackup backup)
    {
        foreach (RestorePoint point in points)
        {
            string name = point.Time.ToString("dd.MM.yyyy-hh.mm.ss.ff");
            string dir = Path.Combine(task.Repository.Directory, task.Name, name);
            task.Repository.DeleteDirectory(dir);
            backup.RemoveRestorePoint(point);
        }
    }
}