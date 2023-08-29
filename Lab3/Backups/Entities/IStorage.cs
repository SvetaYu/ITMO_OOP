using Backups.Models;

namespace Backups.Entities;

public interface IStorage
{
    public IDisposableFolder GetObjects();
}