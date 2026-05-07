namespace ProcSync.Core.Interfaces;

public interface IResource
{
    public string Read();
    public Task WriteAsync(string value);
}