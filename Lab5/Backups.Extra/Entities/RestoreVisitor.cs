using Backups.Entities;
using Backups.Models;

namespace Backups.Extra.Entities;

public class RestoreVisitor : IRepositoryObjectVisitor
{
    private readonly IRepository _repository;
    private readonly Stack<string> _pathsStack = new Stack<string>();

    public RestoreVisitor(string path, IRepository repository)
    {
        _pathsStack.Push(Path.Combine(repository.Directory, path));
        _repository = repository;
    }

    public void Visit(IRepoFile repoFile)
    {
        string newPath = Path.Combine(_pathsStack.Peek(), repoFile.Name);
        using Stream newStream = _repository.OpenWrite(newPath);
        using Stream fileStream = repoFile.OpenRead();
        fileStream.CopyTo(newStream);
    }

    public void Visit(IRepoFolder repoFolder)
    {
        string newPath = Path.Combine(_pathsStack.Peek(), Path.GetFileNameWithoutExtension(repoFolder.Name) ?? throw new InvalidOperationException());
        if (_repository.DirectoryExists(newPath))
        {
            _repository.DeleteDirectory(newPath);
        }

        _repository.CreateDirectory(newPath);
        _pathsStack.Push(newPath);
        foreach (IRepositoryObject obj in repoFolder.GetObjects())
        {
            obj.Accept(this);
        }

        _pathsStack.Pop();
    }
}