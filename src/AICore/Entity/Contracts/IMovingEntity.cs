using System.Numerics;
using AICore.SteeringBehaviour;

namespace AICore.Entity.Contracts
{
    public interface IMovingEntity : IEntity
    {
        ISteeringBehaviour SteeringBehaviour { get; set; }

        Vector2 Velocity { get; set; }
        Vector2 Heading { get; set; }
        Vector2 Side { get; set; }

        float MaxSpeed { get; set; }
        float Mass { get; set; }

        void Update(float timeDelta);
    }
}