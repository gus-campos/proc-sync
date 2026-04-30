
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentSequenceGenerator<TItem>(
    TItem initialItem,
    Func<TItem, TItem> generator
) : IGenerator<TItem>
{
    private TItem? _lastGenerated = default;
    private readonly Func<TItem, TItem> _generator = generator;
    private readonly TItem _initialItem = initialItem;
    private readonly object _lock = new();

    public TItem GenerateNext()
    {
        lock (_lock)
        {
            var item = _lastGenerated == null ? _initialItem : _generator(_lastGenerated);
            _lastGenerated = item;
            return item;
        }
    }
}
