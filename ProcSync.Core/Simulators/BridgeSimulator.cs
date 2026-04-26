using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class BridgeSimulator
{
    private readonly IBridge _bridge;
    private readonly string _label;

    public BridgeSimulator(IBridge bridge, string label)
    {
        _bridge = bridge;
        _label = label;
    }

    public void Run(int northCount, int southCount, int southDelayMs, int travelTimeMs, int spacingMs, int timeoutMs)
    {
        Console.WriteLine($"--- Iniciando travessia [{_label}] (Norte-Sul: {northCount}, Sul-Norte: {southCount}, atraso Sul: {southDelayMs}ms) ---");
        using var cts = new CancellationTokenSource(timeoutMs);
        var tasks = new List<Task>();

        for (int i = 0; i < northCount; i++)
        {
            int id = i;
            tasks.Add(Task.Run(async () =>
            {
                if (cts.Token.IsCancellationRequested) return;
                await Task.Delay(i * spacingMs, cts.Token);
                _bridge.Enter("Norte-Sul");
                Thread.Sleep(travelTimeMs);
                _bridge.Exit();
            }, cts.Token));
        }

        for (int i = 0; i < southCount; i++)
        {
            int id = i;
            tasks.Add(Task.Run(async () =>
            {
                if (cts.Token.IsCancellationRequested) return;
                await Task.Delay(southDelayMs + i * spacingMs, cts.Token);
                _bridge.Enter("Sul-Norte");
                Thread.Sleep(travelTimeMs);
                _bridge.Exit();
            }, cts.Token));
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"--- Fim da travessia [{_label}] ---\n");
    }
}