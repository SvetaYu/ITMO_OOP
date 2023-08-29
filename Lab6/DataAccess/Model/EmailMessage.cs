using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Model;

public class EmailMessage : Message
{
    public EmailMessage(MessageSource source, string subject, string text, MessageState state ) : base(source, state)
    {
        Subject = subject;
        Text = text;
    }

    protected EmailMessage() { }
    public string Subject { get; set; }
    public string Text { get; set; }
}