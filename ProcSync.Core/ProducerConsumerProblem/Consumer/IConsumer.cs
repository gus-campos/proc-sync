
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

namespace ProcSync.Core.ProducerConsumerProblem.Consumer;

public interface IConsumer<TItem>
{
    public void Consume(IBuffer<TItem> buffer);
}
