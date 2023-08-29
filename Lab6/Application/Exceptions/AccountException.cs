using DataAccess.Model;

namespace Application.Exceptions;

public class AccountException : Exception
{
    private AccountException(string message)
        : base(message) { }

    public static AccountException AccountAlreadyExists(string login)
    {
        return new AccountException($"Account with login: {login} already exists");
    }
}