namespace DataAccess.Model;

public class Employee
{
    public Employee(string name, Guid id)
    {
        Name = name;
        Id = id;
    }
    protected Employee() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid? AccountId { get; set; }
    public virtual Account Account { get; set; }
}