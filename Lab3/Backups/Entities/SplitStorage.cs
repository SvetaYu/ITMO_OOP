using System.Text.Json.Serialization;
using Backups.Models;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<IStorage> _storages;

    [JsonConstructor]
    public SplitStorage(IEnumerable<IStorage> storages)
    {
        _storages = storages?.ToList() ?? throw new ArgumentNullException(nameof(storages));
    }

    public IDisposableFolder GetObjects()
    {
        return new DisposableSplitFolder(_storages.Select(obj => obj.GetObjects()));
    }
}