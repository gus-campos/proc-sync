using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;


namespace ProcSync.ConsoleApp.Cli.Commands;

public static class CounterCommand
{
    public static Command Build()
    {
        Option<int> stepsOption = new("--steps")
        {
            Description = "Quantidade de passos",
            DefaultValueFactory = _ => 1000
        };

        Command counterCommand = new("counter", "Simulações de contagem")
        {
            stepsOption
        };

        counterCommand.SetAction(p =>
        {
            int steps = p.GetValue(stepsOption);
            CounterHandler.Run(steps);
        });

        return counterCommand;
    }
}
