
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentProducer<TItem> : IProducer<TItem>
{
    private readonly IBuffer<TItem> _buffer;
    private readonly IGenerator<TItem> _generator;
    private readonly int _timeToProduceInMs;
    private readonly ConcurrentPeriodicWorker _periodicWorker;

    public ConcurrentProducer(
        IBuffer<TItem> buffer,
        IGenerator<TItem> generator,
        int timeToProduceInMs,
        int timeToCheckInMs
    )
    {
        _buffer = buffer;
        _generator = generator;
        _timeToProduceInMs = timeToProduceInMs;
        _periodicWorker = new(TryToProduce, timeToCheckInMs);
    }

    public void Start()
    {
        _periodicWorker.Start();
    }

    public async Task StopAsync()
    {
        await _periodicWorker.StopAsync();
    }

    private async Task TryToProduce(CancellationToken token)
    {
        await Task.Delay(_timeToProduceInMs, token);
        TItem item = _generator.GenerateNext();

        bool hadSucces = _buffer.TryPut(item);

        if (hadSucces)
        {
            Console.WriteLine($"Produziu: {item}");
        }
        else
        {
            Console.WriteLine($"Descartou: {item}");
        }
    }
}
