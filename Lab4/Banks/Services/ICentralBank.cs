using Banks.Entities;

namespace Banks.Services;

public interface ICentralBank
{
    IReadOnlyCollection<Bank> Banks { get; }
    IReadOnlyCollection<Client> Clients { get; }
    Bank CreateBank(string name, Configuration config);

    Guid AddClient(Client client);

    Guid OpenCreditAccount(Bank bank, Client client, decimal amount);

    Guid OpenDepositAccount(Bank bank, Client client, decimal amount, int periodInMonth);

    Guid OpenDebitAccount(Bank bank, Client client, decimal amount);

    Guid TransferMoney(Guid fromId, Guid toId, decimal value);

    Guid WithdrawalMoney(Guid accountId, decimal value);

    Guid TopUpAccount(Guid accountId, decimal value);

    Bank FindBank(string name);
    Client FindClient(Guid id);

    void CancelTransaction(Guid id);
    void CloseAccount(Guid id);

    void RemoveBank(Bank bank);

    void RemoveClient(Client client);
}