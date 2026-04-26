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

        Option<int> totalTime = new("--total-time")
        {
            DefaultValueFactory = _ => 500
        };

        Option<int> checkTime = new("--check-time")
        {
            DefaultValueFactory = _ => 10
        };


        Option<int> produceTime = new("--produce-time")
        {
            DefaultValueFactory = _ => 10
        };


        Option<int> consumeTime = new("--consume-time")
        {
            DefaultValueFactory = _ => 10
        };

        Command producerConsumerCommand = new("producer-consumer", "Simulação produtor/consumidor")
        {
            bufferSizeOption,
            totalTime,
            checkTime,
            produceTime,
            consumeTime
        };

        producerConsumerCommand.SetAction(async (p) =>
        {
            await ProducerConsumerHandler.Run(
                p.GetValue(bufferSizeOption),
                p.GetValue(totalTime),
                p.GetValue(checkTime),
                p.GetValue(produceTime),
                p.GetValue(consumeTime)
            );
        });

        return producerConsumerCommand;
    }
}
