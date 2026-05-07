using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class DinningPhilosofersHandler : BaseHandler
{
    private const int TimeoutMs = 3000; // tempo máximo antes de cancelar a simulação (3 segundos)

    protected override async Task RunSimple()
    {
        var table = new DiningTable(); // todos os filósofos pegam o garfo esquerdo primeiro (deadlock)
        var simulator = new DiningPhilosophersSimulator(table, "COM DEADLOCK");
        simulator.Run(millisecondsTimeout: TimeoutMs);
    }

    protected override async Task RunConcurrent()
    {
        var table = new ConcurrentDiningTable(); // último filósofo inverte a ordem (sem deadlock)
        var simulator = new DiningPhilosophersSimulator(table, "SEM DEADLOCK");
        simulator.Run(millisecondsTimeout: TimeoutMs);
    }
}