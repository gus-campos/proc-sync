using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class PrinterCommand
{
    public static Command Build()
    {
        var printingTimeOption = new Option<int>("--printing-time")
        {
            Description = "Tempo de impressão (ms) {default: 10}",
            DefaultValueFactory = _ => 10
        };

        var checkingTimeOption = new Option<int>("--checking-time")
        {
            Description = "Tempo de checagem (ms) {default: 10}",
            DefaultValueFactory = _ => 10
        };

        var clientsOption = new Option<int>("--clients-amount")
        {
            Description = "Quantidade de clientes {default: 100}",
            DefaultValueFactory = _ => 100
        };

        var filesPerClientOption = new Option<int>("--files-per-client")
        {
            Description = "Arquivos por cliente {default: 100}",
            DefaultValueFactory = _ => 100
        };

        var queueDelayOption = new Option<int>("--queue-delay")
        {
            Description = "Delay base para enfileirar (ms) {default: 100}",
            DefaultValueFactory = _ => 100
        };

        var printerCommand = new Command("printer", "Simula impressora")
        {
            printingTimeOption,
            checkingTimeOption,
            clientsOption,
            filesPerClientOption,
            queueDelayOption
        };

        printerCommand.SetAction(async (p) =>
        {
            PrintOptions.Print(p);

            var handler = new PrinterHandler(
                p.GetValue(printingTimeOption),
                p.GetValue(checkingTimeOption),
                p.GetValue(clientsOption),
                p.GetValue(filesPerClientOption),
                p.GetValue(queueDelayOption)
            );

            await handler.Run();
        });

        return printerCommand;
    }
}
