
namespace ProcSync.ConsoleApp.Handlers;

public abstract class BaseHandler
{
    public async Task Run()
    {
        PrintParams();

        Console.WriteLine("\n=============== Simples ===============\n");

        await RunSimple();

        Console.WriteLine("\n=============== Thread-safe ===============\n");

        Thread.Sleep(1000);
        await RunConcurrent();
    }

    protected abstract void PrintParams();
    protected abstract Task RunSimple();
    protected abstract Task RunConcurrent();
}
