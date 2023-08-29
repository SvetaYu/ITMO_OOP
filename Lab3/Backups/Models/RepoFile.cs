using Backups.Entities;
using Newtonsoft.Json;

namespace Backups.Models;

public class RepoFile : IRepoFile
{
    private readonly Func<Stream> _factory;

    [JsonConstructor]
    public RepoFile(string name, Func<Stream> factory)
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

    public Stream OpenRead()
    {
        return _factory();
    }

    public void Dispose()
    {
    }
}