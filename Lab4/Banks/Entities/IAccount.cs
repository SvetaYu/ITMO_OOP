namespace Banks.Entities;

public interface IAccount
{
    Client Client { get; }
    Bank Bank { get; }
    decimal Amount { get; }
    Guid Id { get; }
    decimal MaxAmountAvailableToUnconfirmedClients { get; }
    void TopUp(decimal value);
    void Withdraw(decimal value);
    void AccrueInterest(DateOnly newDate);
    decimal CommissionDeduction();
}