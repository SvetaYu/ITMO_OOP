using Backups.Entities;

namespace Backups.Models;

public interface IRepositoryObject
{
    string Name { get; }
    void Accept(IRepositoryObjectVisitor visitor);
}