using System.Collections.Generic;
using System.Linq;
using AICore.Behaviour;
using AICore.Util;

namespace AICore.Entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        protected MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
        }

        //	
        public float Mass { get; set; } = 15;
        public float MaxSpeed { get; set; } = 100;
        public int Radius { get; set; } = 100;

        //	
        public Vector2D Velocity { get; set; } = new Vector2D(1, 1);
        public Vector2D Heading { get; set; } = new Vector2D(1, 1);
        public Vector2D Side { get; set; } = new Vector2D(1, 1);

        //	
        public List<SteeringBehaviour> SteeringBehaviours { get; set; } = new List<SteeringBehaviour>();

        public override void Update(float delta)
        {
            FindNeighbors(Radius);

            var steeringForce =
                SteeringBehaviours.Aggregate(
                    new Vector2D(),
                    (accumulator, behaviour) =>
                    {
                        // Stop when steeringforce exceeds max speed
                        if (accumulator.Length() >= MaxSpeed)
                            return accumulator;

                        return accumulator + behaviour.Calculate(delta) * (behaviour.Weight / 100);
                    }
                );

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
            if (pos._X > width)
                return new Vector2D(0, pos._Y);

            if (pos._Y > height)
                return new Vector2D(pos._X, 0);

            if (pos._X < 0)
                return new Vector2D(width, pos._Y);

            if (pos._Y < 0)
                return new Vector2D(pos._X, height);

            return pos;
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}