
namespace ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

public interface ICircularBuffer<TItem>
{
    public void Put(TItem item);
    public TItem Get();
    public bool IsEmpty { get; }
    public bool IsFull { get; }
}
