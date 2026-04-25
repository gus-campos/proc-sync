
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SequenceGenerator<TItem> : IGenerator<TItem>
{
    private TItem? _lastGenerated = default;
    private readonly Func<TItem, TItem> _generator;
    private readonly TItem _initialItem;

    public SequenceGenerator(TItem initialItem, Func<TItem, TItem> generator)
    {
        _generator = generator;
        _initialItem = initialItem;
    }

    public TItem GenerateNext()
    {
        var item = _lastGenerated == null ? _initialItem : _generator(_lastGenerated);
        _lastGenerated = item;
        return item;
    }
}
