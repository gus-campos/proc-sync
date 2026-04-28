
namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentPeriodicWorker(
    Func<CancellationToken, Task> threadSafeWorkToBeDone,
    int timeToCheckInMs
)
{
    /*
    * O PeriodicWorker não é responsável por garantir a segurança e lock
    * no trabalho executado. 
    */

    private readonly int _timeToCheckInMs = timeToCheckInMs;
    private readonly Func<CancellationToken, Task> _workToDo = threadSafeWorkToBeDone;
    private readonly object _lock = new();

    private Task? _taskRunning = null;
    private CancellationTokenSource? _cts;
    // private bool _shouldStop = false;

    public void Start()
    {
        lock (_lock)
        {
            if (_taskRunning != null)
                throw new Exception("Não é possível iniciar agente que já está executando");

            // Iniciar execução assíncrona e armazenar a task
            _cts = new();
            _taskRunning = Task.Run(() => RunLoopAsync(_cts.Token));
        }
    }

    public async Task StopAsync()
    {
        Task taskToWait;

        lock (_lock)
        {
            if (_taskRunning == null)
                throw new Exception("Não é possível encerrar agente que não está executando");

            if (_cts == null || _cts.IsCancellationRequested)
                throw new Exception("Token não existe ou já foi cancelado.");

            // Cancelar token
            taskToWait = _taskRunning;
            _cts.Cancel();
        }

        await taskToWait;

        lock (_lock)
        {
            // Verifica novamente, para caso tenha reiniciado em outra thread
            if (_taskRunning == taskToWait)
                _taskRunning = null;
        }
    }

    private async Task RunLoopAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            // como "await Delay(0)" roda sincronamente, usa-se Yield para forçar assincronismo
            if (_timeToCheckInMs > 0)
                await Task.Delay(_timeToCheckInMs, token);
            else
                await Task.Yield();

            await _workToDo(token);
        }
    }
}
