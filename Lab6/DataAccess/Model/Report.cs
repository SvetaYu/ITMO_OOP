namespace DataAccess.Model;

public class Report
{
    public Report(Guid id, Manager manager, string path)
    {
        Id = id;
        Date = DateOnly.FromDateTime(DateTime.Today);
        Manager = manager;
        Path = path;
    }

    protected Report() { }
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public virtual Manager Manager { get; set; }
    public string Path { get; set; }
}