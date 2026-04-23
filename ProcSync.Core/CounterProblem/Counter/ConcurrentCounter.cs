
namespace ProcSync.Core.CounterProblem.Counter;

public class ConcurrentCounter : ICounter
{
    private readonly ICounter _counter;
    private readonly object _lock = new();

    public int Count => _counter.Count;

    public ConcurrentCounter(ICounter counter)
    {
        _counter = counter;
    }

    public void Increment()
    {
        lock (_lock)
            _counter.Increment();
    }

    public void Decrement()
    {
        lock (_lock)
            _counter.Decrement();
    }

    public void Reset()
    {
        lock (_lock)
            _counter.Reset();
    }
}
