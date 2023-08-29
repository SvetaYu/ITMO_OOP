namespace Isu.Exceptions;

public class ObjectNotFoundException : InvalidOperationException
{
    public ObjectNotFoundException() { }
    public ObjectNotFoundException(string message)
        : base(message) { }
    public ObjectNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}