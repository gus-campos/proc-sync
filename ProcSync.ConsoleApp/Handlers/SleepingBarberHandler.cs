using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class SleepingBarberHandler : BaseHandler
{
    private const int TimeoutSimplesMs = 5000;   // 5 segundos para a versão sem sincronização
    private const int TimeoutConcorrenteMs = 15000; // 15 segundos para a versão concorrente (clientes chegam espaçados)

    protected override async Task RunSimple()
    {
        var shop = new BarberShop(chairs: 5); // sem lock adequado → possível excesso de clientes
        var sim = new BarberSimulator(shop, "SEM SINCRONIZAÇÃO");
        await sim.RunAsync(timeoutMs: TimeoutSimplesMs);
    }

    protected override async Task RunConcurrent()
    {
        var shop = new ConcurrentBarberShop(chairs: 5); // lock + Monitor → sem condições de corrida
        var sim = new BarberSimulator(shop, "COM SINCRONIZAÇÃO");
        await sim.RunAsync(timeoutMs: TimeoutConcorrenteMs);
    }
}