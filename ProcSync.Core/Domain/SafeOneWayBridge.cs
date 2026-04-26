using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SafeOneWayBridge : IOneWayBridge
{
    private readonly object _lock = new();
    private BridgeDirection? _currentDirection = null;
    private int _vehiclesOnBridge = 0;
    private int _waitingNorth = 0;
    private int _waitingSouth = 0;

    public void Enter(BridgeDirection direction)
    {
        lock (_lock)
        {
            if (direction == BridgeDirection.NorthToSouth)
            {
                _waitingNorth++;
            }
            else
            {
                _waitingSouth++;
            }

            while (_currentDirection != null && _currentDirection != direction)
            {
                Monitor.Wait(_lock);
            }

            _currentDirection = direction;
            _vehiclesOnBridge++;

            if (direction == BridgeDirection.NorthToSouth)
            {
                _waitingNorth--;
            }
            else
            {
                _waitingSouth--;
            }

            Console.WriteLine($"[SAFE] Veículo entrou na ponte. Direção: {direction}. Veículos na ponte: {_vehiclesOnBridge}");
        }
    }

    public void Exit()
    {
        lock (_lock)
        {
            _vehiclesOnBridge--;
            Console.WriteLine($"[SAFE] Veículo saiu da ponte. Veículos restantes: {_vehiclesOnBridge}");

            if (_vehiclesOnBridge == 0)
            {
                if (_currentDirection == BridgeDirection.NorthToSouth && _waitingSouth > 0)
                {
                    Console.WriteLine("[SAFE] *** Trocando sentido para Sul-Norte ***");
                    _currentDirection = null;
                    Monitor.PulseAll(_lock);
                }
                else if (_currentDirection == BridgeDirection.SouthToNorth && _waitingNorth > 0)
                {
                    Console.WriteLine("[SAFE] *** Trocando sentido para Norte-Sul ***");
                    _currentDirection = null;
                    Monitor.PulseAll(_lock);
                }
                else
                {
                    _currentDirection = null;
                    Monitor.PulseAll(_lock);
                }
            }
        }
    }
}
