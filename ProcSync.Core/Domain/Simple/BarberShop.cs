using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class BarberShop(int chairs = 5) : IBarberShop
{
    private int _waitingClients = 0;
    // private bool _barberSleeping = true;

    public void Run(int millisecondsTimeout)
    {
        var barberTask = Task.Run(BarberWork);
        var customers = new List<Task>();

        using var cts = new CancellationTokenSource(millisecondsTimeout);

        for (int i = 0; i < 100; i++)
        {
            int id = i;
            customers.Add(Task.Run(() =>
            {
                Customer(id);
            }));
        }

        Task.WaitAll([.. customers]);
        Thread.Sleep(500);
        cts.Cancel();
    }

    private void BarberWork()
    {
        while (true)
        {
            if (_waitingClients == 0)
            {
                Console.WriteLine("[UNSAFE] Barbeiro a dormir...");
                // _barberSleeping = true;
                while (_waitingClients == 0) { }
                // _barberSleeping = false;
            }

            int current = _waitingClients;
            Console.WriteLine($"[UNSAFE] Barbeiro a atender. Espera: {current}");
            _waitingClients--;
        }
    }

    private void Customer(int id)
    {
        if (_waitingClients < chairs)
        {
            Thread.Sleep(Random.Shared.Next(0, 2));
            _waitingClients++;
            Console.WriteLine($"[UNSAFE] Cliente {id} sentou-se. Espera: {_waitingClients}");
            if (_waitingClients > chairs)
                Console.WriteLine($"[UNSAFE] *** ERRO: Excedeu cadeiras ({_waitingClients} > {chairs})! ***");
        }
        else
        {
            Console.WriteLine($"[UNSAFE] Cliente {id} foi embora (casa cheia).");
        }
    }
}
