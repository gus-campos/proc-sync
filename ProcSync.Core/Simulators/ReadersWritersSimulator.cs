using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Simulators;

public class ReadersWritersSimulator(IResource resource, string versionLabel)
{
    private const int _intervaloLeituraMs = 100;  // tempo entre leituras de cada leitor
    private const int _intervaloEscritaMs = 500;  // tempo entre escritas de cada escritor

    public async Task RunAsync(int readerCount, int writerCount, int millisecondsToRun)
    {
        Console.WriteLine($"--- Iniciando {versionLabel} ---");

        using var cts = new CancellationTokenSource(millisecondsToRun);
        var token = cts.Token;

        var readerTasks = new Task[readerCount];
        var writerTasks = new Task[writerCount];

        // Criação dos leitores
        for (int i = 0; i < readerCount; i++)
        {
            int id = i;
            readerTasks[i] = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    string val = resource.Read();
                    Console.WriteLine($"[{versionLabel}] Leitor {id} leu: \"{val}\"");
                    await Task.Delay(_intervaloLeituraMs, token);
                }
            }, token);
        }

        // Criação dos escritores
        for (int i = 0; i < writerCount; i++)
        {
            int id = i;
            writerTasks[i] = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await resource.WriteAsync($"W{id}");
                    Console.WriteLine($"[{versionLabel}] Escritor {id} escreveu.");
                    await Task.Delay(_intervaloEscritaMs, token);
                }
            }, token);
        }

        try
        {
            await Task.WhenAll(readerTasks);
            await Task.WhenAll(writerTasks);
        }
        catch (OperationCanceledException) { } // esperado após o timeout

        Console.WriteLine($"--- Fim {versionLabel} ---\n");
    }
}