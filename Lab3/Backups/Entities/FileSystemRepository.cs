using System.Reflection.PortableExecutable;
using Backups.Exceptions;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Entities;

public class FileSystemRepository : IRepository
{
    [JsonConstructor]
    public FileSystemRepository(string directory)
    {
        Directory = directory ?? throw new ArgumentNullException(nameof(directory));
    }

    public string Directory { get; }

    public IRepositoryObject GetObject(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        IRepositoryObject obj;
        if (File.Exists(path))
        {
            obj = new RepoFile(Path.GetFileName(path), () => System.IO.File.OpenRead(path));
        }
        else if (System.IO.Directory.Exists(path))
        {
            obj = new RepoFolder(new DirectoryInfo(path).Name, () => System.IO.Directory.EnumerateFileSystemEntries(path).Select(GetObject));
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
        string path = Path.Combine(Directory, name);
        System.IO.Directory.CreateDirectory(path);
    }

    public Stream OpenWrite(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        return File.OpenWrite(path);
    }

    public void DeleteDirectory(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        foreach (string objPath in System.IO.Directory.EnumerateFileSystemEntries(path))
        {
            if (File.Exists(objPath))
            {
                File.Delete(objPath);
            }
            else
            {
                DeleteDirectory(objPath);
            }
        }

        System.IO.Directory.Delete(path);
    }

    public void Rename(string sourceDirName, string destDirName)
    {
        string sourcePath = Path.Combine(Directory, sourceDirName);
        string destPath = Path.Combine(Directory, destDirName);
        System.IO.Directory.Move(sourcePath, destPath);
    }

    public bool DirectoryExists(string path)
    {
        return System.IO.Directory.Exists(path);
    }
}