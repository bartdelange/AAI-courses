using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Individual
{
    public class WanderBehaviour : SteeringBehaviour
    {
        private const float WanderRadius = 50;

        private const float WanderDistance = 25;

        private const float WanderJitter = 25;

        private static readonly Random Random = new Random();

        private Vector2 _wanderTarget = new Vector2(0, 0);

        public WanderBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2 Calculate(float deltaTime)
        {
            var addToPerimeter = new Vector2(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter);

            _wanderTarget = (Vector2.Normalize(_wanderTarget + addToPerimeter) * WanderRadius) +
                            new Vector2(WanderDistance, 0);

            return PointToWorldSpace(_wanderTarget) - MovingEntity.Pos;
        }

        private Vector2 PointToWorldSpace(Vector2 localTarget)
        {
            var matrix = new Matrix3();

            matrix.Rotate(MovingEntity.Heading, MovingEntity.Side);
            matrix.Translate(MovingEntity.Pos);

            // Transform the vector to world space
            return localTarget.ApplyMatrix(matrix);
        }

        private static float RandomClamped()
        {
            return (float) (Random.NextDouble() - Random.NextDouble());
        }

        public override void Render(Graphics g)
        {
            var guide = PointToWorldSpace(_wanderTarget);

            g.DrawEllipse(
                new Pen(Color.Red),
                new Rectangle(
                    (int) guide.X,
                    (int) guide.Y,
                    4,
                    4
                )
            );
        }
    }
}