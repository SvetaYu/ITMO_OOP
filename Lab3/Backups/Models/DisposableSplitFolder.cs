namespace Backups.Models;

public class DisposableSplitFolder : IDisposableFolder
{
    private readonly List<IDisposableFolder> _folders;

    public DisposableSplitFolder(IEnumerable<IDisposableFolder> folders)
    {
        _folders = folders.ToList();
    }

    public void Dispose()
    {
        foreach (IDisposableFolder obj in _folders)
        {
            obj.Dispose();
        }
    }

    public IEnumerable<IRepositoryObject> GetObjects()
    {
        return _folders.SelectMany(obj => obj.GetObjects());
    }
}