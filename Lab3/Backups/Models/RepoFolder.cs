using Backups.Entities;
using Newtonsoft.Json;

namespace Backups.Models;

public class RepoFolder : IRepoFolder
{
    private readonly Func<IEnumerable<IRepositoryObject>> _factory;

    [JsonConstructor]
    public RepoFolder(string name, Func<IEnumerable<IRepositoryObject>> factory)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.Visit(this);
    }

    public IEnumerable<IRepositoryObject> GetObjects()
    {
        return _factory();
    }
}