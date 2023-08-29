namespace DataAccess.Model;

public class PhoneMessage : Message
{
    public PhoneMessage(MessageSource source, string text, MessageState state) : base(source, state)
    {
        Text = text;
    }

    protected PhoneMessage() { }
    public string Text { get; set; }
}