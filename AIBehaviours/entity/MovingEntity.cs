using System.Collections.Generic;
using System.Linq;
using AIBehaviours.behaviour;
using AIBehaviours.util;
using AIBehaviours.world;

namespace AIBehaviours.entity
{
    internal abstract class MovingEntity : BaseGameEntity
    {
        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
        }

        public float Mass { get; set; } = 15;

        public float MaxSpeed { get; set; } = 50;

        public Vector2D Velocity { get; set; } = new Vector2D(0, 0);

        public Vector2D Heading { get; set; } = new Vector2D(0, 0);

        public List<SteeringBehaviour> SteeringBehaviours { get; set; } = new List<SteeringBehaviour>();

        public override void Update(float delta)
        {
            var steeringForce = SteeringBehaviours.Aggregate(
                    new Vector2D(),
                    (force, behaviour) => force.Add(behaviour.Calculate())
                )
                .Divide(SteeringBehaviours.Count)
                .Divide(Mass)
                .Multiply(delta);

            Velocity
                .Add(steeringForce)
                .Truncate(MaxSpeed);

            Pos.Add(Velocity.Multiply(delta));

            if (Velocity.LengthSquared() > 0.00000001) Heading = Velocity.Clone().Normalize();

            Pos = WrapToBounds(Pos, MyWorld.Width, MyWorld.Height);
        }

        private Vector2D WrapToBounds(Vector2D inPos, int width, int height)
        {
            var pos = inPos.Clone();

            if (pos.X > width)
            {
                pos.X = 0;
            }

            if (pos.Y > height)
            {
                pos.Y = 0;
            }

            if (pos.X < 0)
            {
                pos.X = width;
            }

            if (pos.Y < 0)
            {
                pos.Y = height;
            }

            return pos;
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}