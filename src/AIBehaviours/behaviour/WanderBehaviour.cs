using AIBehaviours.entity;
using AIBehaviours.util;
using System;
using System.Drawing;

namespace AIBehaviours.behaviour
{
    internal class WanderBehaviour : SteeringBehaviour
    {
        private const double WanderRadius = 40;

        private const double WanderDistance = WanderRadius;

        private const double WanderJitter = 10;

        private readonly Vector2D _wanderTarget = new Vector2D(0, 0);

        private static readonly Random Random = new Random();

        public WanderBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            // Add a small random vector to the target's position
            _wanderTarget.Add(
                    new Vector2D(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter)
                )
                // Re-project this new vector back on to a unit circle
                .Normalize()
                // increase the length of the vector to the same as the radius of the wander circle
                .Multiply(WanderRadius)
                // FIXME side to side steering
                // Move the target into a position WanderDist in front of the agent
                .Add(new Vector2D(WanderDistance, 0));

            return PointToWorldSpace(_wanderTarget)
                .Clone()
                .Subtract(MovingEntity.Pos);
        }

        private Vector2D PointToWorldSpace(Vector2D localTarget)
        {
            var matrix = new MatrixTransformations();

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
            var circle = new Point(
                (int) (MovingEntity.Pos.X),
                (int) (MovingEntity.Pos.Y)
            );

            var guide = PointToWorldSpace(_wanderTarget).Clone();

            g.DrawString(
                "" + guide,
                new Font("arial", 10),
                new SolidBrush(Color.Red),
                new PointF(10, 10)
            );

            g.DrawEllipse(
                new Pen(Color.Black),
                new Rectangle(circle,
                    new Size((int) (WanderRadius + WanderDistance), (int) (WanderRadius + WanderDistance)))
            );

            g.DrawEllipse(
                new Pen(Color.Red),
                new Rectangle(
                    (int) (guide.X),
                    (int) (guide.Y),
                    2,
                    2
                )
            );
        }
    }
}