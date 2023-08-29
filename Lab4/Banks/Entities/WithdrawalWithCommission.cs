using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class WithdrawalWithCommission : ICommand
{
    private readonly ChainItem _chain;
    public WithdrawalWithCommission(CommandContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ArgumentNullException.ThrowIfNull(context.From);
        State = CommandState.Created;
        _chain = new ChainItem(new Withdrawal(context));
        var item = new ChainItem(new CommissionDeduction(context));
        _chain.Next = item;
        item.Prev = _chain;
    }

    public CommandState State { get; private set; }
    public CommandContext Context { get; }
    public void Execute()
    {
        if (!_chain.Execute())
        {
            throw CommandException.OperationFailed();
        }

        State = CommandState.Executed;
    }

    public void UnDo()
    {
        if (State != CommandState.Executed)
        {
            throw CommandException.InvalidOperation();
        }

        _chain.Next.Revert();
        State = CommandState.Reverted;
    }
}