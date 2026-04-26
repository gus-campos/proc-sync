
using ProcSync.Core.Domain;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class OneWayBridgeHandler
{
    public static void Run(int bufferSize, int totalItems)
    {
        Console.WriteLine("===== Teste Ponte de Mão Única =====");

        var unsafeBridge = new UnsafeOneWayBridge();
        var unsafeSim = new BridgeSimulator(unsafeBridge, "SEM ALTERNÂNCIA");
        unsafeSim.Run(northCount: 15, southCount: 5, southDelayMs: 200, travelTimeMs: 80, spacingMs: 20, timeoutMs: 10000);

        Thread.Sleep(500);
        Console.WriteLine();

        var safeBridge = new SafeOneWayBridge();
        var safeSim = new BridgeSimulator(safeBridge, "COM ALTERNÂNCIA");
        safeSim.Run(northCount: 15, southCount: 5, southDelayMs: 200, travelTimeMs: 80, spacingMs: 20, timeoutMs: 10000);

        Console.WriteLine("===== Fim do teste da ponte =====");
    }
}
