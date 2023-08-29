using Backups.Entities;
namespace Backups.Models;

public class BackupObject
{
    public BackupObject(string name, IRepository repository)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public string Name { get; }
    public IRepository Repository { get; }

    public IRepositoryObject GetRepositoryObject()
    {
        return Repository.GetObject(Path.Combine(Repository.Directory, Name));
    }
}