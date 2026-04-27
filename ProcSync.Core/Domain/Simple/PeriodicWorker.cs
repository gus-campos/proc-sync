
namespace ProcSync.Core.Domain.Simple;

public class PeriodicWorker(
    Func<Task> workToDo,
    int timeToCheckInMs
)
{
    private Task? _taskRunning = null;
    private bool _shouldStop = false;

    private readonly int _timeToCheckInMs = timeToCheckInMs;
    private readonly Func<Task> _workToDo = workToDo;

    public bool IsRunning => _taskRunning != null;

    public void Start()
    {
        if (_taskRunning != null)
        {
            throw new Exception("Não é possível iniciar agente que já está executando");
        }

        _taskRunning = Task.Run(RunLoopAsync);
    }

    public async Task StopAsync()
    {
        if (_taskRunning == null)
        {
            throw new Exception("Não é possível encerrar agente que não está executando");
        }

        _shouldStop = true;
        await _taskRunning;
        _taskRunning = null;
        _shouldStop = false;
    }

    private async Task RunLoopAsync()
    {
        while (!_shouldStop)
        {
            // await Task.Delay(0) roda sincronamente, então para forçar 
            // execução assíncrona é usado o yield nesses casos
            if (_timeToCheckInMs > 0)
            {
                await Task.Delay(_timeToCheckInMs);
            }
            else
            {
                await Task.Yield();
            }

            await _workToDo();
        }
    }
}
