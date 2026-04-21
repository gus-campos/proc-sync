namespace ProcSync.Core.Counter;

public static class ParallelCountingTester
{
    public static void RunIncrement(int stepsAmount, ICounter counter)
    {
        Parallel.For(0, stepsAmount, (_) =>
        {
            counter.Increment();
        });
    }

    public static void RunIncrementAndDecrement(int stepsAmount, ICounter counter)
    {
        Parallel.For(0, stepsAmount, (_) =>
        {
            counter.Increment();
            counter.Decrement();
        });
    }
}
