namespace ProcSync.Core.Interfaces;

public interface IResource
{
    public void Write(string value);
    public string Read();
}