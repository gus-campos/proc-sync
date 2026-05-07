using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class BarberShop(int chairs = 5) : IBarberShop
{
    private int _waitingClients = 0;
    private readonly object _lock = new();

    // Constantes para tempos
    private const int _tempoEsperaMaximaMs = 500;   // tempo que o barbeiro espera antes de verificar novamente
    private const int _tempoAtendimentoMs = 200;     // tempo de corte de cabelo
    private const int _atrasoChegadaMs = 2;          // pequeno atraso aleatório na chegada

    public async Task RunAsync(int millisecondsTimeout)
    {
        using var cts = new CancellationTokenSource(millisecondsTimeout);
        var barberTask = Task.Run(() => BarberWorkAsync(cts.Token)); // barbeiro em thread separada

        var customers = new List<Task>();
        for (int i = 0; i < 100; i++) // 100 clientes tentam entrar
        {
            int id = i;
            customers.Add(Task.Run(async () => await CustomerAsync(id, cts.Token)));
        }

        try { await Task.WhenAll(customers); } catch (OperationCanceledException) { }

        cts.Cancel();
        lock (_lock) { Monitor.PulseAll(_lock); } // acorda o barbeiro para terminar

        try { await barberTask; } catch (OperationCanceledException) { }
    }

    private async Task BarberWorkAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            lock (_lock)
            {
                // Dorme enquanto não há clientes (bloqueio passivo)
                while (_waitingClients == 0 && !token.IsCancellationRequested)
                {
                    Console.WriteLine("[UNSAFE] Barbeiro a dormir...");
                    Monitor.Wait(_lock, _tempoEsperaMaximaMs);
                }
                if (token.IsCancellationRequested) break;
                if (_waitingClients > 0)
                {
                    Console.WriteLine($"[UNSAFE] Barbeiro a atender. Espera: {_waitingClients}");
                    _waitingClients--;
                }
            }
            await Task.Delay(_tempoAtendimentoMs, token); // simula o atendimento
        }
    }

    private async Task CustomerAsync(int id, CancellationToken token)
    {
        if (token.IsCancellationRequested) return;
        await Task.Delay(Random.Shared.Next(0, _atrasoChegadaMs), token); // chegada ligeiramente aleatória

        // ***** ZONA CRÍTICA (sem proteção adequada) *****
        lock (_lock)
        {
            if (_waitingClients < chairs)
            {
                _waitingClients++;
                Console.WriteLine($"[UNSAFE] Cliente {id} sentou-se. Espera: {_waitingClients}");
                if (_waitingClients > chairs)
                    Console.WriteLine($"[UNSAFE] *** ERRO: Excedeu cadeiras ({_waitingClients} > {chairs})! ***");
                Monitor.Pulse(_lock); // acorda o barbeiro
            }
            else
            {
                Console.WriteLine($"[UNSAFE] Cliente {id} foi embora (casa cheia).");
            }
        }
    }
}