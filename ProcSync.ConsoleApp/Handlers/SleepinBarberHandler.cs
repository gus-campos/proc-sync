
using ProcSync.Core.Domain;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class SleepingBarberHandler
{
    public static void Run(int bufferSize, int totalItems)
    {
        Console.WriteLine("===== Teste Barbeiro Sonolento =====");

        var unsafeShop = new UnsafeBarberShop(chairs: 5);
        var unsafeSim = new BarberSimulator(unsafeShop, "SEM SINCRONIZAÇÃO");
        unsafeSim.Run(timeoutMs: 5000); // 5 segundos

        Thread.Sleep(500);
        Console.WriteLine();

        var safeShop = new SafeBarberShop(chairs: 5);
        var safeSim = new BarberSimulator(safeShop, "COM SINCRONIZAÇÃO");
        safeSim.Run(timeoutMs: 5000);

        Console.WriteLine("===== Fim do teste Barbeiro Sonolento =====");
    }
}
