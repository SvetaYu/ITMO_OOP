namespace Backups.Models;

public interface IRepoFolder : IRepositoryObject
{
    IEnumerable<IRepositoryObject> GetObjects();
}