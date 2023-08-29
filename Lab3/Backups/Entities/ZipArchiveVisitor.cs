using System.IO.Compression;
using Backups.Models;
using ZipFile = Backups.Models.ZipFile;

namespace Backups.Entities;

public class ZipArchiveVisitor : IRepositoryObjectVisitor
{
    private readonly Stack<List<IZipObject>> _zipObjectsStack = new Stack<List<IZipObject>>();
    private readonly Stack<ZipArchive> _zipArchiveStack = new Stack<ZipArchive>();

    public ZipArchiveVisitor(ZipArchive zip)
    {
        ArgumentNullException.ThrowIfNull(zip);
        _zipObjectsStack.Push(new List<IZipObject>());
        _zipArchiveStack.Push(zip);
    }

    public void Visit(IRepoFile repoFile)
    {
        ArgumentNullException.ThrowIfNull(repoFile);

        using Stream zipStream = _zipArchiveStack.Peek().CreateEntry(repoFile.Name).Open();
        using Stream fileStream = repoFile.OpenRead();
        fileStream.CopyTo(zipStream);
        _zipObjectsStack.Peek().Add(new ZipFile(repoFile.Name));
    }

    public void Visit(IRepoFolder repoFolder)
    {
        ArgumentNullException.ThrowIfNull(repoFolder);

        Stream stream = _zipArchiveStack.Peek().CreateEntry(repoFolder.Name + ".zip").Open();
        var zip = new ZipArchive(stream, ZipArchiveMode.Create);
        _zipArchiveStack.Push(zip);
        _zipObjectsStack.Push(new List<IZipObject>());
        foreach (IRepositoryObject obj in repoFolder.GetObjects())
        {
            obj.Accept(this);
        }

        _zipArchiveStack.Peek().Dispose();
        _zipArchiveStack.Pop();
        List<IZipObject> popped = _zipObjectsStack.Pop();
        _zipObjectsStack.Peek().Add(new ZipFolder(popped, repoFolder.Name));
    }

    public IEnumerable<IZipObject> GetZipObjectsPeak()
    {
        return _zipObjectsStack.Peek();
    }
}