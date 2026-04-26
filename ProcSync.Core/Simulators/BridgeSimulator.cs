using ProcSync.Core.Domain;
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class BridgeSimulator(IOneWayBridge bridge, string label)
{
    public void Run(int northCount, int southCount, int southDelayMs, int travelTimeMs, int spacingMs, int timeoutMs)
    {
        Console.WriteLine($"--- Iniciando travessia [{label}] (Norte-Sul: {northCount}, Sul-Norte: {southCount}, atraso Sul: {southDelayMs}ms) ---");
        using var cts = new CancellationTokenSource(timeoutMs);
        var tasks = new List<Task>();

        for (int i = 0; i < northCount; i++)
        {
            int id = i;
            tasks.Add(Task.Run(async () =>
            {
                if (cts.Token.IsCancellationRequested) return;
                await Task.Delay(i * spacingMs, cts.Token);
                bridge.Enter(BridgeDirection.NorthToSouth);
                Thread.Sleep(travelTimeMs);
                bridge.Exit();
            }, cts.Token));
        }

        for (int i = 0; i < southCount; i++)
        {
            int id = i;
            tasks.Add(Task.Run(async () =>
            {
                if (cts.Token.IsCancellationRequested) return;
                await Task.Delay(southDelayMs + i * spacingMs, cts.Token);
                bridge.Enter(BridgeDirection.SouthToNorth);
                Thread.Sleep(travelTimeMs);
                bridge.Exit();
            }, cts.Token));
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"--- Fim da travessia [{label}] ---\n");
    }
}
