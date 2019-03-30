using System.Numerics;
using AICore.Entity.Dynamic;
using AICore.Model;
using AICore.Worlds;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        Vector2 StartPosition { get; }
        Team Team { get; set; }
        SoccerField SoccerField { get; set; }

        void Dribble(Ball ball);
        void Steal(Ball ball);
    }
}