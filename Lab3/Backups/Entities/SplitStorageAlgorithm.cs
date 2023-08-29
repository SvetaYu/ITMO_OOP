using Backups.Models;

namespace Backups.Entities;

public class SplitStorageAlgorithm : IAlgorithm
{
    public IStorage DoAlgorithm(IEnumerable<IRepositoryObject> repObjects, IRepository repository, IArchiver archiver, string restorePointPath)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(repObjects);
        string path = Path.Combine(repository.Directory, restorePointPath);

        IEnumerable<IStorage> storages = repObjects.Select(obj => ArchiveSingle(obj, repository, archiver, path));

        return new SplitStorage(storages);
    }

    private IStorage ArchiveSingle(IRepositoryObject obj, IRepository repository, IArchiver archiver, string path)
    {
        var array = new IRepositoryObject[] { obj };
        string name = Path.GetFileNameWithoutExtension(obj.Name) + "(1).zip";
        return archiver.Archive(repository, array, path, name);
    }
}