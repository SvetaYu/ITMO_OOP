using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public static class Show
{
    public static void ShowClients(CentralBank centralBank)
    {
        if (centralBank.Clients.Count == 0)
        {
            AnsiConsole.Markup("[red]No clients[/]");
            return;
        }

        var table = new Table();
        table.AddColumns("[red]Name[/]", "[red]Surname[/]", "[red]Id[/]");
        foreach (Client client in centralBank.Clients)
        {
            table.AddRow($"[green]{client.Name}[/]", $"[green]{client.Surname}[/]", $"[green]{client.Id.ToString()}[/]");
        }

        AnsiConsole.Write(table.LeftAligned());
        AnsiConsole.WriteLine();
    }

    public static void ShowBanks(CentralBank centralBank)
    {
        if (centralBank.Clients.Count == 0)
        {
            AnsiConsole.Markup("[red]No banks[/]");
            return;
        }

        var table = new Table();
        table.AddColumns("[red]Name:[/]");
        foreach (Bank bank in centralBank.Banks)
        {
            table.AddRow($"[green]{bank.Name}[/]");
        }

        AnsiConsole.Write(table.LeftAligned());
        AnsiConsole.WriteLine();
    }

    public static void ShowConfiguration(CentralBank centralBank)
    {
        Bank bank = Getter.GetBank(centralBank);
        AnsiConsole.Markup(
            $"[purple]Credit account commission = [/][yellow]{bank.Config.CreditAccountCommission}p[/]\n");
        AnsiConsole.Markup($"[purple]Debit account interest = [/][yellow]{bank.Config.DebitAccountInterest}%[/]\n");
        AnsiConsole.Markup("[purple]DepositAccountInterests:[/]\n");
        var table = new Table();
        table.AddColumn("[red]min amount:[/]").Centered();
        table.AddColumn(new TableColumn("[red]interest:[/]").Centered());
        foreach (DepositAccountInterest interest in bank.Config.DepositAccountInterests)
        {
            table.AddRow($"[green]{interest.MinAmount}p[/]", $"[green]{interest.Interest}%[/]").Centered();
        }

        AnsiConsole.Write(table.LeftAligned());
        AnsiConsole.WriteLine();
    }

    public static void ShowAccounts(CentralBank centralBank)
    {
        Client client = centralBank.FindClient(Asker.AskClientsId());
        if (client is null)
        {
            AnsiConsole.Markup("[red]Client doesn't exist[/]");
            return;
        }

        if (client.Accounts.Count == 0)
        {
            AnsiConsole.Markup("[red]No accounts[/]");
            return;
        }

        var table = new Table();
        table.AddColumns("[red]Bank:[/]", "[red]Id:[/]", "[red]Amount:[/]");
        foreach (IAccount account in client.Accounts)
        {
            table.AddRow($"[green]{account.Bank}[/]", $"[green]{account.Id}[/]", $"[green]{account.Amount}[/]");
        }

        AnsiConsole.Write(table.LeftAligned());
        AnsiConsole.WriteLine();
    }
}