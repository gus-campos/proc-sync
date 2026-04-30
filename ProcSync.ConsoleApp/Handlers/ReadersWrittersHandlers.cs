
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class ReadersWrittersHandlers : BaseHandler
{
    protected override async Task RunSimple()
    {
        var unsafeResource = new ConcurrentResource();
        var unsafeSimulator = new ReadersWritersSimulator(unsafeResource, "SEM SINCRONIZAÇÃO");
        unsafeSimulator.Run(readerCount: 5, writerCount: 2, millisecondsToRun: 5000);
    }


    protected override async Task RunConcurrent()
    {
        var safeResource = new ConcurrentResource();
        var safeSimulator = new ReadersWritersSimulator(safeResource, "COM SINCRONIZAÇÃO");
        safeSimulator.Run(readerCount: 5, writerCount: 2, millisecondsToRun: 5000);
    }
}
