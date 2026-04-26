using ProcSync.Core.Domain;
using ProcSync.Core.Interfaces;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class ProducerConsumerHandler
{
    public static void Run(int bufferSize, int totalItems)
    {
        Console.WriteLine($"Buffer: {bufferSize}");
        Console.WriteLine($"Items: {totalItems}");

        var buffer = new CircularBuffer<double>(bufferSize);

        IGenerator<double> generator = new SequenceGenerator<double>(0, last => last + 1);

        IProducer<double> producer = new LoggingProducer<double>(generator, 10);
        IConsumer<double> consumer = new LoggingConsumer<double>(10);

        var tester = new ProducerConsumerSimulator(buffer, producer, consumer);

        tester.Run(totalItems);
    }
}
