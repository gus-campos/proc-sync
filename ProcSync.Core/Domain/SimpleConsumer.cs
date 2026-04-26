
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SimpleConsumer<TItem>(
    IBuffer<TItem> buffer,
    int timeToConsumeInMs,
    int timeToCheckInMs
) : AsyncAgent(timeToCheckInMs), IConsumer<TItem>
{
    async override protected Task Process()
    {
        if (!buffer.IsEmpty)
            await Consume();
    }

    async private Task Consume()
    {
        var item = buffer.Get();
        await Task.Delay(timeToConsumeInMs);
        Console.WriteLine($"Consumiu: {item}");
    }
}
