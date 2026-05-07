using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class ReadersWrittersHandlers : BaseHandler
{
    // Parâmetros da simulação
    private const int NumeroLeitores = 5;           // threads leitoras concorrentes
    private const int NumeroEscritores = 2;         // threads escritoras concorrentes
    private const int DuracaoMs = 5000;             // tempo total de cada simulação (5 segundos)

    protected override async Task RunSimple()
    {
        var resource = new Resource(); // versão sem sincronização – permite leituras inconsistentes
        var sim = new ReadersWritersSimulator(resource, "SEM SINCRONIZAÇÃO");
        await sim.RunAsync(readerCount: NumeroLeitores, writerCount: NumeroEscritores, millisecondsToRun: DuracaoMs);
    }

    protected override async Task RunConcurrent()
    {
        var resource = new ConcurrentResource(); // versão com bloqueio leitor‑escritor assíncrono
        var sim = new ReadersWritersSimulator(resource, "COM SINCRONIZAÇÃO");
        await sim.RunAsync(readerCount: NumeroLeitores, writerCount: NumeroEscritores, millisecondsToRun: DuracaoMs);
    }
}