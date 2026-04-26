// ProcSync.Core/Interfaces/IBridge.cs
namespace ProcSync.Core.Interfaces;

public interface IBridge
{
    public void Enter(string direction);  // Norte-Sul ou Sul-Norte
    public void Exit();
}