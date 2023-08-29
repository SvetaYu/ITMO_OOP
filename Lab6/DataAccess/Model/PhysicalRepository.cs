
namespace DataAccess.Model;

public class PhysicalRepository : Repository
{
    public PhysicalRepository(string path)
    {
        Path = path;
    }
    public override string Path { get; set; }
    
    public override void CreateFileWithText(string path, string text)
    {
        string newPath = System.IO.Path.Combine(Path, path);
        File.WriteAllText(newPath, text);
    }

    public override void CreateDirectory(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        string path = System.IO.Path.Combine(Path, name);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}