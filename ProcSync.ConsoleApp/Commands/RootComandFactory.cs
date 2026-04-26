using System.CommandLine;

namespace ProcSync.ConsoleApp.Commands;

public static class RootCommandFactory
{
    public static RootCommand Create()
    {
        RootCommand root = new("simulate");

        root.Subcommands.Add(CounterCommand.Build());
        root.Subcommands.Add(ProducerConsumerCommand.Build());

        return root;
    }
}
