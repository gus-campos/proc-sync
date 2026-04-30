using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;


namespace ProcSync.ConsoleApp.Commands;

public static class CounterCommand
{
    public static Command Build()
    {
        Option<int> stepsOption = new("--steps")
        {
            Description = "Quantidade de passos",
            DefaultValueFactory = _ => 1000
        };

        Command counterCommand = new("counter", "Simula contagem")
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
