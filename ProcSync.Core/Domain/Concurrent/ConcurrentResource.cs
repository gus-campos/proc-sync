using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentResource : IResource
{
    private string _value = "---";
    private readonly SemaphoreSlim _writeSemaphore = new(1, 1); // semáforo que funciona como bloqueio de escrita
    private int _readerCount = 0;
    private readonly object _readerLock = new();

    private const int _tempoEscritaParcialMs = 100; // igual à versão simples

    public string Read()
    {
        EnterReadLock();
        try
        {
            return _value;
        }
        finally
        {
            ExitReadLock();
        }
    }

    public void Write(string value)
    {
        throw new NotImplementedException();
    }

    public async Task WriteAsync(string value)
    {
        await _writeSemaphore.WaitAsync(); // espera que não haja leitores nem outro escritor
        try
        {
            // Escrita atómica (do ponto de vista dos leitores)
            _value = $"{value}---";
            await Task.Delay(_tempoEscritaParcialMs);
            _value = $"{value}***";
            await Task.Delay(_tempoEscritaParcialMs);
            _value = $"{value}###";
            await Task.Delay(_tempoEscritaParcialMs);
        }
        finally
        {
            _writeSemaphore.Release();
        }
    }

    // --- Controle manual de leitores ---
    private void EnterReadLock()
    {
        lock (_readerLock)
        {
            if (++_readerCount == 1)
                _writeSemaphore.Wait(); // primeiro leitor bloqueia escritores
        }
    }

    private void ExitReadLock()
    {
        lock (_readerLock)
        {
            if (--_readerCount == 0)
                _writeSemaphore.Release(); // último leitor liberta escritores
        }
    }
}