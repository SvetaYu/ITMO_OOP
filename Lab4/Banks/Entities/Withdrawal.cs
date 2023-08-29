using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class Withdrawal : ICommand
{
    public Withdrawal(CommandContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ArgumentNullException.ThrowIfNull(context.From);
        State = CommandState.Created;
    }

    public CommandState State { get; private set; }
    public CommandContext Context { get; }

    public void Execute()
    {
        Context.From.Withdraw(Context.Value);
        State = CommandState.Executed;
    }

    public void UnDo()
    {
        if (State != CommandState.Executed)
        {
            throw CommandException.InvalidOperation();
        }

        Context.From.TopUp(Context.Value);
        State = CommandState.Reverted;
    }
}