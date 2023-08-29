using Banks.Models;

namespace Banks.Entities;

public interface ICommand
{
    CommandState State { get; }
    CommandContext Context { get; }
    void Execute();
    void UnDo();
}