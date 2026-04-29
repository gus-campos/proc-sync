using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class ParkingLotCommand
{
    public static Command Build()
    {
        // Option<int> stepsOption = new("--steps")
        // {
        //     Description = "Quantidade de passos",
        //     DefaultValueFactory = _ => 1000
        // };

        Command parkingLotCommand = new("parking-lot", "Simulações de estacionamento")
        {
            // stepsOption
        };

        parkingLotCommand.SetAction(async (p) =>
        {
            // p.GetValue(stepsOption);
            await ParkingLotHandler.Run(10, 1000);
        });

        return parkingLotCommand;
    }
}
