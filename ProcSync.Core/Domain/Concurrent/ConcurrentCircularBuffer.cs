
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentCircularBuffer<TItem> : IBuffer<TItem>
{
    private readonly TItem[] _items;
    private readonly int _size;
    private readonly object _lock = new();

    private volatile int _inIndex = 0;
    private volatile int _outIndex = 0;
    private volatile int _count = 0;

    public bool IsEmpty => _count == 0;
    public bool IsFull => _count == _size;

    public ConcurrentCircularBuffer(int size)
    {
        _size = size;
        _items = new TItem[_size];
    }

    public void Put(TItem item)
    {
        lock (_lock)
        {
            if (IsFull)
                throw new Exception("Não é possível inserir em buffer cheio");

            _items[_inIndex] = item;
            IncrementInIndex();
            _count++;
        }
    }

    public TItem Get()
    {
        lock (_lock)
        {
            if (IsEmpty)
                throw new Exception("Não é possível obter de buffer vazio");

            var item = _items[_outIndex];
            IncrementOutIndex();
            _count--;
            return item;
        }
    }

    private void IncrementInIndex()
    {
        _inIndex = CalcIncrementedIn();
    }

    private void IncrementOutIndex()
    {
        _outIndex = CalcIncrementedOut();
    }

    private int CalcIncrementedOut()
    {
        return (_outIndex + 1) % _size;
    }

    private int CalcIncrementedIn()
    {
        return (_inIndex + 1) % _size;
    }
}
