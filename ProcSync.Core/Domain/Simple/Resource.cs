using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class Resource : IResource
{
    private string _value = "---";

    public string Read()
    {
        return _value;
    }

    public void Write(string value)
    {
        _value = $"{value}---";
        Thread.Sleep(100);
        _value = $"{value}***";
        Thread.Sleep(100);
        _value = $"{value}###";
        Thread.Sleep(100);
    }
}
