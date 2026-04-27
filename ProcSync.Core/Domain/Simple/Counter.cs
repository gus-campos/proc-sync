
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class Counter : ICounter
{
    public int Count { get; private set; } = 0;

    public void Increment()
    {
        Count++;
    }

    public void Decrement()
    {
        Count--;
    }

    public void Reset()
    {
        Count = 0;
    }
}
