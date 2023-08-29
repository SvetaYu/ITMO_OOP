namespace DataAccess.Model;

public class MessageResponse
{
    public MessageResponse(Message response, Message to, Employee employee)
    {
        Id = Guid.NewGuid();
        Response = response;
        To = to;
        Employee = employee;
    }
    protected MessageResponse() { }
    public Guid Id { get; set; }
    public virtual Message Response { get; set; }
    public virtual Message To { get; set; }
    public virtual Employee Employee { get; set; }
}