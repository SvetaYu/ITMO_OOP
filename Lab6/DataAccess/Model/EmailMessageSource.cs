namespace DataAccess.Model;

public class EmailMessageSource : MessageSource
{
    public EmailMessageSource(string email)
    {
        Email = email;
    }
    protected EmailMessageSource() { }
    public string Email { get; set; }
}