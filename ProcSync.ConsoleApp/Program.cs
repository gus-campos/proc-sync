namespace ProcSync.ConsoleApp;

using ProcSync.Core.SharedCounter;

public static class Program
{
    public static void Main(string[] args)
    {
        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            var parallelCounting = new ParallelCounting(counter);
            parallelCounting.RunIncrementAndDecrement(1000);
            Console.WriteLine($"Simple     add e sub: {counter.Count}");
        }

        // Simple counter - Increment
        {
            var counter = new SimpleCounter();
            var parallelCounting = new ParallelCounting(counter);
            parallelCounting.RunIncrement(1000);
            Console.WriteLine($"Simple     add      : {counter.Count}");
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new ConcurrentCounter();
            var parallelCounting = new ParallelCounting(counter);
            parallelCounting.RunIncrementAndDecrement(1000);
            Console.WriteLine($"Concurrent add e sub: {counter.Count}");
        }

        // Concurrent counter - Increment
        {
            var counter = new ConcurrentCounter();
            var parallelCounting = new ParallelCounting(counter);
            parallelCounting.RunIncrement(1000);
            Console.WriteLine($"Concurrent add      : {counter.Count}");
        }
    }
}

