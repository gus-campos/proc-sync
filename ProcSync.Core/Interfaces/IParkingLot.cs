namespace ProcSync.Core.Interfaces;

public record Car(string Plate);

public interface IParkingLot
{
    public bool TryPark(Car car);
    public void Unpark(Car car);
}
