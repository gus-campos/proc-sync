
namespace ProcSync.Core.Interfaces;

public interface IProducer<TItem>
{
    public void Produce(IBuffer<TItem> buffer);
}
