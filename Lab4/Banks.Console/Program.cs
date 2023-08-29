using Banks.Models;
using Banks.Services;

namespace Banks.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var timeManager = new TimeManager(new DateOnly(2022, 11, 20));
        var centralBank = new CentralBank(timeManager);
        Handler.Menu(centralBank);
    }
}