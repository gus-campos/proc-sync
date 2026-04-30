
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public record FileAndTaskSource(
    PrinterFile File,
    TaskCompletionSource<PrinterFile> TaskSource
);

public class Printer : IPrinter
{
    private readonly Queue<FileAndTaskSource> _printingQueue = [];
    private readonly PeriodicWorker _periodicWorker;
    private readonly int _printingTime;

    public Printer(int printingTimeInMs, int checkingTimeInMs)
    {
        _periodicWorker = new(TryToPrint, checkingTimeInMs);
        _printingTime = printingTimeInMs;
    }

    public Task<PrinterFile> QueuePrint(PrinterFile file)
    {
        var printTaskSource = new TaskCompletionSource<PrinterFile>();
        var fileAndTaskSource = new FileAndTaskSource(file, printTaskSource);
        _printingQueue.Enqueue(fileAndTaskSource);
        return printTaskSource.Task;
    }

    public void Start()
    {
        _periodicWorker.Start();
    }

    public async Task Stop()
    {
        await _periodicWorker.StopAsync();
    }

    private async Task TryToPrint()
    {
        await Task.Delay(_printingTime);

        FileAndTaskSource? fileAndTaskSource;
        bool wasDequeued = _printingQueue.TryDequeue(out fileAndTaskSource);

        if (wasDequeued)
        {
            fileAndTaskSource!.TaskSource.SetResult(fileAndTaskSource!.File);
            Console.WriteLine($"Imprimiu: {fileAndTaskSource!.File.Content}");
        }
        else
        {
            // Console.WriteLine("Nada para imprimir");
        }
    }
}
