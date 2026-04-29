
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class ParkingLot(int capacity) : IParkingLot
{
    private readonly List<Car> _cars = [];
    private readonly int _capacity = capacity;

    public bool TryPark(Car car)
    {
        if (IsParked(car))
            throw new Exception("Não é possível estacionar carro já estacionado.");

        if (_cars.Count >= _capacity)
            return false;

        _cars.Add(car);
        return true;
    }

    public void Unpark(Car car)
    {
        if (!IsParked(car))
            throw new Exception("Não é possível tirar carro não estacionado.");

        _cars.Add(car);
    }

    private bool IsParked(Car car)
    {
        var carFound = _cars.Find(carParked => carParked.Plate == car.Plate);
        return carFound != null;
    }
}
