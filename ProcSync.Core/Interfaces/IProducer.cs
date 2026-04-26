
namespace ProcSync.Core.Interfaces;

public interface IProducer<TItem>
{
    public Task StartAsync();
    public Task StopAsync();
}
