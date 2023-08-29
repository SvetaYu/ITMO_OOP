namespace Backups.Extra.Models;

public class FIleLogger : ILogger
{
    private readonly string _path;

    public FIleLogger(string path)
    {
        this._path = path;
    }

    public void Log(string message)
    {
        File.WriteAllText(_path, message);
    }
}