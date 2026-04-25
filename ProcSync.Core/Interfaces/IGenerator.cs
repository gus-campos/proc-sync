
namespace ProcSync.Core.Interfaces;

public interface IGenerator<TItem>
{
    public TItem GenerateNext();
}
