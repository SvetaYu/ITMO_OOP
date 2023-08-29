using Backups.Models;

namespace Backups.Entities;

public interface IAlgorithm
{
    IStorage DoAlgorithm(IEnumerable<IRepositoryObject> repObjects, IRepository repository, IArchiver archiver, string restorePointPath);
}