
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class CounterHandler(int steps) : BaseHandler
{
    protected override void PrintParams()
    {
        // Console.WriteLine($"\nTamanho do buffer: {bufferSize}");
        // Console.WriteLine($"Tempo total: {totalTimeInMs / 1000.0} s");
        // Console.WriteLine($"Tempo pra checagem: {totalTimeInMs / 1000.0} s");
        // Console.WriteLine($"Tempo para produzir: {produceTimeInMs / 1000.0} s");
        // Console.WriteLine($"Tempo para consumir: {consumeTimeInMs / 1000.0} s");
    }

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
