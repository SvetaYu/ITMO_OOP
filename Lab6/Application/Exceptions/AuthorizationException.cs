namespace Application.Exceptions;

public class AuthorizationException : Exception
{
    private AuthorizationException(string message)
        : base(message) { }

    public static AuthorizationException AccountNotFound()
    {
        return new AuthorizationException("Invalid Login or password");
    }
}