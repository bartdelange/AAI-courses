using System.Collections.Generic;
using System.Numerics;
using AICore.SteeringBehaviour;

namespace AICore.Entity.Contracts
{
    public interface IMovingEntity : IEntity
    {
        ISteeringBehaviour SteeringBehaviour { get; set; }

        IEnumerable<IMiddleware> Middlewares { get; set; }

        Vector2 Velocity { get; set; }
        Vector2 Heading { get; set; }
        Vector2 SmoothHeading { get; set; }

        float MaxSpeed { get; set; }
        float Mass { get; set; }

        void Update(float timeDelta);
    }
}