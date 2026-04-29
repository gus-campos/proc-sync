
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public static class ParkingLotHandler
{
    public static async Task Run(int capacity, int carsAmount)
    {
        var parkingLot = new ConcurrentParkingLot(capacity);
        var parkingLotSimulator = new ParkingLotSimulator(parkingLot);
        await parkingLotSimulator.Run(carsAmount);
    }
}
