using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class OneWayBridgeCommand
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

        Command command = new("one-way-bridge", "Simulação de ponte de mão única")
        {
            bufferSizeOption,
            totalItemsOption
        };

        command.SetAction((p) =>
        {
            int bufferSize = p.GetValue(bufferSizeOption);
            int totalItems = p.GetValue(totalItemsOption);

            OneWayBridgeHandler.Run(bufferSize, totalItems);
        });

        return command;
    }
}
