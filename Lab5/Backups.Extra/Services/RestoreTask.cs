using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Models;
using Backups.Services;

namespace Backups.Extra.Services;

public class RestoreTask
{
    private readonly IBackupTask _task;

    public RestoreTask(IBackupTask task)
    {
        _task = task;
    }

    public void Restore(RestorePoint point, IRepository repository = null, string path = "")
    {
        while (_task.Objects.Any())
        {
            _task.RemoveObject(_task.Objects.First());
        }

        using IDisposableFolder folder = point.Storage.GetObjects();
        foreach (IRepositoryObject obj in folder.GetObjects())
        {
            BackupObject backupObj =
                point.Objects.FirstOrDefault(o => o.Name.Equals(obj.Name) || obj.Name.Equals(o.Name + ".zip"));

            IRepository repo = repository ?? backupObj?.Repository;
            var visitor = new RestoreVisitor(path, repo);
            obj.Accept(visitor);
            _task.AddObject(backupObj);
        }
    }
}