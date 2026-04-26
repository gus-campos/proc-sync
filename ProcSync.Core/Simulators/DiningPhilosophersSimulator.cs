using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class DiningPhilosophersSimulator(IDiningTable table, string versionLabel)
{
    public void Run(int millisecondsTimeout)
    {
        Console.WriteLine($"--- Iniciando Jantar dos Filósofos [{versionLabel}] ---");
        table.Dine(millisecondsTimeout);
        Console.WriteLine($"--- Fim do Jantar [{versionLabel}] ---\n");
    }
}
