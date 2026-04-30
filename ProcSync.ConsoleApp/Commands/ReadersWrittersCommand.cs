using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class ReadersWrittersCommand
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

        Command command = new("readers-writters", "Simula leitura e escrita")
        {
            bufferSizeOption,
            totalItemsOption
        };

        command.SetAction(async (p) =>
        {
            int bufferSize = p.GetValue(bufferSizeOption);
            int totalItems = p.GetValue(totalItemsOption);

            var handler = new ReadersWrittersHandlers();

            await handler.Run();
        });

        return command;
    }
}
