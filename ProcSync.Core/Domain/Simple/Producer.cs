
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class Producer<TItem> : IProducer<TItem>
{
    private readonly IBuffer<TItem> _buffer;
    private readonly IGenerator<TItem> _generator;
    private readonly int _timeToProduceInMs;
    private readonly PeriodicWorker _periodicWorker;

    public Producer(
        IBuffer<TItem> buffer,
        IGenerator<TItem> generator,
        int timeToProduceInMs,
        int timeToCheckInMs
    )
    {
        _buffer = buffer;
        _generator = generator;
        _timeToProduceInMs = timeToProduceInMs;
        _periodicWorker = new PeriodicWorker(TryToProduce, timeToCheckInMs);
    }

    public void Start()
    {
        _periodicWorker.Start();
    }

    public async Task StopAsync()
    {
        await _periodicWorker.StopAsync();
    }

    private async Task TryToProduce()
    {
        if (_buffer.IsFull)
            return;

        await Produce();
    }

    private async Task Produce()
    {
        await Task.Delay(_timeToProduceInMs);
        TItem item = _generator.GenerateNext();
        _buffer.Put(item);
        Console.WriteLine($"Produziu: {item}");
    }
}
