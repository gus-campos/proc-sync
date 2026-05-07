namespace ProcSync.Core.Interfaces;

public interface IBarberShop
{
    public Task RunAsync(int millisecondsTimeout);
}