
namespace ProcSync.Core.CounterProblem.Counter;

public class SimpleCounter : ICounter
{
    public int Count { get; private set; } = 0;

    async public Task Increment()
    {
        Count++;
    }

    async public Task Decrement()
    {
        Count--;
    }

    async public Task Reset()
    {
        Count = 0;
    }
}
