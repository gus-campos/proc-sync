// ProcSync.Core/Domain/UnsafeBarberShop.cs
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class UnsafeBarberShop : IBarberShop
{
    private readonly int _chairs;
    private int _waitingClients = 0;
    private bool _barberSleeping = true;

    public UnsafeBarberShop(int chairs = 5)
    {
        _chairs = chairs;
    }

    public void Run(int millisecondsTimeout)
    {
        var barberTask = Task.Run(BarberWork);
        var customers = new List<Task>();

        using var cts = new CancellationTokenSource(millisecondsTimeout);

        // Lança todos os clientes concorrentemente para gerar contenção máxima
        for (int i = 0; i < 30; i++)
        {
            int id = i;
            customers.Add(Task.Run(() =>
            {
                // Pequeno sleep variável (0-1ms) para aumentar ainda mais a aleatoriedade
                Thread.Sleep(Random.Shared.Next(0, 2));
                Customer(id);
            }));
        }

        Task.WaitAll(customers.ToArray());
        // Espera um pouco para o barbeiro atender os que ficaram
        Thread.Sleep(2000);
    }

    private void BarberWork()
    {
        while (true)
        {
            if (_waitingClients == 0)
            {
                Console.WriteLine("[UNSAFE] Barbeiro a dormir...");
                _barberSleeping = true;
                // Espera ativa – demonstra ineficiência mas não é o foco
                while (_waitingClients == 0)
                    Thread.Sleep(10);
                _barberSleeping = false;
            }

            Console.WriteLine($"[UNSAFE] Barbeiro a atender cliente. Clientes à espera: {_waitingClients}");
            _waitingClients--;  // condição de corrida!
            Thread.Sleep(200);
        }
    }

    private void Customer(int id)
    {
        Console.WriteLine($"[UNSAFE] Cliente {id} chegou.");

        // Leitura e incremento não atómicos -> condição de corrida
        if (_waitingClients < _chairs)
        {
            _waitingClients++;
            Console.WriteLine($"[UNSAFE] Cliente {id} sentou-se. Espera: {_waitingClients}");
            if (_waitingClients > _chairs)
                Console.WriteLine($"[UNSAFE] *** ERRO: Número de clientes à espera ({_waitingClients}) excede as cadeiras ({_chairs})! ***");
            if (_barberSleeping)
            {
                Console.WriteLine($"[UNSAFE] Cliente {id} acordou o barbeiro.");
                _barberSleeping = false;
            }
        }
        else
        {
            Console.WriteLine($"[UNSAFE] Cliente {id} foi embora (casa cheia).");
        }
    }
}