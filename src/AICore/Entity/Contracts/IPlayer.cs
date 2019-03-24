using System.Numerics;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        Vector2 StartPosition { get; }
        
        IMovingEntity BallEntity { get; }

        float MaxEnergy { get; set; }
        float MinEnergy { get; set; }

        float Energy { get; set; }        
    }
}