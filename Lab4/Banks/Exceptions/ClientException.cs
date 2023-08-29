using Banks.Models;

namespace Banks.Exceptions;

public class ClientException : Exception
{
    private ClientException(string message)
        : base(message) { }

    public static ClientException InvalidName()
    {
        return new ClientException("invalid name");
    }

    public static ClientException InvalidSurname()
    {
        return new ClientException("invalid surname");
    }

    public static ClientException InvalidAddress()
    {
        return new ClientException("invalid address");
    }

    public static ClientException InvalidPassportNumber()
    {
        return new ClientException("invalid passport number");
    }
}