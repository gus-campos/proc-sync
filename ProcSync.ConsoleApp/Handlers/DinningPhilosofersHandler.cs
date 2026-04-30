
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class DinningPhilosofersHandler : BaseHandler
{
    protected override async Task RunSimple()
    {

        var table = new DiningTable();
        var simulator = new DiningPhilosophersSimulator(table, "COM DEADLOCK");
        simulator.Run(millisecondsTimeout: 4000); // após 4 segundos cancela o teste, pois espera-se que haja deadlock e os filósofos parem de comer
    }
    protected override async Task RunConcurrent()
    {

        var safeTable = new ConcurrentDiningTable();
        var safeSimulator = new DiningPhilosophersSimulator(safeTable, "SEM DEADLOCK");
        safeSimulator.Run(millisecondsTimeout: 4000);
    }

}
