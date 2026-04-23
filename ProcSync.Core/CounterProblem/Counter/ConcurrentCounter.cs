using ProcSync.Core.Shared;

namespace ProcSync.Core.CounterProblem.Counter;

public class ConcurrentCounter : ICounter
{
    private readonly ICounter _counter;
    private readonly Locker _locker = new();

    public int Count => _counter.Count;

    public ConcurrentCounter(ICounter counter)
    {
        _counter = counter;
    }

    async public void Increment()
    {
        _locker.RunLocked(_counter.Increment);
    }

    async public void Decrement()
    {
        _locker.RunLocked(_counter.Decrement);
    }

    async public void Reset()
    {
        _locker.RunLocked(_counter.Reset);
    }
}
