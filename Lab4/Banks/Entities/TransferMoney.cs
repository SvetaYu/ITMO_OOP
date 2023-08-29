using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class TransferMoney : ICommand
{
    private readonly ChainItem _chain;
    public TransferMoney(CommandContext context)
    {
        Context = context;
        ArgumentNullException.ThrowIfNull(context.To);
        ArgumentNullException.ThrowIfNull(context.From);
        State = CommandState.Created;
        _chain = new ChainItem(new Withdrawal(context));
        var item = new ChainItem(new CommissionDeduction(context));
        _chain.Next = item;
        item.Prev = _chain;
        var item2 = new ChainItem(new TopUp(context));
        item.Next = item2;
        item2.Prev = item;
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

        var endOfChain = _chain.Next.Next;
        endOfChain.Revert();
        State = CommandState.Reverted;
    }
}