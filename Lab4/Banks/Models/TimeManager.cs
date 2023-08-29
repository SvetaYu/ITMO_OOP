namespace Banks.Models;

public class TimeManager
{
    public TimeManager(DateOnly date)
    {
        Date = date;
    }

    public event EventHandler Changed;
    public DateOnly Date { get; private set; }

    public void AddDay()
    {
        Date = Date.AddDays(1);
        Changed?.Invoke(this, EventArgs.Empty);
    }
}