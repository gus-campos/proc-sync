using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Cli.Commands;

public static class ProducerConsumerCommand
{
    public static Command Build()
    {
        Option<int> bufferSizeOption = new("--buffer-size")
        {
            DefaultValueFactory = _ => 100
        };

        Option<int> itemsOption = new("--items-amount")
        {
            DefaultValueFactory = _ => 1000
        };

        Command producerConsumerCommand = new("producer-consumer", "Simulação produtor/consumidor")
        {
            bufferSizeOption,
            itemsOption
        };

        producerConsumerCommand.SetAction(p =>
        {
            ProducerConsumerHandler.Run(
                p.GetValue(bufferSizeOption),
                p.GetValue(itemsOption)
            );
        });

        return producerConsumerCommand;
    }
}
