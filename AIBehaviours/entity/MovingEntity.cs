using AIBehaviours.behaviour;
using AIBehaviours.util;
using AIBehaviours.world;
using System.Collections.Generic;
using System.Linq;

namespace AIBehaviours.entity
{
    internal abstract class MovingEntity : BaseGameEntity
    {
        public float Mass { get; set; } = 15;

        public float MaxSpeed { get; set; } = 50;

        public Vector2D Velocity { get; set; } = new Vector2D(0, 0);

        public Vector2D Heading { get; set; } = new Vector2D(0, 0);

        public List<SteeringBehaviour> SteeringBehaviours { get; set; } = new List<SteeringBehaviour>();

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
        }

        public override void Update(float delta)
        {
            Vector2D steeringForce = SteeringBehaviours.Aggregate(
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

            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Clone().Normalize();
            }

            WrapAround(Pos, MyWorld.Width, MyWorld.Height);
        }

        private void WrapAround(Vector2D pos, int width, int height)
        {
            double ratio = width / (double)height;

            if (pos.X > width)
            {
                pos.X = width - Pos.Y;
                pos.Y = 0;
                return;
            }

            if (pos.Y > height)
            {
                pos.Y = height - Pos.X;
                pos.X = 0;
                return;
            }

            if (pos.X < 0)
            {
                pos.Y = width - Pos.Y;
                pos.X = width;
                return;
            }

            if (pos.Y < 0)
            {
                pos.X = height - Pos.X;
                pos.Y = height;
                return;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", Velocity);
        }
    }
}
