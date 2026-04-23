
namespace ProcSync.Core.CounterProblem.Counter;

public class ConcurrentCounter : ICounter
{
    private readonly ICounter _counter;

    public int Count => _counter.Count;

    public ConcurrentCounter(ICounter counter)
    {
        _counter = counter;
    }

    public void Increment()
    {
        lock (_counter)
        {
            _counter.Increment();
        }
    }

    public void Decrement()
    {
        lock (_counter)
        {
            _counter.Decrement();
        }
    }

    public void Reset()
    {
        lock (_counter)
        {
            _counter.Reset();
        }
    }
}
