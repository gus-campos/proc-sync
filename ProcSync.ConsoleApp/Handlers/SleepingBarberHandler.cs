
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class SleepingBarberHandler : BaseHandler
{
    protected override async Task RunSimple()
    {
        var unsafeShop = new BarberShop(chairs: 5);
        var unsafeSim = new BarberSimulator(unsafeShop, "SEM SINCRONIZAÇÃO");
        unsafeSim.Run(timeoutMs: 5000); // 5 segundos
    }


    protected override async Task RunConcurrent()
    {
        var safeShop = new ConcurrentBarberShop(chairs: 5);
        var safeSim = new BarberSimulator(safeShop, "COM SINCRONIZAÇÃO");
        safeSim.Run(timeoutMs: 5000);
    }

}

