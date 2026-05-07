using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class OneWayBridgeHandler : BaseHandler
{
    // Parâmetros da simulação
    private const int VeiculosNorte = 15;         // veículos indo de Norte para Sul
    private const int VeiculosSul = 5;             // veículos indo de Sul para Norte
    private const int AtrasoInicialSulMs = 200;    // atraso extra dos veículos do Sul (ms)
    private const int TempoTravessiaMs = 80;       // tempo para atravessar a ponte (ms)
    private const int EspacamentoMs = 20;          // intervalo entre partidas do mesmo sentido (ms)
    private const int TimeoutMs = 10000;           // tempo máximo total da simulação (10 segundos)

    protected override async Task RunSimple()
    {
        var bridge = new OneWayBridge(); // versão sem alternância forçada
        var sim = new BridgeSimulator(bridge, "SEM ALTERNÂNCIA");
        await sim.RunAsync(VeiculosNorte, VeiculosSul, AtrasoInicialSulMs, TempoTravessiaMs, EspacamentoMs, TimeoutMs);
    }

    protected override async Task RunConcurrent()
    {
        var bridge = new ConcurrentOneWayBridge(); // versão com alternância e prevenção de inanição
        var sim = new BridgeSimulator(bridge, "COM ALTERNÂNCIA");
        await sim.RunAsync(VeiculosNorte, VeiculosSul, AtrasoInicialSulMs, TempoTravessiaMs, EspacamentoMs, TimeoutMs);
    }
}