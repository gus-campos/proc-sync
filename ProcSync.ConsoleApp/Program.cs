namespace ProcSync.ConsoleApp;

using ProcSync.Core;
using ProcSync.Core.SharedCounter;

public static class Program
{
    public static void Main(string[] args)
    {
        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            ParallelProcessor.RunParallel(1000, () =>
            {
                counter.Increment();
                counter.Decrement();
            });
            Console.WriteLine($"Simple     add e sub: {counter.Count}");
        }

        // Simple counter - Increment
        {
            var counter = new SimpleCounter();
            ParallelProcessor.RunParallel(1000, () =>
            {
                counter.Increment();
            });
            Console.WriteLine($"Simple     add e sub: {counter.Count}");
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new ConcurrentCounter();
            ParallelProcessor.RunParallel(1000, () =>
            {
                counter.Increment();
                counter.Decrement();
            });
            Console.WriteLine($"Concurrent add e sub: {counter.Count}");
        }

        // Concurrent counter - Increment
        {
            var counter = new ConcurrentCounter();
            ParallelProcessor.RunParallel(1000, () =>
            {
                counter.Increment();
            });
            Console.WriteLine($"Concurrent add e sub: {counter.Count}");
        }
    }
}

