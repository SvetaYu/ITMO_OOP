using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class CommissionDeduction : ICommand
{
    private decimal _commission = 0;
    public CommissionDeduction(CommandContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ArgumentNullException.ThrowIfNull(context.From);
        State = CommandState.Created;
    }

    public CommandState State { get; private set; }
    public CommandContext Context { get; }
    public void Execute()
    {
        _commission = Context.From.CommissionDeduction();
        State = CommandState.Executed;
    }

    public void UnDo()
    {
        if (State != CommandState.Executed)
        {
            throw CommandException.InvalidOperation();
        }

        Context.From.TopUp(_commission);
        State = CommandState.Reverted;
    }
}