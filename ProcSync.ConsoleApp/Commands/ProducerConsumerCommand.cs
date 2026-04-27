using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class ProducerConsumerCommand
{
    public static Command Build()
    {
        Option<int> bufferSizeOption = new("--buffer-size")
        {
            DefaultValueFactory = _ => 100
        };

        Option<int> totalTimeOption = new("--total-time")
        {
            DefaultValueFactory = _ => 100
        };

        Option<int> checkTimeOption = new("--check-time")
        {
            DefaultValueFactory = _ => 10
        };


        Option<int> produceTimeOption = new("--produce-time")
        {
            DefaultValueFactory = _ => 0
        };


        Option<int> consumeTimeOption = new("--consume-time")
        {
            DefaultValueFactory = _ => 0
        };

        Command producerConsumerCommand = new(
            "producer-consumer",
            "Simulação de produtor/consumidor"
        )
        {
            bufferSizeOption,
            totalTimeOption,
            checkTimeOption,
            produceTimeOption,
            consumeTimeOption
        };

        producerConsumerCommand.SetAction(async (p) =>
        {
            await ProducerConsumerHandler.Run(
                p.GetValue(bufferSizeOption),
                p.GetValue(totalTimeOption),
                p.GetValue(checkTimeOption),
                p.GetValue(produceTimeOption),
                p.GetValue(consumeTimeOption)
            );
        });

        return producerConsumerCommand;
    }
}
