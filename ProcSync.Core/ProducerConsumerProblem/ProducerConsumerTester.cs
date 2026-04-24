
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;
using ProcSync.Core.ProducerConsumerProblem.Consumer;
using ProcSync.Core.ProducerConsumerProblem.Producer;

namespace ProcSync.Core.ProducerConsumerProblem;

public class ProducerConsumerTester(
    IBuffer<double> buffer,
    IProducer<double> producer,
    IConsumer<double> consumer
)
{
    public void Run(int totalItemsAmount)
    {
        var consuptionTask = Task.Run(
            () => ConsumeMany(totalItemsAmount)
        );

        var producionTask = Task.Run(
            () => ProduceMany(totalItemsAmount)
        );

        Task.WaitAll([consuptionTask, producionTask]);
    }

    private void ConsumeMany(int totalItemsAmount)
    {
        for (int index = 0; index < totalItemsAmount; index++)
            consumer.Consume(buffer);
    }

    private void ProduceMany(int totalItemsAmount)
    {
        for (int index = 0; index < totalItemsAmount; index++)
            producer.Produce(buffer);
    }
}
