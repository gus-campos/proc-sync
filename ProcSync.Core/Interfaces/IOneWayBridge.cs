
namespace ProcSync.Core.Interfaces;

public interface IOneWayBridge
{
    public void Enter(BridgeDirection direction);
    public void Exit();
}

public enum BridgeDirection
{
    NorthToSouth,
    SouthToNorth
}
