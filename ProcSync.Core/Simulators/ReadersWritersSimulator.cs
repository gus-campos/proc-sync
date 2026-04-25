// Ficheiro: ProcSync.Core/Simulators/ReadersWritersSimulator.cs
using System.Threading;

using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ReadersWritersSimulator
{
    private readonly IResource _resource;
    private readonly string _versionLabel; // para logs

    public ReadersWritersSimulator(IResource resource, string versionLabel)
    {
        _resource = resource;
        _versionLabel = versionLabel;
    }

    public void Run(int readerCount, int writerCount, int millisecondsToRun)
    {
        Console.WriteLine($"--- Iniciando {_versionLabel} ---");

        // CancellationToken para parar ao fim de um tempo
        using var cts = new CancellationTokenSource(millisecondsToRun);
        var token = cts.Token;

        var readerTasks = new Task[readerCount];
        var writerTasks = new Task[writerCount];

        // Criar leitores
        for (int i = 0; i < readerCount; i++)
        {
            int id = i;
            readerTasks[i] = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    string val = _resource.Read();
                    Console.WriteLine($"[{_versionLabel}] Leitor {id} leu: \"{val}\"");
                    Thread.Sleep(100); // simula tempo entre leituras
                }
            }, token);
        }

        // Criar escritores
        for (int i = 0; i < writerCount; i++)
        {
            int id = i;
            writerTasks[i] = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    _resource.Write($"W{id}");
                    Console.WriteLine($"[{_versionLabel}] Escritor {id} escreveu.");
                    Thread.Sleep(500);
                }
            }, token);
        }

        // Espera o tempo de simulação acabar
        Task.WhenAll(readerTasks).Wait();
        Task.WhenAll(writerTasks).Wait();
        Console.WriteLine($"--- Fim {_versionLabel} ---\n");
    }
}