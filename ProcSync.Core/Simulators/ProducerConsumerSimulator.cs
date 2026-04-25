
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ProducerConsumerSimulator(
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
