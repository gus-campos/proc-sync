
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

namespace ProcSync.Core.ProducerConsumerProblem.Producer;

public interface IProducer<TItem>
{
    public void Produce(IBuffer<TItem> buffer);
}
