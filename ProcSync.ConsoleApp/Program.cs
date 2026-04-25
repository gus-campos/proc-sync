// Ficheiro: ProcSync.ConsoleApp/Program.cs
using ProcSync.Core.Domain;
using ProcSync.Core.Interfaces;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp;

public static class Program
{
    public static void Main(string[] args)
    {
        // TestCounter();
        // TestProducerConsumer();

        // TestReadersWriters();
        TestDiningPhilosophers();
    }

    private static void TestProducerConsumer()
    {
        var buffer = new CircularBuffer<double>(size: 10);

        IGenerator<double> generator = new SequenceGenerator<double>(
            initialItem: 0,
            generator: (lastValue) => lastValue + 1
        );

        IProducer<double> producer = new LoggingProducer<double>(
            generator,
            delayInMiliseconds: 10
        );

        IConsumer<double> consumer = new LoggingConsumer<double>(
            delayInMiliseconds: 10
        );

        var tester = new ProducerConsumerSimulator(
            buffer,
            producer,
            consumer
        );

        tester.Run(totalItemsAmount: 100);

        // Para size 300 e steps 10 mi
        // consumer para em 9999399 (pois ficou vazio?)
        // producer para em 9999951 (pois ficou lotado?) 
    }

    private static void TestCounter()
    {
        // Simple counter - Increment
        {
            var counter = new SimpleCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrement(stepsAmount: 1000);
        }

        // Simple counter - Increment and Decrement
        {
            var counter = new SimpleCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrementAndDecrement(stepsAmount: 1000);
        }

        // Concurrent counter - Increment
        {
            var counter = new ConcurrentCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrement(stepsAmount: 1000);
        }

        // Concurrent counter - Increment and Decrement
        {
            var counter = new ConcurrentCounter();
            var tester = new ConcurrentCountingSimulator(counter);
            tester.RunIncrementAndDecrement(stepsAmount: 1000);
        }
    }

    private static void TestReadersWriters()
    {
        Console.WriteLine("===== Teste Leitores e Escritores =====");

        // Versão problemática (sem sincronização)
        var unsafeResource = new UnsafeResource();
        var unsafeSimulator = new ReadersWritersSimulator(unsafeResource, "SEM SINCRONIZAÇÃO");
        unsafeSimulator.Run(readerCount: 5, writerCount: 2, millisecondsToRun: 3000);

        // Pequena pausa para separar os outputs
        Thread.Sleep(500);
        Console.WriteLine();

        // Versão corrigida (com ReaderWriterLockSlim)
        var safeResource = new SafeResource();
        var safeSimulator = new ReadersWritersSimulator(safeResource, "COM SINCRONIZAÇÃO");
        safeSimulator.Run(readerCount: 5, writerCount: 2, millisecondsToRun: 3000);

        Console.WriteLine("===== Fim do teste de Leitores e Escritores =====");
    }

    private static void TestDiningPhilosophers()
    {
        Console.WriteLine("===== Teste Jantar dos Filósofos =====");

        // Versão problemática – demonstra deadlock
        var deadlockTable = new DeadlockDiningTable();
        var deadlockSimulator = new DiningPhilosophersSimulator(deadlockTable, "COM DEADLOCK");
        deadlockSimulator.Run(millisecondsTimeout: 4000); // após 4 segundos cancela
        // Esperado: zero ou quase zero refeições, porque os filósofos ficam em deadlock

        Thread.Sleep(500);
        Console.WriteLine();

        // Versão corrigida – evita deadlock
        var safeTable = new SafeDiningTable();
        var safeSimulator = new DiningPhilosophersSimulator(safeTable, "SEM DEADLOCK");
        safeSimulator.Run(millisecondsTimeout: 4000);
        // Esperado: várias refeições realizadas

        Console.WriteLine("===== Fim do teste Jantar dos Filósofos =====");
    }
}