
using ProcSync.Core.CounterProblem.Counter;

namespace ProcSync.Core.CounterProblem;

public static class ConcurrentCountingTester
{
    public static void RunIncrement(ICounter counter, int stepsAmount)
    {
        Parallel.For(0, stepsAmount, (_) =>
        {
            counter.Increment();
        });

        Console.WriteLine($"Contagem final: {counter.Count}");
    }

    public static void RunIncrementAndDecrement(ICounter counter, int stepsAmount)
    {
        Parallel.For(0, stepsAmount, (_) =>
        {
            counter.Increment();
            counter.Decrement();
        });

        Console.WriteLine($"Contagem final: {counter.Count}");
    }
}
