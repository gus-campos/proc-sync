using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class SleepingBarberCommand
{
    public static Command Build()
    {
        Option<int> bufferSizeOption = new("--buffer-size")
        {
            Description = "Parâmetro bufferSize da simulação {default: 0}",
            DefaultValueFactory = _ => 0
        };

        Option<int> totalItemsOption = new("--total-items")
        {
            Description = "Parâmetro totalItems da simulação {default: 0}",
            DefaultValueFactory = _ => 0
        };

        Command command = new("sleeping-barber", "Simula problema do barbeiro sonolento")
        {
            bufferSizeOption,
            totalItemsOption
        };

        command.SetAction((p) =>
        {
            int bufferSize = p.GetValue(bufferSizeOption);
            int totalItems = p.GetValue(totalItemsOption);

            SleepingBarberHandler.Run(bufferSize, totalItems);
        });

        return command;
    }
}
