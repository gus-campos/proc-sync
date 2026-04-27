
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class DinningPhilosofersHandler
{
    public static void Run(int bufferSize, int totalItems)
    {
        Console.WriteLine("===== Teste Jantar dos Filósofos =====");

        var deadlockTable = new DiningTable();
        var deadlockSimulator = new DiningPhilosophersSimulator(deadlockTable, "COM DEADLOCK");
        deadlockSimulator.Run(millisecondsTimeout: 4000); // após 4 segundos cancela o teste, pois espera-se que haja deadlock e os filósofos parem de comer

        Thread.Sleep(500);
        Console.WriteLine();

        var safeTable = new ConcurrentDiningTable();
        var safeSimulator = new DiningPhilosophersSimulator(safeTable, "SEM DEADLOCK");
        safeSimulator.Run(millisecondsTimeout: 4000);

        Console.WriteLine("===== Fim do teste Jantar dos Filósofos =====");
    }
}
