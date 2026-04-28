
using ProcSync.Core.Interfaces;
using ProcSync.Core.Utils;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentCircularBuffer<TItem> : IBuffer<TItem>
{
    private readonly TItem[] _items;
    private readonly int _size;
    private readonly object _lock = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private int _inIndex = 0;
    private int _outIndex = 0;
    private int _count = 0;

    private bool IsEmpty => _count == 0;
    private bool IsFull => _count == _size;

    public ConcurrentCircularBuffer(int size)
    {
        _size = size;
        _items = new TItem[_size];
    }

    public bool TryPut(TItem item)
    {
        _semaphore.Wait();
        try
        {
            if (IsFull)
                return false;

            _items[_inIndex] = item;
            IncrementInIndex();
            _count++;

            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public Result<TItem> TryGet()
    {
        _semaphore.Wait();
        try
        {
            if (IsEmpty)
                return new(false, default);

            var item = _items[_outIndex];
            IncrementOutIndex();
            _count--;
            return new(true, item);
        }
        finally
        {
            _semaphore.Release();
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
