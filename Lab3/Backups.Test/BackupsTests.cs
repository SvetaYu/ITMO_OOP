using Backups.Entities;
using Backups.Models;
using Backups.Services;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTests : IDisposable
{
    private MemoryFileSystem _fs = new MemoryFileSystem();
    private BackupTask task;
    private BackupObject obj1;
    private BackupObject obj2;

    public BackupsTests()
    {
        _fs.CreateDirectory("/home");
        var subFs = new SubFileSystem(_fs, "/home");
        _fs.CreateDirectory("/home/repo");
        _fs.CreateDirectory("/home/repo1");
        _fs.CreateDirectory("/home/repo2");
        Stream stream = _fs.CreateFile("/home/repo1/obj1.txt");
        stream.Close();
        _fs.CreateDirectory("/home/repo2/obj2");
        stream = _fs.CreateFile("/home/repo2/obj2/brrr.txt");
        stream.Close();
        stream = _fs.CreateFile("/home/repo2/obj2/123.txt");
        stream.Close();
        var repo = new InMemoryRepository("/home/repo", _fs);
        obj1 = new BackupObject("obj1.txt", new InMemoryRepository("/home/repo1", _fs));
        obj2 = new BackupObject("obj2", new InMemoryRepository("/home/repo2", _fs));
        var objects = new List<BackupObject>();

        objects.Add(obj1);
        objects.Add(obj2);
        task = new BackupTask("task1", repo, new SplitStorageAlgorithm(), objects, new Backup(), new Archiver());
    }

    [Fact]
    public void TasksDirectoryWasCreate()
    {
        Assert.True(_fs.DirectoryExists("/home/repo/task1"));
    }

    [Fact]
    public void DoSingleStorageAlgorithm()
    {
        task.DoTask();
        string dirName = task.RestorePoints.Last().Time.ToString("dd.MM.yyyy-hh.mm.ss.ff");
        var path = "/home/repo/task1/" + dirName;
        Assert.True(_fs.FileExists(path + "/obj1(1).zip"));
        Assert.True(_fs.FileExists(path + "/obj2(1).zip"));

        // Thread.Sleep(60000);
        task.RemoveObject(obj1);
        task.DoTask();
        dirName = task.RestorePoints.Last().Time.ToString("dd.MM.yyyy-hh.mm.ss.ff");

        path = "/home/repo/task1/" + dirName;

        // Assert.False(_fs.FileExists(path + "/obj1(1).zip"));
        Assert.True(_fs.FileExists(path + "/obj2(1).zip"));
    }

    public void Dispose()
    {
        _fs.Dispose();
    }
}