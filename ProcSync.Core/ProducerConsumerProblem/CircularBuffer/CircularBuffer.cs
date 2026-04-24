
namespace ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

public class CircularBuffer<TItem> : IBuffer<TItem>
{
    private readonly TItem[] _items;
    private readonly int _size;
    private int _count = 0;
    private int _inIndex = 0;
    private int _outIndex = 0;

    public bool IsEmpty => _count == 0;
    public bool IsFull => _count == _size;

    public CircularBuffer(int size)
    {
        _size = size;
        _items = new TItem[_size];
    }

    public void Put(TItem item)
    {
        if (IsFull)
            throw new Exception("Não é possível inserir: buffer cheio");

        _items[_inIndex] = item;
        IncrementInIndex();
        _count++;
    }

    public TItem Get()
    {
        if (IsEmpty)
            throw new Exception("Não é possível obter: buffer vazio");

        var item = _items[_outIndex];
        IncrementOutIndex();
        _count--;
        return item;
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
