using Zio;
using Zio.FileSystems;

namespace DataAccess.Model;

public class InMemoryRepository : Repository, IDisposable
{
    private readonly MemoryFileSystem _fs = new MemoryFileSystem();

    public InMemoryRepository()
    {
        _fs.CreateDirectory("/home");
        var subFs = new SubFileSystem(_fs, "/home");
        Path = "/home/reports";
        _fs.CreateDirectory(Path);
    }

    public override string Path { get; set; }

    public override void CreateFileWithText(string path, string text)
    {
        string newPath = System.IO.Path.Combine(Path, path);
        _fs.WriteAllText(newPath, text);
    }

    public override void CreateDirectory(string name)
    {
        string path = System.IO.Path.Combine(Path, name);
        if (!_fs.DirectoryExists(path))
        {
            _fs.CreateDirectory(path);
        }
    }

    public void Dispose()
    {
        _fs.Dispose();
    }
}