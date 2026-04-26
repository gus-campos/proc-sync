// ProcSync.Core/Simulators/BridgeSimulator.cs
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

    public void Run(int[] northSouthBursts, int[] southNorthBursts, int travelTimeMs, int timeoutMs)
    {
        Console.WriteLine($"--- Iniciando travessia [{_label}] ---");
        using var cts = new CancellationTokenSource(timeoutMs);
        var tasks = new List<Task>();

        // Lança rajadas de veículos Norte-Sul
        foreach (int count in northSouthBursts)
        {
            for (int i = 0; i < count; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    if (cts.Token.IsCancellationRequested) return;
                    _bridge.Enter("Norte-Sul");
                    Thread.Sleep(travelTimeMs); // simula travessia
                    _bridge.Exit();
                }));
                Thread.Sleep(10); // pequeno intervalo entre veículos da mesma rajada
            }
            Thread.Sleep(100); // pausa entre rajadas
        }

        // Lança rajadas de veículos Sul-Norte
        foreach (int count in southNorthBursts)
        {
            for (int i = 0; i < count; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    if (cts.Token.IsCancellationRequested) return;
                    _bridge.Enter("Sul-Norte");
                    Thread.Sleep(travelTimeMs);
                    _bridge.Exit();
                }));
                Thread.Sleep(10);
            }
            Thread.Sleep(100);
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"--- Fim da travessia [{_label}] ---\n");
    }
}