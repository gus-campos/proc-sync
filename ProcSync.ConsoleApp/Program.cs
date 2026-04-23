namespace ProcSync.ConsoleApp;

using ProcSync.Core.CounterProblem;
using ProcSync.Core.CounterProblem.Counter;
using ProcSync.Core.ProducerConsumerProblem;
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

public static class Program
{
    public static void Main(string[] args)
    {
        TestCounter();
        // TestProcuctionConsumer();
    }

    private static void TestProcuctionConsumer()
    {
        var buffer = new CircularBuffer<double>(size: 10);

        ProducerConsumerTester.RunTest(
            buffer,
            totalItemsAmount: 1000,
            consumptionDelayInMs: 1,
            productionDelayInMs: 0
        );

        // Para size 300 e steps 10 mi
        // consumer para em 9999399 (pois ficou vazio?)
        // producer para em 9999951 (pois ficou lotado?) 
    }

    private static void TestCounter()
    {
        // Simple counter - Increment
        {
            var counter = new SimpleCounter();
            ConcurrentCountingTester.RunIncrement(counter, stepsAmount: 1000);
        }

        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            ConcurrentCountingTester.RunIncrementAndDecrement(counter, stepsAmount: 1000);
        }

        // Concurrent counter - Increment
        {
            var counter = new ConcurrentCounter(new SimpleCounter());
            ConcurrentCountingTester.RunIncrement(counter, stepsAmount: 1000);
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new ConcurrentCounter(new SimpleCounter());
            ConcurrentCountingTester.RunIncrementAndDecrement(counter, stepsAmount: 1000);
        }
    }
}

