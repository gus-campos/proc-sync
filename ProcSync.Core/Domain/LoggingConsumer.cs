
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class LoggingConsumer<TItem> : IConsumer<TItem>
{
    private readonly int _delayInMiliseconds;

    public LoggingConsumer(int delayInMiliseconds)
    {
        _delayInMiliseconds = delayInMiliseconds;
    }

    public void Consume(IBuffer<TItem> buffer)
    {
        while (buffer.IsEmpty)
        {
            // FIXME: Tirar daqui
            Console.WriteLine("\rConsumer esperando...");
            Thread.Yield();
        }

        var item = buffer.Get();
        Thread.Sleep(_delayInMiliseconds);
        Console.WriteLine($"Produziu: {item}");
    }
}
