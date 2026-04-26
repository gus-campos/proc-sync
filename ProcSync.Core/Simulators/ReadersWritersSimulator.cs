using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ReadersWritersSimulator(IResource resource, string versionLabel)
{
    public void Run(int readerCount, int writerCount, int millisecondsToRun)
    {
        Console.WriteLine($"--- Iniciando {versionLabel} ---");

        using var cts = new CancellationTokenSource(millisecondsToRun);
        var token = cts.Token;

        var readerTasks = new Task[readerCount];
        var writerTasks = new Task[writerCount];

        for (int i = 0; i < readerCount; i++)
        {
            int id = i;
            readerTasks[i] = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    string val = resource.Read();
                    Console.WriteLine($"[{versionLabel}] Leitor {id} leu: \"{val}\"");
                    Thread.Sleep(100);
                }
            }, token);
        }

        for (int i = 0; i < writerCount; i++)
        {
            int id = i;
            writerTasks[i] = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    resource.Write($"W{id}");
                    Console.WriteLine($"[{versionLabel}] Escritor {id} escreveu.");
                    Thread.Sleep(500);
                }
            }, token);
        }

        Task.WhenAll(readerTasks).Wait();
        Task.WhenAll(writerTasks).Wait();
        Console.WriteLine($"--- Fim {versionLabel} ---\n");
    }
}
