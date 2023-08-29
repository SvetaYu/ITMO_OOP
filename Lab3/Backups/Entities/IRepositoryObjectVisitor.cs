using Backups.Models;

namespace Backups.Entities;

public interface IRepositoryObjectVisitor
{
    void Visit(IRepoFile repoFile);
    void Visit(IRepoFolder repoFolder);
}