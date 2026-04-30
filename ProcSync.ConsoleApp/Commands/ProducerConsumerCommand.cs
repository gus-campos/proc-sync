using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class ProducerConsumerCommand
{
    public static Command Build()
    {
        Option<int> bufferSizeOption = new("--buffer-size")
        {
            Description = "Tamanho do buffer compartilhado",
            DefaultValueFactory = _ => 100
        };

        Option<int> totalTimeOption = new("--total-time")
        {
            Description = "Tempo total da simulação (ms)",
            DefaultValueFactory = _ => 100
        };

        Option<int> checkTimeOption = new("--check-time")
        {
            Description = "Intervalo de checagem (ms)",
            DefaultValueFactory = _ => 10
        };

        Option<int> produceTimeOption = new("--produce-time")
        {
            Description = "Tempo para produzir um item (ms)",
            DefaultValueFactory = _ => 0
        };

        Option<int> consumeTimeOption = new("--consume-time")
        {
            Description = "Tempo para consumir um item (ms)",
            DefaultValueFactory = _ => 0
        };

        Command producerConsumerCommand = new(
            "producer-consumer",
            "Simula produção e consumo"
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
            PrintOptions.Print(p);

            var handler = new ProducerConsumerHandler(
                p.GetValue(bufferSizeOption),
                p.GetValue(totalTimeOption),
                p.GetValue(checkTimeOption),
                p.GetValue(produceTimeOption),
                p.GetValue(consumeTimeOption)
            );

            await handler.Run();
        });

        return producerConsumerCommand;
    }
}
