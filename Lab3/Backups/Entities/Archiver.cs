using System.IO.Compression;
using Backups.Models;

namespace Backups.Entities;

public class Archiver : IArchiver
{
    public ZipStorage Archive(IRepository repository, IEnumerable<IRepositoryObject> objects, string path, string zipName)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(objects);
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(zipName);

        Stream stream = repository.OpenWrite(Path.Combine(path, zipName));
        var zipArch = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipArchiveVisitor(zipArch);
        foreach (IRepositoryObject obj in objects)
        {
            obj.Accept(visitor);
        }

        zipArch.Dispose();
        stream.Close();
        stream.Dispose();
        return new ZipStorage(repository, Path.Combine(path, zipName), new ZipFolder(visitor.GetZipObjectsPeak(), zipName));
    }
}