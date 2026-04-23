
namespace ProcSync.Core.Shared;

public class Locker
{
    private int _busyFlag = 0;

    public void RunLocked(Action action)
    {
        RunLockedBase(action);
    }

    public TResult RunLocked<TResult>(Func<TResult> func)
    {
        /*
        * Sobrecarga que permite executar um lambda com retorno.
        * Internamente adapta a Func para uma Action, armazenando
        * o resultado em uma variável capturada pelo closure.
        */

        TResult result = default!;

        Action adapterAction = () =>
        {
            result = func();
        };

        RunLockedBase(adapterAction);

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

    private void RunLockedBase(Action action)
    {
        /* 
        * Executa o lambda passado, sem retorno, respeitando o lock do estado.
        * 
        * OBS: Enquanto ele não tiver o lock, esta thread cede a execução para 
        * outras threads.
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

    private void Unlock()
    {
        /* 
        Declara fim do lock do estado.

        OBS: O Exchange em apenas uma instrução atômica, seta a flag com valor 0  
        */

        Interlocked.Exchange(ref _busyFlag, 0);
    }
}
