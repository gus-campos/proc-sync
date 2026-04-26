using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class UnsafeBridge : IBridge
{
    private readonly object _bridgeLock = new();
    private string? _currentDirection = null;
    private int _vehiclesOnBridge = 0;

    public void Enter(string direction)
    {
        lock (_bridgeLock)
        {
            if (_currentDirection == null || _currentDirection == direction)
            {
                _currentDirection = direction;
                _vehiclesOnBridge++;
                Console.WriteLine($"[UNSAFE] Veículo entrou na ponte. Direção: {direction}. Veículos na ponte: {_vehiclesOnBridge}");
            }
            else
            {
                while (_currentDirection != direction && _vehiclesOnBridge > 0)
                {
                    Monitor.Wait(_bridgeLock);
                }
                if (_currentDirection == null || _currentDirection == direction)
                {
                    _currentDirection = direction;
                    _vehiclesOnBridge++;
                    Console.WriteLine($"[UNSAFE] Veículo entrou na ponte. Direção: {direction}. Veículos na ponte: {_vehiclesOnBridge}");
                }
            }
        }
    }

    public void Exit()
    {
        lock (_bridgeLock)
        {
            _vehiclesOnBridge--;
            Console.WriteLine($"[UNSAFE] Veículo saiu da ponte. Veículos restantes: {_vehiclesOnBridge}");
            if (_vehiclesOnBridge == 0)
            {
                _currentDirection = null;
                Monitor.PulseAll(_bridgeLock);
            }
        }
    }
}