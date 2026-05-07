using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentBarberShop(int chairs = 5) : IBarberShop
{
    private int _waitingClients = 0;
    private readonly object _lock = new();

    private const int _tempoEsperaBarbeiroMs = 500;
    private const int _tempoAtendimentoMs = 200;
    private const int _totalClientes = 30;          // número de clientes na simulação
    private const int _minIntervaloChegadaMs = 100; // intervalo mínimo entre chegadas
    private const int _maxIntervaloChegadaMs = 300; // intervalo máximo entre chegadas
    private const int _atrasoChegadaMs = 2;         // pequeno atraso aleatório antes de tentar sentar

    public async Task RunAsync(int millisecondsTimeout)
    {
        using var cts = new CancellationTokenSource(millisecondsTimeout);
        // Barbeiro é iniciado numa thread separada para não bloquear a thread principal
        var barberTask = Task.Run(() => BarberWorkAsync(cts.Token));

        var random = new Random();
        var customerTasks = new List<Task>();

        // Clientes chegam com intervalo aleatório (sem token, para não serem cancelados prematuramente)
        for (int i = 0; i < _totalClientes; i++)
        {
            int id = i;
            customerTasks.Add(CustomerAsync(id));
            await Task.Delay(random.Next(_minIntervaloChegadaMs, _maxIntervaloChegadaMs));
        }

        // Aguarda todos os clientes terminarem as suas tentativas
        await Task.WhenAll(customerTasks);

        cts.Cancel();
        lock (_lock) { Monitor.PulseAll(_lock); } // acorda o barbeiro para terminar

        try { await barberTask; }
        catch (OperationCanceledException) { }
    }

    private async Task BarberWorkAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            lock (_lock)
            {
                // Dorme (Wait) enquanto não há clientes – thread fica passiva
                while (_waitingClients == 0 && !token.IsCancellationRequested)
                {
                    Console.WriteLine("[SAFE] Barbeiro a dormir...");
                    Monitor.Wait(_lock, _tempoEsperaBarbeiroMs);
                }
                if (token.IsCancellationRequested) break;
                if (_waitingClients > 0)
                {
                    Console.WriteLine($"[SAFE] Barbeiro a atender cliente. Clientes à espera: {_waitingClients}");
                    _waitingClients--;
                }
            }
            // Atendimento decorre fora do lock para permitir novas chegadas
            await Task.Delay(_tempoAtendimentoMs, token);
        }
    }

    private async Task CustomerAsync(int id)
    {
        await Task.Delay(Random.Shared.Next(0, _atrasoChegadaMs)); // atraso mínimo, sem token

        // *** ZONA CRÍTICA protegida por lock ***
        lock (_lock)
        {
            if (_waitingClients < chairs)
            {
                _waitingClients++;
                Console.WriteLine($"[SAFE] Cliente {id} sentou-se. Espera: {_waitingClients}");
                if (_waitingClients == 1)
                {
                    Console.WriteLine($"[SAFE] Cliente {id} acordou o barbeiro.");
                    Monitor.Pulse(_lock); // acorda o barbeiro se ele estava a dormir
                }
            }
            else
            {
                Console.WriteLine($"[SAFE] Cliente {id} foi embora (casa cheia).");
            }
        }
    }
}