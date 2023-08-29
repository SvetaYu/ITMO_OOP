using Banks.Models;

namespace Banks.Exceptions;

public class CentralBankException : Exception
{
    private CentralBankException(string message)
        : base(message) { }

    public static CentralBankException BankAlreadyExists()
    {
        return new CentralBankException("Bank already exists");
    }

    public static CentralBankException ClientAlreadyExists(string parameter)
    {
        return new CentralBankException($"Client with this {parameter} already exists");
    }

    public static CentralBankException AccountNotFound()
    {
        return new CentralBankException($"account not found");
    }

    public static CentralBankException InvalidPeriod()
    {
        return new CentralBankException($"the minimum period for opening an account is 1 month");
    }

    public static CentralBankException TransactionNotFound()
    {
        return new CentralBankException($"transaction not found");
    }
}