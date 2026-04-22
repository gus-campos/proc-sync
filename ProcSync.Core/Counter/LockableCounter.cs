using ProcSync.Core.Shared;

namespace ProcSync.Core.Counter;

public class LockableCounter : ICounter
{
    private readonly ICounter _counter;
    private readonly Locker _locker = new();

    public int Count => _counter.Count;

    public LockableCounter(ICounter counter)
    {
        _counter = counter;
    }

    public void Increment()
    {
        _locker.RunLocked(_counter.Increment);
    }

    public void Decrement()
    {
        _locker.RunLocked(_counter.Decrement);
    }

    public void Reset()
    {
        _locker.RunLocked(_counter.Decrement);
    }
}
