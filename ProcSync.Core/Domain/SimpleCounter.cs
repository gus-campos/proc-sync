
namespace ProcSync.Core.Domain;

public class SimpleCounter : ICounter
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
