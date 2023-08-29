namespace Application.Exceptions;

public class DbSetException: Exception
{
    private DbSetException(string message)
        : base(message) { }

    public static DbSetException ObjectNotFound(Type type, Guid id)
    {
        return new DbSetException($"{type} with id {id} not found");
    }
}