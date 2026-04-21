namespace ProcSync.Core.SharedCounter;

public class ConcurrentCounter : ICounter
{
    public int Count { get; private set; } = 0;

    private int _busyFlag = 0;

    public void Increment()
    {
        RunLocked(() => Count++);
    }

    public void Decrement()
    {
        RunLocked(() => Count--);
    }

    private void RunLocked(Action action)
    {
        // Enquanto não conseguir obter lock do estado
        while (!TryLock())
        {
            // Ceder execução para outra thread
            Thread.Yield();
        }

        action();
        Unlock();
    }

    private bool TryLock()
    {
        /* Tenta obter o lock do estado, retornando se houve sucesso */

        // Compara _busyFlag e troca valor, em uma instrução atômica
        int oldValue = Interlocked.CompareExchange(ref _busyFlag, 1, 0);
        bool success = oldValue == 0;
        return success;
    }

    private void Unlock()
    {
        Interlocked.Exchange(ref _busyFlag, 0);
    }
}
