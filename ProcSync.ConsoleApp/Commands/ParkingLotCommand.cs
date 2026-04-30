using System.CommandLine;

using ProcSync.ConsoleApp.Handlers;

namespace ProcSync.ConsoleApp.Commands;

public static class ParkingLotCommand
{
    public static Command Build()
    {
        Option<int> capacityOption = new("--capacity")
        {
            Description = "Capacidade do estacionamento {default: 10}",
            DefaultValueFactory = _ => 10
        };

        Option<int> carsAmountOption = new("--cars-amount")
        {
            Description = "Quantidade de carros na simulação {default: 1000}",
            DefaultValueFactory = _ => 1000
        };

        Command parkingLotCommand = new("parking-lot", "Simula estacionamento")
        {
            capacityOption,
            carsAmountOption
        };

        parkingLotCommand.SetAction(async (p) =>
        {
            PrintOptions.Print(p);

            var handler = new ParkingLotHandler(
                p.GetValue(capacityOption),
                p.GetValue(carsAmountOption)
            );

            await handler.Run();
        });

        return parkingLotCommand;
    }
}
