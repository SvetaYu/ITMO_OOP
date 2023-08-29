using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class TopUp : ICommand
{
    public TopUp(CommandContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ArgumentNullException.ThrowIfNull(context.To);
        State = CommandState.Created;
    }

    public CommandState State { get; private set; }
    public CommandContext Context { get; }
    public void Execute()
    {
        Context.To.TopUp(Context.Value);
        State = CommandState.Executed;
    }

    public void UnDo()
    {
        if (State != CommandState.Executed)
        {
            throw CommandException.InvalidOperation();
        }

        Context.To.Withdraw(Context.Value);
        State = CommandState.Reverted;
    }
}