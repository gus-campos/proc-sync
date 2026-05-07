using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class DiningTable : IDiningTable
{
    private readonly int _philosopherCount = 5;
    private readonly object[] _forks;
    private int _mealsEaten = 0;

    // Constantes de tempo
    private const int _tempoPensarMs = 100;   // tempo que o filósofo pensa
    private const int _tempoComerMs = 200;    // tempo que o filósofo come

    public DiningTable()
    {
        _forks = new object[_philosopherCount];
        for (int i = 0; i < _philosopherCount; i++)
            _forks[i] = new object(); // cada garfo é um trinco exclusivo
    }

    public void Dine(int millisecondsTimeout)
    {
        var cts = new CancellationTokenSource(millisecondsTimeout);
        var tasks = new Task[_philosopherCount];

        for (int i = 0; i < _philosopherCount; i++)
        {
            int id = i;
            tasks[i] = Task.Run(async () => await PhilosopherAsync(id, cts.Token));
        }

        // Como pode haver deadlock, esperamos apenas o tempo limite
        Task.WhenAny(Task.WhenAll(tasks), Task.Delay(millisecondsTimeout)).Wait();
        Console.WriteLine($"[DEADLOCK] Refeições realizadas: {_mealsEaten}");
    }

    private async Task PhilosopherAsync(int id, CancellationToken token)
    {
        int left = id;                         // garfo esquerdo = próprio id
        int right = (id + 1) % _philosopherCount; // garfo direito = id seguinte (circular)

        while (!token.IsCancellationRequested)
        {
            Console.WriteLine($"[DEADLOCK] Filósofo {id} pensa...");
            await Task.Delay(_tempoPensarMs, token);

            // Todos pegam o esquerdo primeiro → risco de espera circular (deadlock)
            Monitor.Enter(_forks[left]);
            Console.WriteLine($"[DEADLOCK] Filósofo {id} pegou garfo esquerdo {left}");

            Monitor.Enter(_forks[right]);
            Console.WriteLine($"[DEADLOCK] Filósofo {id} pegou garfo direito {right}");

            try
            {
                Console.WriteLine($"[DEADLOCK] Filósofo {id} comendo...");
                Interlocked.Increment(ref _mealsEaten);
                await Task.Delay(_tempoComerMs, token);
            }
            finally
            {
                Monitor.Exit(_forks[right]);
                Monitor.Exit(_forks[left]);
            }
        }
    }
}