
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class CounterHandler
{
    public static void Run(int steps)
    {
        Console.WriteLine($"Passos: {steps}\n");

        Run_Simple_Increment(steps);
        Run_Simple_IncrementDecrement(steps);
        Run_Concurrent_Increment(steps);
        Run_Concurrent_IncrementDecrement(steps);
    }

    private static void Run_Simple_Increment(int steps)
    {
        var counter = new Counter();
        var simulator = new ConcurrentCountingSimulator(counter);
        simulator.RunIncrement(steps);
    }

    private static void Run_Simple_IncrementDecrement(int steps)
    {
        var counter = new Counter();
        var simulator = new ConcurrentCountingSimulator(counter);
        simulator.RunIncrementAndDecrement(steps);
    }

    private static void Run_Concurrent_Increment(int steps)
    {
        var counter = new ConcurrentCounter();
        var simulator = new ConcurrentCountingSimulator(counter);
        simulator.RunIncrement(steps);
    }

    private static void Run_Concurrent_IncrementDecrement(int steps)
    {
        var counter = new ConcurrentCounter();
        var simulator = new ConcurrentCountingSimulator(counter);
        simulator.RunIncrementAndDecrement(steps);
    }
}
