
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ProducerConsumerSimulator(
    IEnumerable<IProducer<double>> producers,
    IEnumerable<IConsumer<double>> consumers
)
{
    public async Task Run(int totalTimeInMs)
    {
        foreach (var producer in producers)
            producer.Start();

        foreach (var consumer in consumers)
            consumer.Start();

        await Task.Delay(totalTimeInMs);

        // Esperar tudo terminar
        await Task.WhenAll([
            .. producers.Select(p => p.StopAsync()),
            .. consumers.Select(c => c.StopAsync())
        ]);
    }
}
