
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class CounterHandler(int steps) : BaseHandler
{
    protected override async Task RunSimple()
    {
        var counter = new Counter();
        var simulator = new ConcurrentCountingSimulator(counter);
        simulator.RunIncrementAndDecrement(steps);
    }

    protected override async Task RunConcurrent()
    {
        var counter = new ConcurrentCounter();
        var simulator = new ConcurrentCountingSimulator(counter);
        simulator.RunIncrementAndDecrement(steps);
    }
}
