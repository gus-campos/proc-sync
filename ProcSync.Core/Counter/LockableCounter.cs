using ProcSync.Core.Shared;

namespace ProcSync.Core.Counter;

public class LockableCounter : ICounter
{
    public int Count { get; private set; } = 0;
    private readonly Locker _locker = new();

    public void Increment()
    {
        _locker.RunLocked(() => Count++);
    }

    public void Decrement()
    {
        _locker.RunLocked(() => Count--);
    }
}
