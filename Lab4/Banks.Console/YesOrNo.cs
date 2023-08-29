using Spectre.Console;

namespace Banks.Console;

public static class YesOrNo
{
    public static bool SpecifyPassport()
        => Ask("Will you fill in your passport details?");

    public static bool SpecifyAddress()
        => Ask("Will you fill out the address?");
    private static bool Ask(string message)
        => AnsiConsole
            .Prompt(
                new SelectionPrompt<bool> { Converter = value => value ? "[green]Yes[/]" : "[red]No[/]" }
                    .Title($"[yellow]{message}[/]")
                    .AddChoices(true, false));
}