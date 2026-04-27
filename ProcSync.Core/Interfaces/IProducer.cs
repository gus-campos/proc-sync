
namespace ProcSync.Core.Interfaces;

public interface IProducer<TItem>
{
    public void Start();
    public Task StopAsync();
}
