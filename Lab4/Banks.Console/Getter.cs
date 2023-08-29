using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public static class Getter
{
    public static Bank GetBank(CentralBank centralBank)
        => centralBank.FindBank(Asker.AskBankName());

    public static Client GetClient(CentralBank centralBank)
    {
        return centralBank.FindClient(Asker.AskClientsId());
    }
}