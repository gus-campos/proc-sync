// ProcSync.Core/Domain/SafeBridge.cs
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SafeBridge : IBridge
{
    private readonly object _lock = new();
    private string? _currentDirection = null;
    private int _vehiclesOnBridge = 0;
    private int _waitingNorth = 0;  // contagem de veículos Norte-Sul à espera
    private int _waitingSouth = 0;  // contagem de veículos Sul-Norte à espera

    public void Enter(string direction)
    {
        lock (_lock)
        {
            if (direction == "Norte-Sul")
                _waitingNorth++;
            else
                _waitingSouth++;

            // Enquanto não puder entrar (ponte ocupada ou sentido oposto tem preferência e há veículos nesse sentido)
            while (_currentDirection != null && _currentDirection != direction)
            {
                Monitor.Wait(_lock);
            }

            // Se chegou aqui, a ponte está livre ou é o seu sentido
            _currentDirection = direction;
            _vehiclesOnBridge++;

            if (direction == "Norte-Sul")
                _waitingNorth--;
            else
                _waitingSouth--;

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
                // dentro de Exit(), após _vehiclesOnBridge == 0
                if (_currentDirection == "Norte-Sul" && _waitingSouth > 0)
                {
                    Console.WriteLine("[SAFE] *** Trocando sentido para Sul-Norte ***");
                    _currentDirection = null;
                    Monitor.PulseAll(_lock);
                }
                else if (_currentDirection == "Sul-Norte" && _waitingNorth > 0)
                {
                    Console.WriteLine("[SAFE] *** Trocando sentido para Norte-Sul ***");
                    _currentDirection = null;
                    Monitor.PulseAll(_lock);
                }
                else
                {
                    // Não há ninguém do outro lado, ou não há ninguém à espera; liberta geral
                    _currentDirection = null;
                    Monitor.PulseAll(_lock);
                }
            }
        }
    }
}