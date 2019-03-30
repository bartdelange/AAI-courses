using System.Numerics;
using AICore.Entity.Dynamic;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        Vector2 StartPosition { get; }
        
        float MaxEnergy { get; set; }
        float MinEnergy { get; set; }

        float Energy { get; set; }

        void KickBall(Ball ball, Vector2 position);
        void Dribble(Ball ball);
        void Steal(Ball ball);
    }
}