
namespace ProcSync.Core.CounterProblem.Counter;

public interface ICounter
{
    public int Count { get; }
    public Task Increment();
    public Task Decrement();
    public Task Reset();
}
