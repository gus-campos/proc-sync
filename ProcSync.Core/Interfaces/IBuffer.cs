
namespace ProcSync.Core.Interfaces;

public interface IBuffer<TItem>
{
    public void Put(TItem item);
    public TItem Get();
    public bool IsEmpty { get; }
    public bool IsFull { get; }
}
