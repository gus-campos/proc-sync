
namespace ProcSync.Core.Interfaces;

public interface IConsumer<TItem>
{
    public void Start();
    public Task StopAsync();
}
