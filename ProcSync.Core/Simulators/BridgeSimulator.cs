using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class BridgeSimulator(IOneWayBridge bridge, string label)
{
    public async Task RunAsync(int northCount, int southCount, int southDelayMs, int travelTimeMs, int spacingMs, int timeoutMs)
    {
        Console.WriteLine($"--- Iniciando travessia [{label}] ---");
        using var cts = new CancellationTokenSource(timeoutMs);
        var tasks = new List<Task>();

        // Veículos de Norte para Sul
        for (int i = 0; i < northCount; i++)
        {
            int id = i;
            tasks.Add(Task.Run(async () =>
            {
                if (cts.Token.IsCancellationRequested) return;
                await Task.Delay(id * spacingMs, cts.Token); // espaçamento progressivo
                bridge.Enter(BridgeDirection.NorthToSouth);
                await Task.Delay(travelTimeMs, cts.Token); // tempo a atravessar
                bridge.Exit();
            }, cts.Token));
        }

        // Veículos de Sul para Norte (com atraso extra)
        for (int i = 0; i < southCount; i++)
        {
            int id = i;
            tasks.Add(Task.Run(async () =>
            {
                if (cts.Token.IsCancellationRequested) return;
                await Task.Delay(southDelayMs + i * spacingMs, cts.Token);
                bridge.Enter(BridgeDirection.SouthToNorth);
                await Task.Delay(travelTimeMs, cts.Token);
                bridge.Exit();
            }, cts.Token));
        }

        await Task.WhenAll(tasks); // aguarda todos os veículos terminarem
        Console.WriteLine($"--- Fim da travessia [{label}] ---\n");
    }
}