using System.Numerics;
using AICore.Entity.Dynamic;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        Vector2 StartPosition { get; }

        void Dribble(Ball ball);
        void Steal(Ball ball);
    }
}