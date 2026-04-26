
namespace ProcSync.Core.Interfaces;

public interface IConsumer<TItem>
{
    public Task StartAsync();
    public Task StopAsync();
}
