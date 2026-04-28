
using ProcSync.Core.Utils;

namespace ProcSync.Core.Interfaces;

public interface IBuffer<TItem>
{
    public bool TryPut(TItem item);
    public Result<TItem> TryGet();
}
