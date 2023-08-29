namespace Backups.Models;

public class DisposableFolder : IDisposableFolder
{
    private readonly IEnumerable<IRepositoryObject> _objects;
    private readonly Stream _stream;
    public DisposableFolder(IEnumerable<IRepositoryObject> objects, Stream stream)
    {
        _objects = objects;
        _stream = stream;
    }

    public IEnumerable<IRepositoryObject> GetObjects()
    {
        return _objects;
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}