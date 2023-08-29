namespace DataAccess.Model;

public class Account
{
    public Account(Guid id, string login, string password, Employee employee)
    {
        Id = id;
        Login = login;
        Password = password;
        Employee = employee;
    }

    protected Account() { }

    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }
    
    public virtual Employee Employee { get; set; }
}