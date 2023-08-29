using Backups.Models;
using Backups.Services;

namespace Backups.Extra.Entities;

public interface IControlRestorePointsAlgorithm
{
    void Execute(IEnumerable<RestorePoint> points, IBackupTask task, IBackup backup);
}