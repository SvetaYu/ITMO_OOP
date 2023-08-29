using Banks.Entities;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public static class Handler
{
 public static void ChangeConfiguration(CentralBank centralBank)
    {
        Configuration config = Getter.GetBank(centralBank).Config;
        switch (Choice.ChoosingConfigurationField())
        {
            case "[yellow]Debit account interest[/]":
            {
                config.ChangeDebitAccountInterest(Asker.AskDebitAccountInterest());
                break;
            }

            case "[purple]Credit account commission[/]":
            {
                config.ChangeCreditAccountInterest(Asker.AskCreditAccountCommission());
                break;
            }

            case "[red]Deposit account interests[/]":
            {
                config.ChangeDepositAccountInterest(Asker.AskDepositAccountInterests());
                break;
            }
        }
    }

 public static void MakeTransaction(CentralBank centralBank)
    {
        switch (Choice.ChoosingTransaction())
        {
            case "[yellow]Top up the balance[/]":
            {
                Guid accountId = Asker.AskAccountsId();
                decimal amount = Asker.AskAmount();
                centralBank.TopUpAccount(accountId, amount);
                break;
            }

            case "[purple]Transfer money[/]":
            {
                Guid accountId = Asker.AskAccountsId();
                decimal amount = Asker.AskAmount();
                centralBank.WithdrawalMoney(accountId, amount);
                break;
            }

            case "[red]Withdraw cash[/]":
            {
                AnsiConsole.Markup($"[blue]From:[/]");
                Guid fromId = Asker.AskAccountsId();
                AnsiConsole.Markup($"[blue]To:[/]");
                Guid toId = Asker.AskAccountsId();
                decimal amount = Asker.AskAmount();
                centralBank.TransferMoney(fromId, toId, amount);
                break;
            }
        }
    }

 public static void Menu(CentralBank centralBank)
    {
        switch (Choice.ChoosingTheInitialAction())
        {
            case "[green]Create client[/]":
            {
                Guid id = centralBank.AddClient(Creator.CreateClient());
                AnsiConsole.Markup($"\n[purple]New client id: {id}[/]\n");
                Menu(centralBank);
                break;
            }

            case "[green]Create bank[/]":
            {
                Creator.CreateBank(centralBank);
                Menu(centralBank);
                break;
            }

            case "[green]Create account[/]":
            {
                Guid id = Creator.CreateAccount(centralBank);
                AnsiConsole.Markup($"\n[purple]New account id: {id}[/]\n");
                Menu(centralBank);
                break;
            }

            case "[green]Make a transaction[/]":
            {
                MakeTransaction(centralBank);
                Menu(centralBank);
                break;
            }

            case "[yellow]Show Clients[/]":
            {
                Show.ShowClients(centralBank);
                Menu(centralBank);
                break;
            }

            case "[yellow]Show balance[/]":
            {
                Guid accountId = Asker.AskAccountsId();
                centralBank.ShowBalance(accountId);
                break;
            }

            case "[yellow]Show banks[/]":
            {
                Show.ShowBanks(centralBank);
                Menu(centralBank);
                break;
            }

            case "[yellow]Show the bank configuration[/]":
            {
                Show.ShowConfiguration(centralBank);
                Menu(centralBank);
                break;
            }

            case "[purple]Change the bank configuration[/]":
            {
                ChangeConfiguration(centralBank);
                Menu(centralBank);
                break;
            }

            case "[red]Delete bank[/]":
            {
                centralBank.RemoveBank(centralBank.FindBank(Asker.AskName()));
                Menu(centralBank);
                break;
            }

            case "[red]Delete client[/]":
            {
                Guid id = Asker.AskClientsId();
                centralBank.RemoveClient(centralBank.FindClient(id));
                Menu(centralBank);
                break;
            }

            case "[red]Delete account[/]":
            {
                Guid id = Asker.AskAccountsId();
                centralBank.CloseAccount(id);
                Menu(centralBank);
                break;
            }

            case "[red]exit[/]":
            {
                break;
            }
        }
    }
}