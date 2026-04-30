using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class SleepingBarberCommand
{
    public static Command Build()
    {
        Command command = new("sleeping-barber", "Simula problema do barbeiro sonolento")
        {

        };

        command.SetAction(async (p) =>
        {
            PrintOptions.Print(p);

            var handler = new SleepingBarberHandler();
            await handler.Run();
        });

        return command;
    }
}
