using System.Numerics;
using AICore.Behaviour;
using AICore.Util;

namespace AICore.Entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        // Entity properties
        public readonly float Mass = 15;
        public readonly float MaxSpeed = 100;
        public readonly int Radius = 100;

        // Entity behaviour
        public ISteeringBehaviour SteeringBehaviour;

        protected MovingEntity(Vector2 pos, World w) : base(pos, w)
        {
        }

        //	
        public Vector2 Velocity { get; set; } = new Vector2(1, 1);
        public Vector2 Heading { get; set; } = new Vector2(1, 1);
        public Vector2 Side { get; set; } = new Vector2(1, 1);

        public override void Update(float delta)
        {
            FindNeighbors(Radius);

            var acceleration = SteeringBehaviour == null
                ? new Vector2()
                : SteeringBehaviour.Calculate(delta) / Mass;

            /* Weighted sum
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
            */

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

    public static class MovingEntityExtensionMethods
    {
        public static Vector2 GetPointToWorldSpace(this MovingEntity movingEntity, Vector2 localTarget)
        {
            var matrix = new Matrix3()
                .Rotate(movingEntity.Heading, movingEntity.Side)
                .Translate(movingEntity.Pos);

            // Transform the vector to world space
            return localTarget.ApplyMatrix(matrix);
        }
    }
}