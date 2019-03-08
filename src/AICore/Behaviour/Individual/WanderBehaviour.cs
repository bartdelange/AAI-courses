using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class WanderBehaviour : ISteeringBehaviour
    {
        private static readonly Random Random = new Random();

        private const float WanderRadius = 50;
        private const float WanderDistance = 25;
        private const float WanderJitter = 25;

        private Vector2 _wanderTarget = new Vector2(0, 0);

        private readonly MovingEntity _movingEntity;
        private readonly MovingEntity _target;

        public WanderBehaviour(MovingEntity movingEntity, MovingEntity target)
        {
            _movingEntity = movingEntity;
            _target = target;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var addToPerimeter = new Vector2(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter);

            _wanderTarget = (Vector2.Normalize(_wanderTarget + addToPerimeter) * WanderRadius)
                            + new Vector2(WanderDistance, 0);

            return _movingEntity.GetPointToWorldSpace(_wanderTarget) - _movingEntity.Pos;
        }

        private static float RandomClamped()
        {
            return (float) (Random.NextDouble() - Random.NextDouble());
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
    }
}