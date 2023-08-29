namespace Backups.Extra.Models;

public class FileSystemRepositoryJson : IRepositoryJson
{
    public void WriteText(string path, string text)
    {
        File.WriteAllText(path, text);
    }

    public string ReadText(string path)
    {
        return File.ReadAllText(path);
    }

    public bool IsFileExists(string path)
    {
        return File.Exists(path);
    }
}