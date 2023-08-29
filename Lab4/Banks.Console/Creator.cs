using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public static class Creator
{
    public static Client CreateClient()
    {
        IClientBuilder client = Client.Builder.SetName(Asker.AskName()).SetSurname(Asker.AskSurname());
        if (YesOrNo.SpecifyPassport())
        {
            client.SetPassport(Asker.AskPassportNumber());
        }

        if (YesOrNo.SpecifyAddress())
        {
            client.SetAddress(Asker.AskAddress());
        }

        return client.Build();
    }

    public static void CreateBank(CentralBank centralBank)
    {
        string name = Asker.AskName();
        Configuration config = CreateConfig();
        centralBank.CreateBank(name, config);
    }

    public static Guid CreateAccount(CentralBank centralBank)
    {
        string type = Choice.ChoosingAnAccountType();
        Bank bank = Getter.GetBank(centralBank);
        Client client = Getter.GetClient(centralBank);
        decimal amount = Asker.AskAmount();

        switch (type)
        {
            case "[yellow]Credit account[/]":
            {
                return centralBank.OpenCreditAccount(bank, client, amount);
            }

            case "[purple]Debit account[/]":
            {
                return centralBank.OpenDebitAccount(bank, client, amount);
            }

            case "[red]Deposit account[/]":
            {
                int period =
                    AnsiConsole.Prompt(new TextPrompt<int>($"[{Color.BlueViolet}]On what period? (in month)[/]"));
                return centralBank.OpenDepositAccount(bank, client, amount, period);
            }
        }

        throw new InvalidOperationException();
    }

    public static Configuration CreateConfig()
    {
        decimal debitAccountInterest = Asker.AskDebitAccountInterest();
        decimal creditAccountCommission = Asker.AskCreditAccountCommission();
        IEnumerable<DepositAccountInterest> depositAccountInterests = Asker.AskDepositAccountInterests();
        decimal maxAmountAvailableToUnconfirmedClients = Asker.AskMaxAmountAvailableToUnconfirmedClients();
        return new Configuration(debitAccountInterest, creditAccountCommission, depositAccountInterests, maxAmountAvailableToUnconfirmedClients);
    }
}