namespace Backups.Extra.Models;

public interface IRepositoryJson
{
    void WriteText(string path, string text);
    string ReadText(string path);
    bool IsFileExists(string path);
}