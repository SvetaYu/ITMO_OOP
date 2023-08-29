using DataAccess;
using DataAccess.Model;

namespace Application.Dto;

public class DeleteFileHandler : DHandler
{
    private string Command { get; }

    public override bool HandleRequest(CommandContext context, CurrentRepository repository)
    {
        if (!context.Command.Equals(Command)) return Next is not null && Next.HandleRequest(context, repository);
        repository.DeleteFile();

            return true;

    }
}

public class CommandContext
{
    public CommandContext(string command, string arg)
    {
        Command = command;
        Arg = arg;
    }

    public string Command { get; }
    public string Arg { get; }

}

public abstract class DHandler
{
    public DHandler Next { get; set; }
    public abstract bool HandleRequest(CommandContext context,  CurrentRepository repository);
}

public class Chain
{
    public Chain(params DHandler[] handlers)
    {
        Root = handlers.First();
        for (int i = 0; i < handlers.Length - 1; i++)
        {
            handlers[i].Next = handlers[i + 1];
        }
    }
    public DHandler Root { get; set; }
    public bool Execute(CommandContext context,  CurrentRepository repository) => Root.HandleRequest(context, repository);
}

public class CurrentRepository
{
    public void DeleteFile()
    {
        throw new Exception();
    }
}