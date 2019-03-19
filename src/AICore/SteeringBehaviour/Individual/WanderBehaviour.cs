using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class WanderBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;
        
        private const float WanderRadius = 50;
        private const float WanderDistance = 25;
        private const float WanderJitter = 25;
        private static readonly Random Random = new Random();

        private readonly IMovingEntity _movingEntity;

        private Vector2 _wanderTarget = new Vector2(0, 0);

        public WanderBehaviour(IMovingEntity movingEntity)
        {
            _movingEntity = movingEntity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var addToPerimeter = new Vector2(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter);

            _wanderTarget = Vector2.Normalize(_wanderTarget + addToPerimeter) * WanderRadius
                            + new Vector2(WanderDistance, 0);

            return _movingEntity.GetPointToWorldSpace(_wanderTarget) - _movingEntity.Position;
        }

        private static float RandomClamped()
        {
            return (float) (Random.NextDouble() - Random.NextDouble());
        }

        public void Render(Graphics graphics)
        {
            var guidePosition = _movingEntity.GetPointToWorldSpace(_wanderTarget);

            graphics.DrawEllipse(
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