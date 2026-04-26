
namespace ProcSync.Core.Domain;

public abstract class AsyncAgent(int timeToCheckInMs)
{
    private Task? _taskRunning = null;
    private bool _shouldStop = false;

    public bool IsRunning => _taskRunning != null;

    async public Task StartAsync()
    {
        if (_taskRunning != null)
        {
            throw new Exception("Não é possível iniciar agente que já está executando");
        }

        _taskRunning = RunLoopAsync();

        return;
    }

    async public Task StopAsync()
    {
        _shouldStop = true;

        if (_taskRunning == null)
        {
            throw new Exception("Não é possível encerrar agente que não está executando");
        }

        await _taskRunning;
        _taskRunning = null;
        _shouldStop = false;
    }

    async private Task RunLoopAsync()
    {
        while (!_shouldStop)
        {
            // await Task.Delay(0) roda sincronamente, então para forçar 
            // execução assíncrona é usado o yield nesses casos
            if (timeToCheckInMs > 0)
            {
                await Task.Delay(timeToCheckInMs);
            }
            else
            {
                await Task.Yield();
            }

            await Process();
        }
    }

    abstract protected Task Process();
}
