
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class ProducerConsumerHandler(
    int bufferSize,
    int totalTimeInMs,
    int timeToCheckInMs,
    int produceTimeInMs,
    int consumeTimeInMs
) : BaseHandler
{
    protected override void PrintParams()
    {
        Console.WriteLine($"\nTamanho do buffer: {bufferSize}");
        Console.WriteLine($"Tempo total: {totalTimeInMs / 1000.0} s");
        Console.WriteLine($"Tempo pra checagem: {totalTimeInMs / 1000.0} s");
        Console.WriteLine($"Tempo para produzir: {produceTimeInMs / 1000.0} s");
        Console.WriteLine($"Tempo para consumir: {consumeTimeInMs / 1000.0} s");
    }

    protected override async Task RunSimple()
    {
        var buffer = new ConcurrentCircularBuffer<double>(bufferSize);

        var indexGenerator = new ConcurrentSequenceGenerator<double>(0, last => last + 1);
        var producers = Enumerable.Range(0, 1).Select(
            _ => new ConcurrentProducer<double>(buffer, indexGenerator, produceTimeInMs, timeToCheckInMs)
        );

        var consumers = Enumerable.Range(0, 1).Select(
            _ => new ConcurrentConsumer<double>(buffer, consumeTimeInMs, timeToCheckInMs)
        );

        var simulator = new ProducerConsumerSimulator(producers, consumers);
        await simulator.Run(totalTimeInMs);
    }

    protected override async Task RunConcurrent()
    {
        var buffer = new CircularBuffer<double>(bufferSize);

        var indexGenerator = new ConcurrentSequenceGenerator<double>(0, last => last + 1);
        var producers = Enumerable.Range(0, 1).Select(
            _ => new ConcurrentProducer<double>(buffer, indexGenerator, produceTimeInMs, timeToCheckInMs)
        );

        var consumers = Enumerable.Range(0, 1).Select(
            _ => new ConcurrentConsumer<double>(buffer, consumeTimeInMs, timeToCheckInMs)
        );

        var simulator = new ProducerConsumerSimulator(producers, consumers);
        await simulator.Run(totalTimeInMs);
    }
}
