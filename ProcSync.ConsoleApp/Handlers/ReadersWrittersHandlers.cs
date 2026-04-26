
using ProcSync.Core.Domain;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class ReadersWrittersHandlers
{
    public static void Run(int bufferSize, int totalItems)
    {
        Console.WriteLine("===== Teste Leitores e Escritores =====");

        var unsafeResource = new UnsafeResource();
        var unsafeSimulator = new ReadersWritersSimulator(unsafeResource, "SEM SINCRONIZAÇÃO");
        unsafeSimulator.Run(readerCount: 5, writerCount: 2, millisecondsToRun: 5000);

        Thread.Sleep(500);
        Console.WriteLine();

        var safeResource = new SafeResource();
        var safeSimulator = new ReadersWritersSimulator(safeResource, "COM SINCRONIZAÇÃO");
        safeSimulator.Run(readerCount: 5, writerCount: 2, millisecondsToRun: 5000);

        Console.WriteLine("===== Fim do teste de Leitores e Escritores =====");
    }
}
