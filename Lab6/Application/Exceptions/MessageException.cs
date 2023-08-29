namespace Application.Exceptions;

public class MessageException : Exception
{
    private MessageException(string message)
        : base(message) { }

    public static MessageException InvalidMessageType()
    {
        return new MessageException("Invalid message type");
    }
}