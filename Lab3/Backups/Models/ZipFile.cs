using System.IO.Compression;

namespace Backups.Models;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zip)
    {
        ArgumentNullException.ThrowIfNull(zip);
        return new RepoFile(Name, zip.Open);
    }
}