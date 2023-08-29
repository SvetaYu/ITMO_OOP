using Backups.Entities;
using Backups.Models;
using Backups.Services;

var repo = new FileSystemRepository(@"C:\Users\Core i5-8250\Desktop\repo");
var obj1 = new BackupObject("obj1.txt", new FileSystemRepository(@"C:\Users\Core i5-8250\Desktop\repo1"));

var obj2 = new BackupObject("obj2", new FileSystemRepository(@"C:\Users\Core i5-8250\Desktop\repo2"));
var objects = new List<BackupObject>();

objects.Add(obj2);
objects.Add(obj1);
var task = new BackupTask("task1", repo, new SingleStorageAlgorithm(), objects,  new Backup(), new Archiver());
task.DoTask();

task.ChangeAlgorithm(new SplitStorageAlgorithm());

task.DoTask();