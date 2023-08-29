using System.IO.Compression;
using Backups.Exceptions;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    [JsonProperty("zipFolder")]
    private readonly ZipFolder _zipFolder;

    [JsonProperty("repository")]
    private readonly IRepository _repository;

    [JsonConstructor]
    public ZipStorage(IRepository repository, string path, ZipFolder zipFolder)
    {
        Path = path ?? throw new ArgumentNullException(nameof(path));
        _zipFolder = zipFolder ?? throw new ArgumentNullException(nameof(zipFolder));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public string Path { get; }

    public IDisposableFolder GetObjects()
    {
        IRepositoryObject repoObj = _repository.GetObject(Path);

        if (repoObj is not RepoFile file)
            throw ZipStorageException.NotValidExtension();

        Stream stream = file.OpenRead();
        var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        IEnumerable<IRepositoryObject> repositoryObjects = _zipFolder.ZipObjects
            .Select(obj => obj.GetRepositoryObject(zip.GetEntry(obj.Name)));

        return new DisposableFolder(repositoryObjects, stream);
    }
}