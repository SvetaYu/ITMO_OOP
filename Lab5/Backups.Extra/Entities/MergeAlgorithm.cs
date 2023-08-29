using Backups.Entities;
using Backups.Extra.Models;
using Backups.Models;
using Backups.Services;

namespace Backups.Extra.Entities;

public class MergeAlgorithm : IControlRestorePointsAlgorithm
{
    private ILogger _logger;

    public MergeAlgorithm(ILogger logger)
    {
        _logger = logger;
    }

    public void Execute(IEnumerable<RestorePoint> points, IBackupTask task, IBackup backup)
    {
        var evaluatedPoints = points.ToList();
        if (evaluatedPoints.Count <= 1) return;
        IAlgorithm algorithm = task.Algorithm;
        IRepository repository = task.Repository;
        IArchiver archiver = task.Archiver;
        IEnumerable<(RestorePoint Point, BackupObject Object)> po = evaluatedPoints
            .SelectMany(x => x.Objects, (p, o) => (Point: p, Object: o))
            .GroupBy(x => x.Object)
            .Select(x => x.MaxBy(xx => xx.Point.Time));

        var valueTuples = po.ToList();
        DateTime newDate = valueTuples.MaxBy(x => x.Point.Time).Point.Time;
        IEnumerable<BackupObject> backupObjects = valueTuples.Select(x => x.Object);

        var a = valueTuples.GroupBy(x => x.Point).Select(Selector).ToList();

        IEnumerable<IRepositoryObject> repoObjects = a.SelectMany(x => x.o);
        string name = newDate.ToString("dd.MM.yyyy-hh.mm.ss.ff") + "-merge";
        string restorePointPath = Path.Combine(task.Name, name);
        string newName = newDate.ToString("dd.MM.yyyy-hh.mm.ss.ff");
        repository.CreateDirectory(restorePointPath);
        IStorage storage = algorithm.DoAlgorithm(repoObjects, repository, archiver, restorePointPath);

        var newPoint = new RestorePoint(storage, backupObjects, newDate);
        backup.AddRestorePoint(newPoint);

        foreach ((IDisposableFolder folder, _) in a)
        {
            folder.Dispose();
        }

        new ClearAlgorithm().Execute(evaluatedPoints.DistinctBy(x => x.Time), task, backup);
        repository.Rename(Path.Combine(task.Name, name), Path.Combine(task.Name, newName));
        _logger.Log($"merge in {newName}");
    }

    private (IDisposableFolder folder, IEnumerable<IRepositoryObject> o) Selector(
        IGrouping<RestorePoint, (RestorePoint Point, BackupObject Object)> x)
    {
        IDisposableFolder folder = x.Key.Storage.GetObjects();

        IEnumerable<IRepositoryObject> o = x.SelectMany(xx => folder.GetObjects()
            .Where(xxx => xxx.Name.Equals(xx.Object.Name) || xxx.Name.Equals(xx.Object.Name + ".zip")));

        return (folder, o);
    }
}