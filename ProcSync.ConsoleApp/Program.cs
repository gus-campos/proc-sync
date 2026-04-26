using System.CommandLine;

using ProcSync.ConsoleApp.Commands;

namespace ProcSync.ConsoleApp;

public static class Program
{
    public static int Main(string[] args)
    {
        RootCommand rootCommand = RootCommandFactory.Create();
        return rootCommand.Parse(args).Invoke();
    }
}

