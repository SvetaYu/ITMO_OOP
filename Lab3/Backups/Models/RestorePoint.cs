using Backups.Entities;
using Newtonsoft.Json;

namespace Backups.Models;

public class RestorePoint
{
    [JsonProperty]
    private readonly List<BackupObject> _objects;

    [JsonConstructor]
    public RestorePoint(IStorage storage, IEnumerable<BackupObject> objects, DateTime time)
    {
        Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        Time = time;
        _objects = objects?.ToList() ?? throw new ArgumentNullException(nameof(objects));
    }

    public IStorage Storage { get; }
    public IReadOnlyCollection<BackupObject> Objects => _objects;
    public DateTime Time { get; }
}