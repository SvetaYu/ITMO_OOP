namespace Backups.Models;

public interface IRepoFile : IRepositoryObject
{
    Stream OpenRead();
}