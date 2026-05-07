using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentDiningTable : IDiningTable
{
    private readonly int _philosopherCount = 5;
    private readonly SemaphoreSlim[] _forks; // semáforos binários (1,1) representam os garfos
    private int _mealsEaten = 0;

    private const int _tempoPensarMs = 100;
    private const int _tempoComerMs = 200;

    public ConcurrentDiningTable()
    {
        _forks = new SemaphoreSlim[_philosopherCount];
        for (int i = 0; i < _philosopherCount; i++)
            _forks[i] = new SemaphoreSlim(1, 1); // garfo livre inicialmente
    }

    public void Dine(int millisecondsTimeout)
    {
        using var cts = new CancellationTokenSource(millisecondsTimeout);
        var tasks = new Task[_philosopherCount];

        for (int i = 0; i < _philosopherCount; i++)
        {
            int id = i;
            tasks[i] = Task.Run(async () => await PhilosopherAsync(id, cts.Token));
        }

        try
        {
            Task.WaitAll(tasks);
        }
        catch (AggregateException ex)
        {
            // Cancelamento esperado no final do timeout
            ex.Handle(inner => inner is OperationCanceledException);
        }

        Console.WriteLine($"[Solução] Refeições realizadas: {_mealsEaten}");
    }

    private async Task PhilosopherAsync(int id, CancellationToken token)
    {
        int left = id;
        int right = (id + 1) % _philosopherCount;

        // Quebra da condição de espera circular: o último filósofo pega o garfo direito primeiro
        bool leftFirst = (id != _philosopherCount - 1);
        SemaphoreSlim firstFork = leftFirst ? _forks[left] : _forks[right];
        SemaphoreSlim secondFork = leftFirst ? _forks[right] : _forks[left];

        while (!token.IsCancellationRequested)
        {
            Console.WriteLine($"[Solução] Filósofo {id} pensa...");
            await Task.Delay(_tempoPensarMs, token);

            // Adquire os dois garfos de forma assíncrona
            await firstFork.WaitAsync(token);
            await secondFork.WaitAsync(token);

            try
            {
                Console.WriteLine($"[Solução] Filósofo {id} comendo (garfos {left} e {right})...");
                Interlocked.Increment(ref _mealsEaten);
                await Task.Delay(_tempoComerMs, token);
            }
            finally
            {
                // Liberta os garfos sempre, mesmo em caso de erro
                secondFork.Release();
                firstFork.Release();
            }
        }
    }
}