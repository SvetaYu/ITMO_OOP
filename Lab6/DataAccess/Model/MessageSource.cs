namespace DataAccess.Model;

public abstract class MessageSource
{
    public MessageSource()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
}