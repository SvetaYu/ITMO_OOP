using Banks.Entities;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private readonly List<Client> _clients = new List<Client>();
    private readonly List<Transaction> _history = new List<Transaction>();
    private readonly TimeManager _timeManager;

    public CentralBank(TimeManager timeManager)
    {
        _timeManager = timeManager;
        timeManager.Changed += (sender, args) =>
        {
            foreach (Bank bank in _banks)
            {
                bank.NotifyOfDateChange(timeManager.Date);
            }
        };
    }

    public IReadOnlyCollection<Bank> Banks => _banks;
    public IReadOnlyCollection<Client> Clients => _clients;

    public Bank CreateBank(string name, Configuration config)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(name);
        if (FindBank(name) is not null)
        {
            throw CentralBankException.BankAlreadyExists();
        }

        _banks.Add(new Bank(name, config));
        return _banks.Last();
    }

    public Guid AddClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        if (_clients.Contains(client))
        {
            throw CentralBankException.ClientAlreadyExists("id");
        }

        if (FindClient(client.PassportNumber) is not null)
        {
            throw CentralBankException.ClientAlreadyExists("passport number");
        }

        _clients.Add(client);
        return client.Id;
    }

    public Guid OpenCreditAccount(Bank bank, Client client, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        if (amount < 0)
        {
            throw MoneyException.LessThanZero();
        }

        var account = new CreditAccount(bank, client, amount);
        bank.AddAccount(account);
        client.AddAccount(account);
        client.SubscribeToBankNotifications(bank);
        return account.Id;
    }

    public Guid OpenDepositAccount(Bank bank, Client client, decimal amount, int periodInMonth)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        if (amount < 0)
        {
            throw MoneyException.LessThanZero();
        }

        if (periodInMonth <= 0)
        {
            throw CentralBankException.InvalidPeriod();
        }

        decimal interest = bank.Config.GetDepositIntersect(amount);
        var account = new DepositAccount(periodInMonth, bank, client, amount, interest, _timeManager.Date);
        bank.AddAccount(account);
        client.AddAccount(account);
        client.SubscribeToBankNotifications(bank);
        return account.Id;
    }

    public Guid OpenDebitAccount(Bank bank, Client client, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        if (amount < 0)
        {
            throw MoneyException.LessThanZero();
        }

        var account = new DebitAccount(bank, client, amount, _timeManager.Date);
        bank.AddAccount(account);
        client.AddAccount(account);
        client.SubscribeToBankNotifications(bank);
        return account.Id;
    }

    public Guid TransferMoney(Guid fromId, Guid toId, decimal value)
    {
        IAccount from = FindAccount(fromId);
        IAccount to = FindAccount(toId);

        if (from is null || to is null)
        {
            throw CentralBankException.AccountNotFound();
        }

        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        var context = new CommandContext(from, to, value);
        var transaction = new TransferMoney(context);
        transaction.Execute();
        _history.Add(new Transaction(transaction, _timeManager.Date));
        return _history.Last().Id;
    }

    public Guid WithdrawalMoney(Guid accountId, decimal value)
    {
        IAccount account = FindAccount(accountId);

        if (account is null)
        {
            throw CentralBankException.AccountNotFound();
        }

        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        var context = new CommandContext(account, null, value);
        var transaction = new WithdrawalWithCommission(context);
        transaction.Execute();
        _history.Add(new Transaction(transaction, _timeManager.Date));
        return _history.Last().Id;
    }

    public Guid TopUpAccount(Guid accountId, decimal value)
    {
        IAccount account = FindAccount(accountId);
        if (account is null)
        {
            throw CentralBankException.AccountNotFound();
        }

        if (value < 0)
        {
            throw MoneyException.LessThanZero();
        }

        var context = new CommandContext(null, account, value);
        var transaction = new TopUp(context);
        transaction.Execute();
        _history.Add(new Transaction(transaction, _timeManager.Date));
        return _history.Last().Id;
    }

    public decimal ShowBalance(Guid accountId)
    {
        return FindAccount(accountId).Amount;
    }

    public Bank FindBank(string name)
    {
        return _banks.FirstOrDefault(bank => bank.Name.Equals(name));
    }

    public Client FindClient(Guid id)
    {
        return _clients.FirstOrDefault(client => client.Id.Equals(id));
    }

    public void CancelTransaction(Guid id)
    {
        Transaction transaction = FindTransaction(id);
        if (transaction is null)
        {
            throw CentralBankException.TransactionNotFound();
        }

        transaction.Command.UnDo();
    }

    public void CloseAccount(Guid id)
    {
        IAccount account = _banks.SelectMany(bank => bank.Accounts).FirstOrDefault(account => account.Id.Equals(id));
        if (account is null)
        {
            throw new Exception();
        }

        account.Bank.RemoveAccount(account);
    }

    public void RemoveBank(Bank bank)
    {
        _banks.Remove(bank);
    }

    public void RemoveClient(Client client)
    {
        _clients.Remove(client);
    }

    private IAccount FindAccount(Guid id)
    {
        return _banks.SelectMany(bank => bank.Accounts).FirstOrDefault(account => account.Id.Equals(id));
    }

    private Transaction FindTransaction(Guid id)
    {
        return _history.FirstOrDefault(transaction => transaction.Id.Equals(id));
    }

    private Client FindClient(int? passportNumber)
    {
        return _clients.FirstOrDefault(client => client.PassportNumber.Equals(passportNumber));
    }
}