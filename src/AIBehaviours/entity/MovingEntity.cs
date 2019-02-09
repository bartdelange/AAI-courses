using AIBehaviours.Behaviour;
using System.Collections.Generic;
using System.Linq;
using AIBehaviours.Util;

namespace AIBehaviours.Entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        //
        public float Mass { get; set; } = 15;
        public float MaxSpeed { get; set; } = 100;
        public float Radius { get; set; } = 50;

        //
        public Vector2D Velocity { get; set; } = new Vector2D(1, 1);
        public Vector2D Heading { get; set; } = new Vector2D(1, 1);
        public Vector2D Side { get; set; } = new Vector2D(1, 1);

        //
        public List<SteeringBehaviour> SteeringBehaviours { get; set; } = new List<SteeringBehaviour>();

        protected MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
        }

        public override void Update(float delta)
        {
            FindNeighbours(Radius);
            
            var steeringForce = SteeringBehaviours.Aggregate(
                new Vector2D(),
                (force, behaviour) => force + behaviour.Calculate(delta)
            ) / SteeringBehaviours.Count;

            var acceleration = steeringForce / Mass;

            Velocity = acceleration * delta;
            Pos += Velocity * delta;

            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perpendicular();
            }

            Pos = WrapToBounds(Pos, MyWorld.Width, MyWorld.Height);
        }

        private static Vector2D WrapToBounds(Vector2D pos, int width, int height)
        {
            if (pos.X > width)
                return new Vector2D(0, pos.Y);

            if (pos.Y > height)
                return new Vector2D(pos.X, 0);

            if (pos.X < 0)
                return new Vector2D(width, pos.Y);

            if (pos.Y < 0)
                return new Vector2D(pos.X, height);

            return pos;
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}