using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Models;

public class InMemoryRepositoryJson : IRepositoryJson
{
    private MemoryFileSystem _fs;

    public InMemoryRepositoryJson(MemoryFileSystem fs)
    {
        _fs = fs;
    }

    public void WriteText(string path, string text)
    {
        _fs.WriteAllText(path, text);
    }

    public string ReadText(string path)
    {
        return _fs.ReadAllText(path);
    }

    public bool IsFileExists(string path)
    {
        return _fs.FileExists(path);
    }
}