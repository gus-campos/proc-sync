
using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class ParkingLotHandler(int capacity, int carsAmount) : BaseHandler
{
    protected override async Task RunSimple()
    {
        var parkingLot = new ParkingLot(capacity);
        var parkingLotSimulator = new ParkingLotSimulator(parkingLot);
        await parkingLotSimulator.Run(carsAmount);
    }

    protected override async Task RunConcurrent()
    {
        var parkingLot = new ConcurrentParkingLot(capacity);
        var parkingLotSimulator = new ParkingLotSimulator(parkingLot);
        await parkingLotSimulator.Run(carsAmount);
    }
}
