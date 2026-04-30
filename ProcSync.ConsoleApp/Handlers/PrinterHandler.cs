
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class PrinterHandler
{
    public static async Task Run(
        int printingTimeInMs,
        int checkingTimeInMs,
        int clientsAmount,
        int filesPerClient,
        int baseTimeToQueue
    )
    {
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
}
