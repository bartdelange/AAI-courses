using System.Numerics;
using AICore.Behaviour.Goals;
using AICore.Entity.Dynamic;
using AICore.Model;

namespace AICore.Entity.Contracts
{
    public interface IPlayer : IMovingEntity
    {
        Team Team { get; set; }
        PlayerStrategy Strategy { get; }        
        
        Think ThinkGoal { get; set; }

        float Energy { get; set; }

        void Steal(Ball ball);
    }
}