namespace ProcSync.Core;

public static class ParallelProcessor
{
    public static void RunParallel(int stepsAmount, Action action)
    {
        Parallel.For(0, stepsAmount, (_) =>
        {
            action();
        });
    }
}
