using System.IO.Compression;
using Newtonsoft.Json;

namespace Backups.Models;

public class ZipFolder : IZipObject
{
    [JsonProperty("zipObjects")]
    private readonly List<IZipObject> _zipObjects;

    [JsonConstructor]
    public ZipFolder(IEnumerable<IZipObject> zipObjects, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _zipObjects = zipObjects?.ToList() ?? throw new ArgumentNullException(nameof(zipObjects));
        Name = Path.GetFileNameWithoutExtension(name) + ".zip";
    }

    [JsonIgnore]
    public IReadOnlyCollection<IZipObject> ZipObjects => _zipObjects;

    public string Name { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zip)
    {
        ArgumentNullException.ThrowIfNull(zip);
        var newZip = new ZipArchive(zip.Open());
        return new RepoFolder(zip.Name, () => _zipObjects.Select(obj => obj.GetRepositoryObject(newZip.GetEntry(obj.Name))));
    }
}