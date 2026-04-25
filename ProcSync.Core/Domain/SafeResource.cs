// Ficheiro: ProcSync.Core/Domain/SafeResource.cs
using System.Threading;

using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain;

public class SafeResource : IResource
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
            Thread.Sleep(30);
            _value = $"{value}***";
            Thread.Sleep(30);
            _value = $"{value}###";
            Thread.Sleep(30);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }
}