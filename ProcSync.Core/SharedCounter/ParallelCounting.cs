
namespace ProcSync.Core.SharedCounter;

public class ParallelCounting(ICounter counter)
{
    public void RunIncrementAndDecrement(int stepsAmount)
    {
        Parallel.For(0, stepsAmount, (i) =>
        {
            counter.Increment();
            counter.Decrement();
        });
    }

    public void RunIncrement(int stepsAmount)
    {
        Parallel.For(0, stepsAmount, (i) =>
        {
            counter.Increment();
            counter.Decrement();
        });
    }
}
