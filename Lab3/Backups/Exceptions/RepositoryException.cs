namespace Backups.Exceptions;

public class RepositoryException : Exception
{
    private RepositoryException(string massage)
        : base(massage)
    {
    }

    public static RepositoryException FileOrDirectoryDoesntExist(string path)
    {
        return new RepositoryException($"Path doesn't exist: {path}");
    }

    public static RepositoryException NotValidExtension()
    {
        return new RepositoryException("not valid extension");
    }
}