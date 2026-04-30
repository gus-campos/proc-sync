
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class PrinterHandler(
    int printingTimeInMs,
    int checkingTimeInMs,
    int clientsAmount,
    int filesPerClient,
    int baseTimeToQueue
) : BaseHandler
{
    protected override async Task RunSimple()
    {
        var printer = new Printer(printingTimeInMs, checkingTimeInMs);

        var printerSimulator = new PrinterSimulator(
            printer,
            clientsAmount,
            filesPerClient,
            baseTimeToQueue
        );

        await printerSimulator.Run();
    }

    protected override async Task RunConcurrent()
    {
        var printer = new ConcurrentPrinter(printingTimeInMs, checkingTimeInMs);

        var printerSimulator = new PrinterSimulator(
            printer,
            clientsAmount,
            filesPerClient,
            baseTimeToQueue
        );

        await printerSimulator.Run();
    }
}
