using System;
using System.Drawing;
using AIBehaviors.Entity;
using AIBehaviors.Util;

namespace AIBehaviors.Behaviour.Individual
{
    internal class WanderBehaviour : SteeringBehaviour
    {
        private const double WanderRadius = 50;

        private const double WanderDistance = 25;

        private const double WanderJitter = 25;

        private static readonly Random Random = new Random();

        private Vector2D _wanderTarget = new Vector2D(0, 0);

        public WanderBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            var addToPerimeter = new Vector2D(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter);
            _wanderTarget = (_wanderTarget + addToPerimeter).Normalize() * WanderRadius +
                            new Vector2D(WanderDistance, 0);

            return PointToWorldSpace(_wanderTarget) - MovingEntity.Pos;
        }

        private Vector2D PointToWorldSpace(Vector2D localTarget)
        {
            var matrix = new Matrix();

            matrix.Rotate(MovingEntity.Heading, MovingEntity.Side);
            matrix.Translate(MovingEntity.Pos._X, MovingEntity.Pos._Y);

            // Transform the vector to world space
            return matrix.TransformVector2Ds(localTarget);
        }

        private static double RandomClamped()
        {
            return Random.NextDouble() - Random.NextDouble();
        }

        public override void Render(Graphics g)
        {
            var guide = PointToWorldSpace(_wanderTarget);

            g.DrawEllipse(
                new Pen(Color.Red),
                new Rectangle(
                    (int) guide._X,
                    (int) guide._Y,
                    4,
                    4
                )
            );
        }
    }
}