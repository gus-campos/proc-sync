namespace ProcSync.ConsoleApp;

using ProcSync.Core.Counter;

public static class Program
{
    public static void Main(string[] args)
    {
        // Simple counter - Increment
        {
            var counter = new SimpleCounter();
            ParallerCountingTester.RunIncrement(1000, counter);
            Console.WriteLine($"Simple     add e sub: {counter.Count}");
        }

        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            ParallerCountingTester.RunIncrementAndDecrement(1000, counter);
            Console.WriteLine($"Simple     add e sub: {counter.Count}");
        }

        // Concurrent counter - Increment
        {
            var counter = new ConcurrentCounter();
            ParallerCountingTester.RunIncrement(1000, counter);
            Console.WriteLine($"Concurrent add e sub: {counter.Count}");
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new ConcurrentCounter();
            ParallerCountingTester.RunIncrementAndDecrement(1000, counter);
            Console.WriteLine($"Concurrent add e sub: {counter.Count}");
        }
    }
}

