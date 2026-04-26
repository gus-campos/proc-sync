
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SimpleProducer<TItem> : IProducer<TItem>
{
    private readonly IBuffer<TItem> _buffer;
    private readonly IGenerator<TItem> _generator;
    private readonly int _timeToProduceInMs;
    private readonly PeriodicWorker _periodicWorker;

    public SimpleProducer(
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

    async public Task StopAsync()
    {
        await _periodicWorker.StopAsync();
    }

    async private Task TryToProduce()
    {
        if (!_buffer.IsFull)
            await Produce();
    }

    async private Task Produce()
    {
        await Task.Delay(_timeToProduceInMs);
        TItem item = _generator.GenerateNext();
        _buffer.Put(item);
        Console.WriteLine($"Produziu: {item}");
    }
}
