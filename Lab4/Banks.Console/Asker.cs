using System.Collections.Generic;
using Banks.Models;
using Spectre.Console;

namespace Banks.Console;

public static class Asker
{
    public static decimal AskDebitAccountInterest()
        => Ask<decimal>("What's the interest of the debit account?");

    public static decimal AskCreditAccountCommission()
        => Ask<decimal>("What's the commission of the credit account?");

    public static string AskName()
        => Ask<string>("What's the name?");

    public static string AskSurname()
        => Ask<string>("What's the surname?");

    public static string AskAddress()
        => Ask<string>("What's the address?");

    public static Guid AskClientsId()
        => Ask<Guid>("What's the client's Id?");

    public static Guid AskAccountsId()
        => Ask<Guid>("What's the account's Id?");

    public static decimal AskAmount()
        => Ask<decimal>("What's the amount?");

    public static decimal AskMaxAmountAvailableToUnconfirmedClients()
        => Ask<decimal>("What's the Max amount available to unconfirmed clients?");

    public static int AskPassportNumber()
        => Ask<int>("What's the passport number?");

    public static string AskBankName()
        => Ask<string>("What's the bank's name?");
    public static IEnumerable<DepositAccountInterest> AskDepositAccountInterests()
    {
        int count = Ask<int>("How many different interests for a deposit account?");
        var interests = new List<DepositAccountInterest>();
        for (int i = 1; i <= count; ++i)
        {
            AnsiConsole.Markup($"[yellow]{count}[/]");
            decimal minAmount = Ask<decimal>("What's min amount?");
            decimal interest = Ask<decimal>("What's interest?");
            interests.Add(new DepositAccountInterest(minAmount, interest));
        }

        return interests;
    }

    private static T Ask<T>(string message)
        => AnsiConsole.Prompt(
            new TextPrompt<T>($"[{Color.BlueViolet}]{message}[/]"));
}