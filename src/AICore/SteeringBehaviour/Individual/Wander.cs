using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class Wander : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private static readonly Random Random = new Random();

        private readonly IMovingEntity _movingEntity;

        private readonly float _wanderRadius;
        private readonly float _wanderDistance;
        private readonly float _wanderJitter;

        private Vector2 _wanderTarget = new Vector2(0, 0);

        public Wander(
            IMovingEntity movingEntity,
            float wanderDistance = 50,
            float wanderRadius = 50,
            float wanderJitter = 15
        )
        {
            _movingEntity = movingEntity;

            _wanderDistance = wanderDistance;
            _wanderRadius = wanderRadius;
            _wanderJitter = wanderJitter;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var addToPerimeter = new Vector2(RandomClamped() * _wanderJitter, RandomClamped() * _wanderJitter);

            _wanderTarget = Vector2.Normalize(_wanderTarget + addToPerimeter) * _wanderRadius
                            + new Vector2(_wanderDistance, 0);

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