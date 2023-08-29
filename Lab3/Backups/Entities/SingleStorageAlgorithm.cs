using Backups.Models;

namespace Backups.Entities;

public class SingleStorageAlgorithm : IAlgorithm
{
    public IStorage DoAlgorithm(IEnumerable<IRepositoryObject> repObjects, IRepository repository, IArchiver archiver, string restorePointPath)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(repObjects);
        string path = Path.Combine(repository.Directory, restorePointPath);
        return archiver.Archive(repository, repObjects, path, "storage.zip");
    }
}