using Backups.Models;

namespace Backups.Entities;

public interface IArchiver
{
    ZipStorage Archive(IRepository repository, IEnumerable<IRepositoryObject> objects, string path, string zipName);
}