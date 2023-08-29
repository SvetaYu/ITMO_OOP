using Backups.Extra.Models;
using Backups.Extra.Services;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class BackupTaskCollection
{
    private readonly List<BackupTaskExtra> _tasks;
    private readonly JsonSerializerSettings _settings;
    private readonly IRepositoryJson _repository;

    public BackupTaskCollection(IRepositoryJson repository)
    {
        _repository = repository;
        _settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        };

        _tasks = Restore().ToList();
    }

    public IReadOnlyCollection<BackupTaskExtra> Tasks => _tasks.AsReadOnly();

    public void AddTask(BackupTaskExtra task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (_tasks.Contains(task))
        {
            throw new Exception("Task is already tracked");
        }

        _tasks.Add(task);
    }

    public void Store()
    {
        string json = JsonConvert.SerializeObject(_tasks, _settings);
        _repository.WriteText("store.json", json);
    }

    private IEnumerable<BackupTaskExtra> Restore()
    {
        if (!_repository.IsFileExists("store.json")) return Array.Empty<BackupTaskExtra>();
        string json = _repository.ReadText("store.json");
        return JsonConvert.DeserializeObject<IEnumerable<BackupTaskExtra>>(json, _settings);
    }
}