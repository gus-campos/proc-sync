// Ficheiro: ProcSync.Core/Domain/SafeDiningTable.cs
using System.Threading;

using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SafeDiningTable : IDiningTable
{
    private readonly int _philosopherCount = 5;
    private readonly object[] _forks;
    private int _mealsEaten = 0;

    public SafeDiningTable()
    {
        _forks = new object[_philosopherCount];
        for (int i = 0; i < _philosopherCount; i++)
            _forks[i] = new object();
    }

    public void Dine(int millisecondsTimeout)
    {
        using var cts = new CancellationTokenSource(millisecondsTimeout);
        var tasks = new Task[_philosopherCount];

        for (int i = 0; i < _philosopherCount; i++)
        {
            int id = i;
            tasks[i] = Task.Run(() => Philosopher(id, cts.Token));
        }

        Task.WaitAll(tasks);  // aqui as tasks terminam quando o token é cancelado
        Console.WriteLine($"[Solução] Refeições realizadas: {_mealsEaten}");
    }

    private void Philosopher(int id, CancellationToken token)
    {
        int left = id;
        int right = (id + 1) % _philosopherCount;

        // Assimetria: o último filósofo (id 4) pega o direito primeiro
        bool leftFirst = (id != _philosopherCount - 1);

        while (!token.IsCancellationRequested)
        {
            Console.WriteLine($"[Solução] Filósofo {id} pensa...");
            Thread.Sleep(100);

            object firstFork = leftFirst ? _forks[left] : _forks[right];
            object secondFork = leftFirst ? _forks[right] : _forks[left];

            Monitor.Enter(firstFork);
            // Importante: se não conseguirmos o segundo garfo imediatamente, não há deadlock
            // pois a assimetria quebra o ciclo.
            Monitor.Enter(secondFork);

            try
            {
                Console.WriteLine($"[Solução] Filósofo {id} comendo (garfos {left} e {right})...");
                Interlocked.Increment(ref _mealsEaten);
                Thread.Sleep(200);
            }
            finally
            {
                Monitor.Exit(secondFork);
                Monitor.Exit(firstFork);
            }
        }
    }
}