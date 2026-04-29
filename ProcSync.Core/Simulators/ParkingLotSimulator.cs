
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ParkingLotSimulator(IParkingLot parkingLot)
{
    private readonly Random _random = new();

    public async Task Run(int carsAmount)
    {
        var cars = Enumerable.Range(0, carsAmount)
            .Select(index =>
            {
                string plate = GenerateSerialPlate(index);
                return new Car(plate);
            }
            ).ToList();

        var tasks = cars.Select(SimulateCarPark);

        await Task.WhenAll(tasks);
    }

    private int GetRandomTimeInMs()
    {
        return _random.Next(100, 1000);
    }

    private static string GenerateSerialPlate(int i)
    {
        return $"ABC-{i:0000}";
    }

    private async Task SimulateCarPark(Car car)
    {
        await Task.Delay(GetRandomTimeInMs());

        if (parkingLot.TryPark(car))
        {
            Console.WriteLine($"{car.Plate} estacionou");

            await Task.Delay(GetRandomTimeInMs());

            parkingLot.Unpark(car);
            Console.WriteLine($"{car.Plate} saiu");
        }
        else
        {
            Console.WriteLine($"{car.Plate} não conseguiu vaga");
        }
    }
}
