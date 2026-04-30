namespace ProcSync.Core.Interfaces;

public record PrinterFile(string Content);

public interface IPrinter
{
    public Task<PrinterFile> QueuePrint(PrinterFile file);
    public void Start();
    public Task Stop();
}
