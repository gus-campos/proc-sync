
namespace ProcSync.Core.Shared;

public class Locker
{
    private int _busyFlag = 0;

    private void RunLockedBase(Action action)
    {
        /* 
        Executa o lambda passado, respeitando o lock do estado.
        
        OBS: Enquanto ele não tiver o lock, esta thread cede a execução para 
        outras threads.
        */

        while (!TryLock())
        {
            Thread.Yield();
        }

        try
        {
            action();
        }
        finally
        {
            Unlock();
        }
    }

    public void RunLocked(Action action)
    {
        RunLockedBase(action);
    }

    public TResult RunLocked<TResult>(Func<TResult> action)
    {
        /* Sobrecarga que permite execução de um lambda com retorno */

        TResult result = default!;

        RunLockedBase(() =>
        {
            result = action();
        });

        return result;
    }

    private bool TryLock()
    {
        /* 
        Tenta obter o lock do estado, retornando se houve sucesso.
        
        OBS: Aqui o CompareExchange em apenas uma instrução atômica, compara 
        a flag com 0 e se possível troca o seu valor pra 1.
        */

        int oldValue = Interlocked.CompareExchange(ref _busyFlag, 1, 0);
        bool success = oldValue == 0;
        return success;
    }

    private void Unlock()
    {
        /* 
        Declara fim do lock do estado.

        OBS: O Exchange em apenas uma instrução atômica, seta a flag com valor 0  
        */

        Interlocked.Exchange(ref _busyFlag, 0);
    }
}
