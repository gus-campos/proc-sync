
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class OneWayBridgeHandler : BaseHandler
{
    protected override async Task RunSimple()
    {
        var unsafeBridge = new OneWayBridge();
        var unsafeSim = new BridgeSimulator(unsafeBridge, "SEM ALTERNÂNCIA");
        unsafeSim.Run(northCount: 15, southCount: 5, southDelayMs: 200, travelTimeMs: 80, spacingMs: 20, timeoutMs: 10000);
    }

    protected override async Task RunConcurrent()
    {
        var safeBridge = new ConcurrentOneWayBridge();
        var safeSim = new BridgeSimulator(safeBridge, "COM ALTERNÂNCIA");
        safeSim.Run(northCount: 15, southCount: 5, southDelayMs: 200, travelTimeMs: 80, spacingMs: 20, timeoutMs: 10000);
    }
}
