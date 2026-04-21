namespace ProcSync.Core.SharedCounter;

public interface ICounter
{
    public int Count { get; }
    public void Increment();
    public void Decrement();
}
