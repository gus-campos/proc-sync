
using System.Collections.Concurrent;

using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class PrinterSimulator(
    IPrinter printer,
    int clientsAmount,
    int filesPerClient,
    int baseTimeToQueue
)
{
    private readonly Random _random = new();

    public async Task Run()
    {
        // Varios clients, cada um vai pedir pra imprimir
        // um arquivo de cada vez, com um intervalo aleatório de tempo entre eles

        printer.Start();

        var tasks = Enumerable.Range(0, clientsAmount).Select(clientIndex =>
        {
            var clientQueue = BuildClientQueue(clientIndex);
            return SimulateClient(clientQueue, clientIndex);
        });

        await Task.WhenAll(tasks);
        await printer.Stop();
    }

    private async Task SimulateClient(
        ConcurrentQueue<PrinterFile> clientQueue,
        int clientIndex
    )
    {
        ConcurrentBag<Task<PrinterFile>> printTasksBag = [];

        PrinterFile? file;
        while (clientQueue.TryDequeue(out file))
        {
            await Task.Delay(GetRandomTimeInMs());
            var printTask = printer.QueuePrint(file);
            printTasksBag.Add(printTask);
        }

        Console.WriteLine($"{clientIndex:0000} terminou a impressão");
    }

    private int GetRandomTimeInMs()
    {
        const double variationRate = 0.3;
        int variationDelta = (int)Math.Floor(variationRate * baseTimeToQueue);

        return _random.Next(
            baseTimeToQueue - variationDelta,
            baseTimeToQueue + variationDelta
        );
    }

    private ConcurrentQueue<PrinterFile> BuildClientQueue(int clientIndex)
    {
        ConcurrentQueue<PrinterFile> clientQueue = [];

        var files = Enumerable.Range(0, filesPerClient)
            .Select(fileIndex =>
            {
                string content = $"{clientIndex:0000}-{fileIndex:0000}";
                return new PrinterFile(content);
            }
            ).ToList();

        foreach (var file in files)
            clientQueue.Enqueue(file);

        return clientQueue;
    }
}
