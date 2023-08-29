using Backups.Entities;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Services;

public class BackupTask : IBackupTask
{
    [JsonProperty("backup")]
    private readonly IBackup _backup;

    [JsonProperty]
    private readonly List<BackupObject> _objects;

    public BackupTask(string name, IRepository repository, IAlgorithm algorithm, IEnumerable<BackupObject> objects, IBackup backup, IArchiver archiver)
    {
        Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _objects = objects?.ToList() ?? throw new ArgumentNullException(nameof(objects));
        _backup = backup ?? throw new ArgumentNullException(nameof(backup));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Repository.CreateDirectory(name);
        Archiver = archiver ?? throw new ArgumentNullException(nameof(archiver));
    }

    public IArchiver Archiver { get; }
    public IEnumerable<BackupObject> Objects => _objects;
    public IAlgorithm Algorithm { get; private set; }
    public IRepository Repository { get; }

    [JsonIgnore]
    public IReadOnlyCollection<RestorePoint> RestorePoints => _backup.Points;
    public string Name { get; }

    public void AddObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _objects.Add(backupObject);
    }

    public void RemoveObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _objects.Remove(backupObject);
    }

    public void ChangeAlgorithm(IAlgorithm algorithm)
    {
        ArgumentNullException.ThrowIfNull(algorithm);
        Algorithm = algorithm;
    }

    public void DoTask()
    {
        DateTime time = DateTime.Now;
        string dirName = time.ToString("dd.MM.yyyy-hh.mm.ss.ff");
        Repository.CreateDirectory(Path.Combine(Name, dirName));
        string restorePointPath = Path.Combine(Name, dirName);
        IEnumerable<IRepositoryObject> repoObjects = _objects.Select(obj => obj.GetRepositoryObject());
        IStorage storage = Algorithm.DoAlgorithm(repoObjects, Repository, Archiver, restorePointPath);
        _backup.AddRestorePoint(new RestorePoint(storage, Objects, time));
    }
}