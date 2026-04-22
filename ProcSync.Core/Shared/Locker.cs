
namespace ProcSync.Core.Shared;

public class Locker
{
    private int _busyFlag = 0;

    public TResult RunLocked<TResult>(Func<TResult> action)
    {
        // Enquanto não conseguir obter lock do estado
        while (!TryLock())
        {
            // Ceder execução para outra thread
            Thread.Yield();
        }

        TResult result;
        try
        {
            result = action();
        }
        finally
        {
            Unlock();
        }
        return result;
    }

    private bool TryLock()
    {
        /* 
        Tenta obter o lock do estado, retornando se houve sucesso.
        O CompareExchange em uma instrução atômica apenas, compara 
        _busyFlag com 0 e troca o valor pra 1, quando possível.
        */

        int oldValue = Interlocked.CompareExchange(ref _busyFlag, 1, 0);
        bool success = oldValue == 0;
        return success;
    }

    private void Unlock()
    {
        // Troca valor de _busyFlagh pra 0, em uma instrução atômica
        Interlocked.Exchange(ref _busyFlag, 0);
    }
}
