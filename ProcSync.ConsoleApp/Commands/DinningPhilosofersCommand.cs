using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class DinningPhilosofersCommand
{
    public static Command Build()
    {
        Command command = new("dinning-philosofers", "Simulação do problema dos filósofos")
        {

        };

        command.SetAction(async (p) =>
        {
            PrintOptions.Print(p);

            var handler = new DinningPhilosofersHandler();
            await handler.Run();
        });

        return command;
    }
}
