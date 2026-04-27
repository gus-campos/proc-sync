using ProcSync.Core.Domain;
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class ProducerConsumerHandler()
{
    async public static Task Run(
        int bufferSize,
        int totalTimeInMs,
        int timeToCheckInMs,
        int produceTimeInMs,
        int consumeTimeInMs
    )
    {
        Console.WriteLine($"\nTamanho do buffer: {bufferSize}");
        Console.WriteLine($"Tempo total: {totalTimeInMs / 1000.0} s");
        Console.WriteLine($"Tempo pra checagem: {totalTimeInMs}");
        Console.WriteLine($"Tempo para produzir: {produceTimeInMs}");
        Console.WriteLine($"Tempo para consumir: {consumeTimeInMs}\n");

        var buffer = new CircularBuffer<double>(bufferSize);

        // Producers
        var indexGenerator = new SequenceGenerator<double>(0, last => last + 1);
        var producers = Enumerable.Range(0, 1).Select(
            _ => new Producer<double>(buffer, indexGenerator, produceTimeInMs, timeToCheckInMs)
        );

        // Consumers
        var consumers = Enumerable.Range(0, 1).Select(
            _ => new Consumer<double>(buffer, consumeTimeInMs, timeToCheckInMs)
        );

        var simulator = new ProducerConsumerSimulator(producers, consumers);

        await simulator.Run(totalTimeInMs);
    }
}
