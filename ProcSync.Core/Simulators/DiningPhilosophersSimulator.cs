// Ficheiro: ProcSync.Core/Simulators/DiningPhilosophersSimulator.cs
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class DiningPhilosophersSimulator
{
    private readonly IDiningTable _table;
    private readonly string _versionLabel;

    public DiningPhilosophersSimulator(IDiningTable table, string versionLabel)
    {
        _table = table;
        _versionLabel = versionLabel;
    }

    public void Run(int millisecondsTimeout)
    {
        Console.WriteLine($"--- Iniciando Jantar dos Filósofos [{_versionLabel}] ---");
        _table.Dine(millisecondsTimeout);
        Console.WriteLine($"--- Fim do Jantar [{_versionLabel}] ---\n");
    }
}