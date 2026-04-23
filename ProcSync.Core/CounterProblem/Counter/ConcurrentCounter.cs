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

    async public Task Increment()
    {
        await _locker.RunLocked(_counter.Increment);
    }

    async public Task Decrement()
    {
        await _locker.RunLocked(_counter.Decrement);
    }

    async public Task Reset()
    {
        await _locker.RunLocked(_counter.Reset);
    }
}
