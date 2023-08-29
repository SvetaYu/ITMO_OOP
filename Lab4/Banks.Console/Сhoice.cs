using Spectre.Console;

namespace Banks.Console;

public static class Choice
{
    public static string ChoosingConfigurationField()
        => AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the configuration field")
                .AddChoices(
                    "[yellow]Debit account interest[/]",
                    "[purple]Credit account commission[/]",
                    "[red]Deposit account interests[/]"));

    public static string ChoosingAnAccountType()
        => AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the account type")
                .AddChoices(
                    "[yellow]Credit account[/]",
                    "[purple]Debit account[/]",
                    "[red]Deposit account[/]"));

    public static string ChoosingTransaction()
        => AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the transaction")
                .AddChoices(
                    "[yellow]Top up the balance[/]",
                    "[purple]Transfer money[/]",
                    "[red]Withdraw cash[/]"));
    public static string ChoosingTheInitialAction()
        => AnsiConsole.Prompt(
            new SelectionPrompt<string>()

                .Title($"[{Color.BlueViolet}]What do you want to do?[/]")
                .AddChoices(
                    "[green]Create client[/]",
                    "[green]Create bank[/]",
                    "[green]Create account[/]",
                    "[green]Make a transaction[/]",
                    "[purple]Change the bank configuration[/]",
                    "[yellow]Show the bank configuration[/]",
                    "[yellow]Show Clients[/]",
                    "[yellow]Show banks[/]",
                    "[red]Delete client[/]",
                    "[red]Delete bank[/]",
                    "[red]Delete account[/]",
                    "[red]exit[/]"));
}