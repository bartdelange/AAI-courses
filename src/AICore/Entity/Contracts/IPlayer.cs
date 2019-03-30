using System.Numerics;
using AICore.Entity.Dynamic;
using AICore.Model;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        Vector2 StartPosition { get; }
        Team Team { get; set; }

        void Dribble(Ball ball);
        void Steal(Ball ball);
    }
}