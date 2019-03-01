using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AICore.Behaviour;
using AICore.Util;

namespace AICore.Entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        protected MovingEntity(Vector2 pos, World w) : base(pos, w)
        {
        }

        //	
        public float Mass { get; set; } = 15;
        public float MaxSpeed { get; set; } = 100;
        public int Radius { get; set; } = 100;

        //	
        public Vector2 Velocity { get; set; } = new Vector2(1, 1);
        public Vector2 Heading { get; set; } = new Vector2(1, 1);
        public Vector2 Side { get; set; } = new Vector2(1, 1);

        //	
        public List<SteeringBehaviour> SteeringBehaviours { get; set; } = new List<SteeringBehaviour>();

        public override void Update(float delta)
        {
            FindNeighbors(Radius);

            var steeringForce =
                SteeringBehaviours.Aggregate(
                    new Vector2(),
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
                Heading = Vector2.Normalize(Velocity);
                Side = Heading.Perpendicular();
            }

            Pos = WrapToBounds(Pos, MyWorld.Width, MyWorld.Height);
        }

        private static Vector2 WrapToBounds(Vector2 pos, int width, int height)
        {
            if (pos.X > width)
                return new Vector2(0, pos.Y);

            if (pos.Y > height)
                return new Vector2(pos.X, 0);

            if (pos.X < 0)
                return new Vector2(width, pos.Y);

            if (pos.Y < 0)
                return new Vector2(pos.X, height);

            return pos;
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}