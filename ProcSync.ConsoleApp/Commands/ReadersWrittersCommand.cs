using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class ReadersWrittersCommand
{
    public static Command Build()
    {
        Command command = new("readers-writters", "Simula leitura e escrita")
        {

        };

        command.SetAction(async (p) =>
        {
            PrintOptions.Print(p);

            var handler = new ReadersWrittersHandlers();
            await handler.Run();
        });

        return command;
    }
}
