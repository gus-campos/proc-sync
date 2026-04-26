
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SimpleProducer<TItem>(
    IBuffer<TItem> buffer,
    IGenerator<TItem> generator,
    int timeToProduceInMs,
    int timeToCheckInMs
) : AsyncAgent(timeToCheckInMs), IProducer<TItem>
{
    async override protected Task Process()
    {
        if (!buffer.IsFull)
            await Produce();
    }

    async private Task Produce()
    {
        await Task.Delay(timeToProduceInMs);
        TItem item = generator.GenerateNext();
        buffer.Put(item);
        Console.WriteLine($"Produziu: {item}");
    }
}
