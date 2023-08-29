using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Models;
using Backups.Extra.Services;
using Backups.Models;
using Backups.Services;

namespace Backups.Extra;

public static class Program
{
    public static void Main(string[] args)
    {
        BackupTaskExtra task;
        var logger = new ConsoleLogger();
        var collection = new BackupTaskCollection(new FileSystemRepositoryJson());
        if (collection.Tasks.Any())
        {
            task = collection.Tasks.First();
        }
        else
        {
            var repo = new FileSystemRepository(@"C:\Users\Core i5-8250\Desktop\repo");
            var obj1 = new BackupObject("obj1.txt", new FileSystemRepository(@"C:\Users\Core i5-8250\Desktop\repo1"));

            var obj2 = new BackupObject("obj2", new FileSystemRepository(@"C:\Users\Core i5-8250\Desktop\repo2"));
            var objects = new List<BackupObject>();

            objects.Add(obj2);
            objects.Add(obj1);
            var backup = new BackupExtra(new Backup(), logger);
            var task1 = new BackupTask("task2", repo, new SingleStorageAlgorithm(), objects, backup, new Archiver());
            task = new BackupTaskExtra(task1, logger, backup);
            collection.AddTask(task);
        }

        var span = TimeSpan.FromMinutes(1);
        task.DoTask();
        collection.Store();
        task.ControlOfTheNumberOfRestorePoints(new MergeAlgorithm(logger), new DateFilter(TimeSpan.Zero));
    }
}