namespace ProcSync.ConsoleApp;

using ProcSync.Core.CounterProblem;
using ProcSync.Core.CounterProblem.Counter;
using ProcSync.Core.ProducerConsumerProblem;
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;
using ProcSync.Core.ProducerConsumerProblem.Consumer;
using ProcSync.Core.ProducerConsumerProblem.Producer;

public static class Program
{
    public static void Main(string[] args)
    {
        // TestCounter();
        TestProducerConsumer();
    }

    private static void TestProducerConsumer()
    {
        var buffer = new CircularBuffer<double>(size: 10);

        IGenerator<double> generator = new SequenceGenerator<double>(
            initialItem: 0,
            generator: (lastValue) => lastValue + 1
        );

        IProducer<double> producer = new GeneratorProducerWithLogger<double>(
            generator,
            delayInMiliseconds: 10
        );

        IConsumer<double> consumer = new ConsumerWithLogger<double>(
            delayInMiliseconds: 10
        );

        var tester = new ProducerConsumerTester(
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
            var tester = new ConcurrentCountingTester(counter);
            tester.RunIncrement(stepsAmount: 1000);
        }

        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            var tester = new ConcurrentCountingTester(counter);
            tester.RunIncrementAndDecrement(stepsAmount: 1000);
        }

        // Concurrent counter - Increment
        {
            var counter = new ConcurrentCounter(new SimpleCounter());
            var tester = new ConcurrentCountingTester(counter);
            tester.RunIncrement(stepsAmount: 1000);
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new ConcurrentCounter(new SimpleCounter());
            var tester = new ConcurrentCountingTester(counter);
            tester.RunIncrementAndDecrement(stepsAmount: 1000);
        }
    }
}

