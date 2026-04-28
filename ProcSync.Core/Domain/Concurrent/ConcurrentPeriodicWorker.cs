
namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentPeriodicWorker(
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

        // Iniciar execução assíncrona e guardar a task
        _taskRunning = Task.Run(RunLoopAsync);
    }

    public async Task StopAsync()
    {
        if (_taskRunning == null)
        {
            throw new Exception("Não é possível encerrar agente que não está executando");
        }

        // Sinalizar parada
        Volatile.Write(ref _shouldStop, true);

        // esperar parar e resetar estado
        await _taskRunning;
        _taskRunning = null;
        _shouldStop = false;
    }

    private async Task RunLoopAsync()
    {
        while (!Volatile.Read(ref _shouldStop))
        {
            if (_timeToCheckInMs > 0)
            {
                await Task.Delay(_timeToCheckInMs);
            }
            else
            {
                // como "await Delay(0)" roda sincronamente
                // usa-se Yield para forçar assincronismo
                await Task.Yield();
            }

            await _workToDo();
        }
    }
}
