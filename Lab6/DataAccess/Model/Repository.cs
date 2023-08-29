namespace DataAccess.Model;

public abstract class Repository
{
   public abstract string Path { get; set; }
   public abstract void CreateFileWithText(string path, string text);
   public abstract void CreateDirectory(string name);
}