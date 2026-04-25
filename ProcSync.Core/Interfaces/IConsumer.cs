
namespace ProcSync.Core.Interfaces;

public interface IConsumer<TItem>
{
    public void Consume(IBuffer<TItem> buffer);
}
