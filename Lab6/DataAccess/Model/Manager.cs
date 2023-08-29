namespace DataAccess.Model;

public class Manager : Employee
{
    public Manager(string name, Guid id, ICollection<Employee> employees): base(name, id)
    {
        Subordinates = employees;
    }
    protected Manager() { }
    public virtual ICollection<Employee> Subordinates { get; set; }
}