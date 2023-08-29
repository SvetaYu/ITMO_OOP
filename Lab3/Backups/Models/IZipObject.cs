using System.IO.Compression;

namespace Backups.Models;

public interface IZipObject
{
    string Name { get; }
    IRepositoryObject GetRepositoryObject(ZipArchiveEntry zip);
}