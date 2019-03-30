using System.Numerics;
using AICore.Behaviour.Goals;
using AICore.Behaviour.Goals.StrikerGoals;
using AICore.Entity.Dynamic;
using AICore.Model;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        new Vector2 StartPosition { get; }
        Team Team { get; set; }
        
        Think ThinkGoal { get; set; }

        float Energy { get; set; }

        void Steal(Ball ball);
    }
}