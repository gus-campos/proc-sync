using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class BarberSimulator
{
    private readonly IBarberShop _shop;
    private readonly string _label;

    public BarberSimulator(IBarberShop shop, string label)
    {
        _shop = shop;
        _label = label;
    }

    public void Run(int timeoutMs)
    {
        Console.WriteLine($"--- Iniciando Barbearia [{_label}] ---");
        _shop.Run(timeoutMs);
        Console.WriteLine($"--- Fim da Barbearia [{_label}] ---\n");
    }
}