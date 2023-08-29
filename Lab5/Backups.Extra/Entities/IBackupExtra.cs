using Backups.Extra.Models;
using Backups.Services;

namespace Backups.Extra.Entities;

public interface IBackupExtra
{
    void ControlOfTheNumberOfRestorePoints(IControlRestorePointsAlgorithm algorithm, IFilter filter, IBackupTask task);
}