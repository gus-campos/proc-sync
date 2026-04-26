
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ProducerConsumerSimulator(
    IProducer<double> producer,
    IConsumer<double> consumer
)
{
    async public Task Run(int totalTimeInMs)
    {
        await producer.StartAsync();
        await consumer.StartAsync();

        await Task.Delay(totalTimeInMs);

        var stopProducerTask = producer.StopAsync();
        var stopConsumerTask = consumer.StopAsync();

        await Task.WhenAll([stopProducerTask, stopConsumerTask]);
    }
}
