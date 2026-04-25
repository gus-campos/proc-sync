using System.CommandLine;

using ProcSync.ConsoleApp.Cli.Commands;

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
