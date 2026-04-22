namespace ProcSync.ConsoleApp;

using ProcSync.Core.Counter;

public static class Program
{
    public static void Main(string[] args)
    {
        // Simple counter - Increment
        {
            var counter = new SimpleCounter();
            ParallelCountingTester.RunIncrement(1000, counter);
            Console.WriteLine($"Simple     add      : {counter.Count}");
        }

        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            ParallelCountingTester.RunIncrementAndDecrement(1000, counter);
            Console.WriteLine($"Simple     add e sub: {counter.Count}");
        }

        // Concurrent counter - Increment
        {
            var counter = new LockableCounter(new SimpleCounter());
            ParallelCountingTester.RunIncrement(1000, counter);
            Console.WriteLine($"Concurrent add e sub: {counter.Count}");
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new LockableCounter(new SimpleCounter());
            ParallelCountingTester.RunIncrementAndDecrement(1000, counter);
            Console.WriteLine($"Concurrent add      : {counter.Count}");
        }
    }
}

