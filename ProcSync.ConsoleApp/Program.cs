using ProcSync.Core.Domain;
using ProcSync.Core.Interfaces;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp;

public static class Program
{
    public static void Main(string[] args)
    {
        TestCounter();
        // TestProducerConsumer();
    }

    private static void TestProducerConsumer()
    {
        var buffer = new CircularBuffer<double>(size: 10);

        IGenerator<double> generator = new SequenceGenerator<double>(
            initialItem: 0,
            generator: (lastValue) => lastValue + 1
        );

        IProducer<double> producer = new LoggingProducer<double>(
            generator,
            delayInMiliseconds: 10
        );

        IConsumer<double> consumer = new LoggingConsumer<double>(
            delayInMiliseconds: 10
        );

        var tester = new ProducerConsumerSimulator(
            buffer,
            producer,
            consumer
        );

        tester.Run(totalItemsAmount: 100);

        // Para size 300 e steps 10 mi
        // consumer para em 9999399 (pois ficou vazio?)
        // producer para em 9999951 (pois ficou lotado?) 
    }

    private static void TestCounter()
    {
        // Simple counter - Increment
        {
            var counter = new SimpleCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrement(stepsAmount: 1000);
        }

        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrementAndDecrement(stepsAmount: 1000);
        }

        // Concurrent counter - Increment
        {
            var counter = new ConcurrentCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrement(stepsAmount: 1000);
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new ConcurrentCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrementAndDecrement(stepsAmount: 1000);
        }
    }
}

