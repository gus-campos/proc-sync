using ProcSync.Core.Domain.Concurrent;
using ProcSync.Core.Domain.Simple;
using ProcSync.Core.Simulators;

namespace ProcSync.ConsoleApp.Handlers;

public class OneWayBridgeHandler : BaseHandler
{
    // Parâmetros da simulação
    private const int _veiculosNorte = 15;         // veículos indo de Norte para Sul
    private const int _veiculosSul = 5;             // veículos indo de Sul para Norte
    private const int _atrasoInicialSulMs = 200;    // atraso extra dos veículos do Sul (ms)
    private const int _tempoTravessiaMs = 80;       // tempo para atravessar a ponte (ms)
    private const int _espacamentoMs = 20;          // intervalo entre partidas do mesmo sentido (ms)
    private const int _timeoutMs = 10000;           // tempo máximo total da simulação (10 segundos)

    protected override async Task RunSimple()
    {
        var bridge = new OneWayBridge(); // versão sem alternância forçada
        var sim = new BridgeSimulator(bridge, "SEM ALTERNÂNCIA");
        await sim.RunAsync(_veiculosNorte, _veiculosSul, _atrasoInicialSulMs, _tempoTravessiaMs, _espacamentoMs, _timeoutMs);
    }

    protected override async Task RunConcurrent()
    {
        var bridge = new ConcurrentOneWayBridge(); // versão com alternância e prevenção de inanição
        var sim = new BridgeSimulator(bridge, "COM ALTERNÂNCIA");
        await sim.RunAsync(_veiculosNorte, _veiculosSul, _atrasoInicialSulMs, _tempoTravessiaMs, _espacamentoMs, _timeoutMs);
    }
}