namespace DataAccess.Model;

public class PhoneMessageSource : MessageSource
{
    public PhoneMessageSource(string number)
    {
        Number = number;
    }
    protected PhoneMessageSource() { }
    public string Number { get; set; }
}