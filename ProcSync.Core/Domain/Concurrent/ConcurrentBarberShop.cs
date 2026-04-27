using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentBarberShop : IBarberShop
{
    private readonly int _chairs;
    private int _waitingClients = 0;
    private readonly object _lock = new object();

    public ConcurrentBarberShop(int chairs = 5)
    {
        _chairs = chairs;
    }

    public void Run(int millisecondsTimeout)
    {
        using var cts = new CancellationTokenSource(millisecondsTimeout);
        var barberTask = Task.Run(() => BarberWork(cts.Token));

        var customers = new List<Task>();
        var random = new Random();

        for (int i = 0; i < 30; i++)
        {
            int id = i;
            customers.Add(Task.Run(() =>
            {
                if (cts.Token.IsCancellationRequested) return;
                Customer(id, cts.Token);
            }));
            Thread.Sleep(random.Next(100, 300)); // intervalo normal
        }

        Task.WaitAll(customers.ToArray());

        cts.Cancel();
        lock (_lock)
        {
            Monitor.PulseAll(_lock); // acorda o barbeiro se estiver em Wait
        }

        barberTask.Wait();
    }

    private void BarberWork(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            lock (_lock)
            {
                while (_waitingClients == 0 && !token.IsCancellationRequested)
                {
                    Console.WriteLine("[SAFE] Barbeiro a dormir...");
                    Monitor.Wait(_lock, 500);
                }

                if (token.IsCancellationRequested) break;

                if (_waitingClients > 0)
                {
                    Console.WriteLine($"[SAFE] Barbeiro a atender cliente. Clientes à espera: {_waitingClients}");
                    _waitingClients--;
                }
            }

            Thread.Sleep(200);
        }
    }

    private void Customer(int id, CancellationToken token)
    {
        if (token.IsCancellationRequested) return;

        lock (_lock)
        {
            if (_waitingClients < _chairs)
            {
                _waitingClients++;
                Console.WriteLine($"[SAFE] Cliente {id} sentou-se. Espera: {_waitingClients}");

                if (_waitingClients == 1)
                {
                    Console.WriteLine($"[SAFE] Cliente {id} acordou o barbeiro.");
                    Monitor.Pulse(_lock);
                }
            }
            else
            {
                Console.WriteLine($"[SAFE] Cliente {id} foi embora (casa cheia).");
            }
        }
    }
}
