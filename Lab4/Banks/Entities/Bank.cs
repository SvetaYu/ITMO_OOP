using Banks.Exceptions;

namespace Banks.Entities;

public class Bank
{
    private readonly List<IAccount> _accounts = new List<IAccount>();
    internal Bank(string name, Configuration config)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw BankException.InvalidName();
        }

        Name = name;
        Config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public IReadOnlyCollection<IAccount> Accounts => _accounts;
    public Configuration Config { get; }
    public string Name { get; }

    public override string ToString()
    {
        return Name;
    }

    internal void AddAccount(IAccount account)
    {
        _accounts.Add(account);
    }

    internal void RemoveAccount(IAccount account)
    {
        _accounts.Remove(account);
    }

    internal void NotifyOfDateChange(DateOnly newDate)
    {
        foreach (IAccount account in _accounts)
        {
            account.AccrueInterest(newDate);
        }
    }
}