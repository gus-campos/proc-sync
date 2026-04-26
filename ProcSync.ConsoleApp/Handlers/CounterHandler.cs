using ProcSync.Core.Domain;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class CounterHandler
{
    public static void Run(int steps)
    {
        Console.WriteLine($"Passos: {steps}\n");

        // Simples - Incremento
        {
            var counter = new SimpleCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrement(steps);
        }

        // Simples - Incremento e decremento
        {
            var counter = new SimpleCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrementAndDecrement(steps);
        }

        // Simples - Incremento
        {
            var counter = new ConcurrentCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrement(steps);
        }

        // Simples - Incremento e decremento
        {
            var counter = new ConcurrentCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrementAndDecrement(steps);
        }
    }
}
