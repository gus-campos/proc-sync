using ProcSync.Core.Domain;
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
        var indexGenerator = new SequenceGenerator<double>(0, last => last + 1);
        var producer = new SimpleProducer<double>(buffer, indexGenerator, produceTimeInMs, timeToCheckInMs);
        var consumer = new SimpleConsumer<double>(buffer, consumeTimeInMs, timeToCheckInMs);

        var simulator = new ProducerConsumerSimulator(producer, consumer);

        await simulator.Run(totalTimeInMs);
    }
}
