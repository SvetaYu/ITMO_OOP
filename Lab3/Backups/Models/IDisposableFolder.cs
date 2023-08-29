namespace Backups.Models;

public interface IDisposableFolder : IDisposable
{
    IEnumerable<IRepositoryObject> GetObjects();
}