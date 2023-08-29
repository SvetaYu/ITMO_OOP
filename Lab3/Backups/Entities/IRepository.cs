using Backups.Models;

namespace Backups.Entities;

public interface IRepository
{
    string Directory { get; }
    Stream OpenWrite(string path);
    IRepositoryObject GetObject(string path);
    void CreateDirectory(string name);
    void DeleteDirectory(string path);
    void Rename(string sourceDirName, string destDirName);
    bool DirectoryExists(string path);
}