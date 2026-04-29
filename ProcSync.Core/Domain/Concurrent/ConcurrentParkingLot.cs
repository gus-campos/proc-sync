
using System.Collections.Concurrent;

using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentParkingLot(int capacity) : IParkingLot
{
    private readonly ConcurrentDictionary<string, Car> _carsByPlate = [];
    private readonly int _capacity = capacity;
    private readonly object _lock = new();

    private int _totalParked = 0;

    public bool TryPark(Car car)
    {
        lock (_lock)
        {
            if (_totalParked >= _capacity)
                return false;

            bool wasAdded = _carsByPlate.TryAdd(car.Plate, car);

            if (!wasAdded)
                throw new Exception("Não é possível estacionar carro já estacionado.");

            _totalParked++;
            return true;
        }
    }

    public void Unpark(Car car)
    {
        lock (_lock)
        {
            bool wasRemoved = _carsByPlate.TryRemove(car.Plate, out _);

            if (!wasRemoved)
                throw new Exception("Não é possível tirar carro não estacionado.");

            _totalParked--;
        }
    }
}
