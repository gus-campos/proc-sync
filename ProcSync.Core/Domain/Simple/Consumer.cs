
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class Consumer<TItem> : IConsumer<TItem>
{
    private readonly IBuffer<TItem> _buffer;
    private readonly int _timeToConsumeInMs;
    private readonly PeriodicWorker _periodicWorker;

    public Consumer(
        IBuffer<TItem> buffer,
        int timeToConsumeInMs,
        int timeToCheckInMs
    )
    {
        _buffer = buffer;
        _timeToConsumeInMs = timeToConsumeInMs;
        _periodicWorker = new PeriodicWorker(TryToConsume, timeToCheckInMs);
    }

    public void Start()
    {
        _periodicWorker.Start();
    }

    async public Task StopAsync()
    {
        await _periodicWorker.StopAsync();
    }

    async private Task TryToConsume()
    {
        if (_buffer.IsEmpty)
            return;

        await Consume();
    }

    async private Task Consume()
    {
        var item = _buffer.Get();
        await Task.Delay(_timeToConsumeInMs);
        Console.WriteLine($"Consumiu: {item}");
    }
}
