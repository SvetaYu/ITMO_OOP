using Banks.Entities;

namespace Banks.Models;

public class ChainItem
{
    public ChainItem(ICommand command)
    {
        Command = command;
    }

    public ICommand Command { get; }
    public ChainItem Next { get; set; }
    public ChainItem Prev { get; set; }

    public bool Execute()
    {
        try
        {
            Command.Execute();
            return Next?.Execute() ?? true;
        }
        catch (Exception)
        {
            Prev?.Revert();
            return false;
        }
    }

    public void Revert()
    {
        Command.UnDo();
        Prev?.Revert();
    }
}