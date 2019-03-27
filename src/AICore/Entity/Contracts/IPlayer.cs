using System.Numerics;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        Vector2 StartPosition { get; }
        
        IMovingEntity BallEntity { get; }

        float MaxEnergy { get; set; }
        float MinEnergy { get; set; }
        string TeamName { get; set; } 

        float Energy { get; set; }

        void KickBall(Ball ball, Vector2 position);
        void Dribble(Ball ball);
        void Steal(Ball ball);
    }
}