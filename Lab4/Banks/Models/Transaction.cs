using Banks.Entities;

namespace Banks.Models;

public class Transaction
{
    public Transaction(ICommand command, DateOnly date)
    {
        Command = command;
        Date = date;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public DateOnly Date { get; }
    internal ICommand Command { get; }
}