
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class ConcurrentCounter : ICounter
{
    private volatile int _count = 0;

    public int Count => _count;

    public void Increment()
    {
        Interlocked.Increment(ref _count);
    }

    public void Decrement()
    {
        Interlocked.Decrement(ref _count);
    }

    public void Reset()
    {
        Interlocked.Exchange(ref _count, 0);
    }
}
