
namespace ProcSync.Core.CounterProblem.Counter;

public interface ICounter
{
    public int Count { get; }
    public void Increment();
    public void Decrement();
    public void Reset();
}
