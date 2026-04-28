
namespace ProcSync.Core.Domain.Simple;

public class PeriodicWorker(
    Func<Task> workToDo,
    int timeToCheckInMs
)
{
    private readonly int _timeToCheckInMs = timeToCheckInMs;
    private readonly Func<Task> _workToDo = workToDo;

    private Task? _taskRunning = null;
    private bool _shouldStop = false;

    public void Start()
    {
        if (_taskRunning != null)
        {
            throw new Exception("Não é possível iniciar agente que já está executando");
        }

        // Iniciar execução assíncrona e armazenar a task
        _taskRunning = Task.Run(RunLoopAsync);
    }

    public async Task StopAsync()
    {
        if (_taskRunning == null)
        {
            throw new Exception("Não é possível encerrar agente que não está executando");
        }

        // Sinalizar parada
        _shouldStop = true;

        // esperar parar e resetar estado
        await _taskRunning;
        _taskRunning = null;
        _shouldStop = false;
    }

    private async Task RunLoopAsync()
    {
        while (!_shouldStop)
        {
            // como "await Delay(0)" roda sincronamente, usa-se Yield para forçar assincronismo
            if (_timeToCheckInMs > 0)
                await Task.Delay(_timeToCheckInMs);
            else
                await Task.Yield();

            await _workToDo();
        }
    }
}
