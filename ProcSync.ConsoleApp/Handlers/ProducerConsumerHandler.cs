
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
