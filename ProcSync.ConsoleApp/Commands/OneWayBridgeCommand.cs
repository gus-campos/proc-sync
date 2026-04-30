using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class OneWayBridgeCommand
{
    public static Command Build()
    {
        Command command = new("one-way-bridge", "Simula de ponte de mão única")
        {

        };

        command.SetAction(async (p) =>
        {
            PrintOptions.Print(p);

            var handler = new OneWayBridgeHandler();
            await handler.Run();
        });

        return command;
    }
}
