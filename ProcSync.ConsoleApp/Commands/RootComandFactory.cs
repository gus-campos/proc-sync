using System.CommandLine;

namespace ProcSync.ConsoleApp.Commands;

public static class RootCommandFactory
{
    public static RootCommand Create()
    {
        RootCommand root = new("Simula processos que exigem sincronização");

        root.Subcommands.Add(CounterCommand.Build());
        root.Subcommands.Add(ProducerConsumerCommand.Build());
        root.Subcommands.Add(ReadersWrittersCommand.Build());
        root.Subcommands.Add(DinningPhilosofersCommand.Build());
        root.Subcommands.Add(SleepingBarberCommand.Build());
        root.Subcommands.Add(OneWayBridgeCommand.Build());
        root.Subcommands.Add(ParkingLotCommand.Build());
        root.Subcommands.Add(PrinterCommand.Build());

        return root;
    }
}
