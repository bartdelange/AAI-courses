using AIBehaviours.entity;
using AIBehaviours.util;
using System;
using System.Drawing;

namespace AIBehaviours.behaviour
{
    internal class WanderBehaviour : SteeringBehaviour
    {
        private const double WanderRadius = 50;

        private const double WanderDistance = WanderRadius + 25;

        private const double WanderJitter = 100;

        private Vector2D _wanderTarget = new Vector2D(0, 0);

        private static readonly Random Random = new Random();

        public WanderBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            _wanderTarget.Add(new Vector2D(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter))
                .Normalize()
                .Multiply(WanderRadius)
                .Add(new Vector2D(WanderDistance, 0));

            return PointToWorldSpace(_wanderTarget)
                .Clone()
                .Subtract(MovingEntity.Pos);
        }

        private Vector2D PointToWorldSpace(Vector2D localTarget)
        {
            MatrixTransformations matrix = new MatrixTransformations();

            matrix.Rotate(MovingEntity.Heading, MovingEntity.Side);
            matrix.Translate(MovingEntity.Pos.X, MovingEntity.Pos.Y);

            // Transform the vector to world space
            return matrix.TransformVector2Ds(localTarget);
        }

        private static double RandomClamped()
        {
            return Random.NextDouble() - Random.NextDouble();
        }

        public override void Render(Graphics g)
        {
            Vector2D guide = PointToWorldSpace(_wanderTarget.Clone());

            g.DrawEllipse(
                new Pen(Color.Red),
                new Rectangle(
                    (int)(guide.X),
                    (int)(guide.Y),
                    4,
                    4
                )
            );
        }
    }
}
