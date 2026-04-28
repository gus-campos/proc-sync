
using ProcSync.Core.Interfaces;
using ProcSync.Core.Utils;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentConsumer<TItem> : IConsumer<TItem>
{
    private readonly IBuffer<TItem> _buffer;
    private readonly int _timeToConsumeInMs;
    private readonly ConcurrentPeriodicWorker _periodicWorker;

    public ConcurrentConsumer(
        IBuffer<TItem> buffer,
        int timeToConsumeInMs,
        int timeToCheckInMs
    )
    {
        _buffer = buffer;
        _timeToConsumeInMs = timeToConsumeInMs;
        _periodicWorker = new(TryToConsume, timeToCheckInMs);
    }

    public void Start()
    {
        _periodicWorker.Start();
    }

    public async Task StopAsync()
    {
        await _periodicWorker.StopAsync();
    }

    private async Task TryToConsume(CancellationToken token)
    {
        Result<TItem> result = _buffer.TryGet();

        if (result.Succes)
        {
            await Task.Delay(_timeToConsumeInMs, token);
            Console.WriteLine($"Consumiu: {result.Item}");
        }
    }
}
