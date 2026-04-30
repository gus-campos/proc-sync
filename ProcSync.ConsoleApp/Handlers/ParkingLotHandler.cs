
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class ParkingLotHandler
{
    public static async Task Run(int capacity, int carsAmount)
    {
        {
            var parkingLot = new ParkingLot(capacity);
            var parkingLotSimulator = new ParkingLotSimulator(parkingLot);
            await parkingLotSimulator.Run(carsAmount);
        }

        {
            var parkingLot = new ConcurrentParkingLot(capacity);
            var parkingLotSimulator = new ParkingLotSimulator(parkingLot);
            await parkingLotSimulator.Run(carsAmount);
        }
    }
}
