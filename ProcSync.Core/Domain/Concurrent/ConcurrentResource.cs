
using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Concurrent;

public class ConcurrentResource : IResource
{
    private string _value = "---";
    private readonly ReaderWriterLockSlim _rwLock = new();

    public string Read()
    {
        _rwLock.EnterReadLock();
        try
        {
            return _value;
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public void Write(string value)
    {
        _rwLock.EnterWriteLock();
        try
        {
            _value = $"{value}---";
            Thread.Sleep(100);
            _value = $"{value}***";
            Thread.Sleep(100);
            _value = $"{value}###";
            Thread.Sleep(100);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }
}
