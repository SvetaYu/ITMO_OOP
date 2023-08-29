using Backups.Exceptions;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class InMemoryRepository : IRepository
{
    private readonly MemoryFileSystem _fs;

    public InMemoryRepository(string directory, MemoryFileSystem fs)
    {
        _fs = fs ?? throw new ArgumentNullException(nameof(fs));
        Directory = directory ?? throw new ArgumentNullException(nameof(directory));
    }

    public string Directory { get; }

    public Stream OpenWrite(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        return _fs.OpenFile(path, FileMode.OpenOrCreate, FileAccess.Write);
    }

    public IRepositoryObject GetObject(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        IRepositoryObject obj;
        if (_fs.FileExists(path))
        {
            obj = new RepoFile(Path.GetFileName(path), () => _fs.OpenFile(path, FileMode.Open, FileAccess.Read));
        }
        else if (_fs.DirectoryExists(path))
        {
            obj = new RepoFolder(_fs.GetDirectoryEntry(path).Name, () => _fs.GetDirectoryEntry(path).EnumerateEntries().Select(entry => GetObject(entry.Path.FullName)));
        }
        else
        {
            throw RepositoryException.FileOrDirectoryDoesntExist(path);
        }

        return obj;
    }

    public void CreateDirectory(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _fs.CreateDirectory(Path.Combine(Directory, name));
    }

    public void DeleteDirectory(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        _fs.DeleteDirectory(path, true);
    }

    public void Rename(string sourceDirName, string destDirName)
    {
        string sourcePath = Path.Combine(Directory, sourceDirName);
        string destPath = Path.Combine(Directory, destDirName);
        _fs.MoveDirectory(sourcePath, destPath);
    }

    public bool DirectoryExists(string path)
    {
        return _fs.DirectoryExists(path);
    }
}