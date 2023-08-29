using DataAccess.Model;

namespace Application.Exceptions;

public class MessageSourceException : Exception
{
    private MessageSourceException(string message)
        : base(message) { }

    public static MessageSourceException MessageSourceNotFound(Guid id)
    {
        return new MessageSourceException($"Message source {id} not found");
    }
    public static MessageSourceException MessageSourceNotFound(string name)
    {
        return new MessageSourceException($"Message source {name} not found");
    }
}