using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class DinningPhilosofersCommand
{
    public static Command Build()
    {
        Option<int> bufferSizeOption = new("--buffer-size")
        {
            Description = "Parâmetro bufferSize da simulação {default: 0}",
            DefaultValueFactory = _ => 10
        };

        Option<int> totalItemsOption = new("--total-items")
        {
            Description = "Parâmetro totalItems da simulação {default: 0}",
            DefaultValueFactory = _ => 1000
        };

        Command command = new("dinning-philosofers", "Simulação do problema dos filósofos")
        {
            bufferSizeOption,
            totalItemsOption
        };

        command.SetAction((p) =>
        {
            int bufferSize = p.GetValue(bufferSizeOption);
            int totalItems = p.GetValue(totalItemsOption);

            DinningPhilosofersHandler.Run(bufferSize, totalItems);
        });

        return command;
    }
}
