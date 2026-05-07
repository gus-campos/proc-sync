using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class BarberSimulator(IBarberShop shop, string label)
{
    public async Task RunAsync(int timeoutMs)
    {
        Console.WriteLine($"--- Iniciando Barbearia [{label}] ---");
        await shop.RunAsync(timeoutMs);
        Console.WriteLine($"--- Fim da Barbearia [{label}] ---\n");
    }
}