
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ConcurrentCountingSimulator(
    ICounter counter
)
{
    public void RunIncrement(int stepsAmount)
    {
        Parallel.For(0, stepsAmount, (_) =>
        {
            counter.Increment();
        });

        Console.WriteLine($"Contagem final: {counter.Count}");
    }

    public void RunIncrementAndDecrement(int stepsAmount)
    {
        Parallel.For(0, stepsAmount, (_) =>
        {
            counter.Increment();
            counter.Decrement();
        });

        Console.WriteLine($"Contagem final: {counter.Count}");
    }
}
