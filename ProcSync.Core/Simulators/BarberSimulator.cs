using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class BarberSimulator(IBarberShop shop, string label)
{
    public void Run(int timeoutMs)
    {
        Console.WriteLine($"--- Iniciando Barbearia [{label}] ---");
        shop.Run(timeoutMs);
        Console.WriteLine($"--- Fim da Barbearia [{label}] ---\n");
    }
}
