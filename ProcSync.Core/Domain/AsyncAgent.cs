
namespace ProcSync.Core.Domain;

public abstract class AsyncAgent(int timeToCheckInMs)
{
    private Task? _taskRunning = null;
    private bool _shouldStop = false;

    async public Task StartAsync()
    {
        if (_taskRunning != null)
            return;

        _taskRunning = RunLoopAsync();

        return;
    }

    async public Task StopAsync()
    {
        _shouldStop = true;

        if (_taskRunning != null)
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
