using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class WanderBehaviour : ISteeringBehaviour
    {
        private const float WanderRadius = 50;
        private const float WanderDistance = 25;
        private const float WanderJitter = 25;
        private static readonly Random Random = new Random();

        private readonly MovingEntity _movingEntity;

        private Vector2 _wanderTarget = new Vector2(0, 0);

        public WanderBehaviour(MovingEntity movingEntity)
        {
            _movingEntity = movingEntity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var addToPerimeter = new Vector2(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter);

            _wanderTarget = Vector2.Normalize(_wanderTarget + addToPerimeter) * WanderRadius
                            + new Vector2(WanderDistance, 0);

            return _movingEntity.GetPointToWorldSpace(_wanderTarget) - _movingEntity.Pos;
        }

        public void Draw(Graphics g)
        {
            var guidePosition = _movingEntity.GetPointToWorldSpace(_wanderTarget);

            g.DrawEllipse(
                new Pen(Color.Red),
                new Rectangle(
                    (int) guidePosition.X,
                    (int) guidePosition.Y,
                    4,
                    4
                )
            );
        }

        private static float RandomClamped()
        {
            return (float) (Random.NextDouble() - Random.NextDouble());
        }
    }
}