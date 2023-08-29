namespace Backups.Exceptions;

public class ZipStorageException : Exception
{
    private ZipStorageException(string massage)
        : base(massage) { }
    public static ZipStorageException NotValidExtension()
    {
        return new ZipStorageException("not valid extension");
    }
}