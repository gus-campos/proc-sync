using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class DeadlockDiningTable : IDiningTable
{
    private readonly int _philosopherCount = 5;
    private readonly object[] _forks;
    private int _mealsEaten = 0;

    public DeadlockDiningTable()
    {
        _forks = new object[_philosopherCount];
        for (int i = 0; i < _philosopherCount; i++)
            _forks[i] = new object();
    }

    public void Dine(int millisecondsTimeout)
    {
        using var cts = new CancellationTokenSource();
        var tasks = new Task[_philosopherCount];

        for (int i = 0; i < _philosopherCount; i++)
        {
            int id = i;
            tasks[i] = Task.Run(() => Philosopher(id, cts.Token));
        }

        Task.WhenAny(Task.WhenAll(tasks), Task.Delay(millisecondsTimeout)).Wait();

        Console.WriteLine($"[DEADLOCK] Refeições realizadas: {_mealsEaten}");
    }

    private void Philosopher(int id, CancellationToken token)
    {
        int left = id;
        int right = (id + 1) % _philosopherCount;

        while (true)
        {
            Console.WriteLine($"[DEADLOCK] Filósofo {id} pensa...");
            Thread.Sleep(100);

            Monitor.Enter(_forks[left]);
            Console.WriteLine($"[DEADLOCK] Filósofo {id} pegou garfo esquerdo {left}");

            Monitor.Enter(_forks[right]);
            Console.WriteLine($"[DEADLOCK] Filósofo {id} pegou garfo direito {right}");

            try
            {
                Console.WriteLine($"[DEADLOCK] Filósofo {id} comendo...");
                Interlocked.Increment(ref _mealsEaten);
                Thread.Sleep(200);
            }
            finally
            {
                Monitor.Exit(_forks[right]);
                Monitor.Exit(_forks[left]);
            }
        }
    }
}