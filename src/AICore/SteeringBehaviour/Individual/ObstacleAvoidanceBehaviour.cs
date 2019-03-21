using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.SteeringBehaviour.Individual
{
    public class ObstacleAvoidanceBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerable<IObstacle> _obstacles;

        // Render properties
        private readonly float _detectionBoxLength;

        // Debug property
        private Vector2 _targetPosition;

        public ObstacleAvoidanceBehaviour(
            IMovingEntity movingEntity,
            IEnumerable<IObstacle> obstacles,
            float detectionBoxLength
        )
        {
            _movingEntity = movingEntity;
            _obstacles = obstacles;
            _detectionBoxLength = detectionBoxLength;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var currentSpeed = _movingEntity.Velocity.Length();
            var boxLength = _detectionBoxLength + (currentSpeed / _movingEntity.MaxSpeed) * _detectionBoxLength;

            IObstacle closestObstacle = null;

            var closestObstacleLocalPosition = Vector2.Zero;
            var closestPoint = double.MaxValue;

            // Find closest intersecting obstacle
            foreach (var obstacle in _obstacles)
            {
                var distanceToObstacle = obstacle.Position - _movingEntity.Position;

                // The bounding radius of the other is taken into account by adding it to the range
                var squaredRange = Math.Pow(boxLength + obstacle.BoundingRadius, 2);

                // Return true if entity is within range. (working in distance-squared space to avoid sqrts)
                if (distanceToObstacle.LengthSquared() >= squaredRange)
                {
                    continue;
                }

                var localPosition = _movingEntity.GetPointToLocalSpace(obstacle.Position);

                // If the local position has a negative x value then it must lay behind the agent.
                // (in which case it can be ignored)
                if (localPosition.X < 0)
                {
                    continue;
                }

                var expandedRadius = obstacle.BoundingRadius + _movingEntity.BoundingRadius;

                // If the distance from the x axis to the object's position is higher than its radius + half the
                // width of the detection box then there isn't a potential intersection.
                if (Math.Abs(localPosition.Y) >= expandedRadius)
                {
                    continue;
                }

                // Now to do a line/circle intersection test. The center of the circle is represented by (cX, cY).
                // The intersection points are given by the formula x = cX +/- sqrt(r ^ 2 - c Y ^ 2) for y=0. We only need
                // to look at the smallest positive value of x because that will be the closest point of intersection.
                double cX = localPosition.X;
                double cY = localPosition.Y;

                // We only need to calculate the sqrt part of the above equation once
                var sqrtPart = Math.Sqrt(expandedRadius * expandedRadius - cY * cY);

                var intersectionPoint = cX - sqrtPart;

                if (intersectionPoint <= 0.0)
                {
                    intersectionPoint = cX + sqrtPart;
                }

                // Test to see if this is the closest so far. If it is keep a record of the obstacle and its local coordinates
                if (intersectionPoint >= closestPoint)
                {
                    continue;
                }

                closestPoint = intersectionPoint;
                closestObstacle = obstacle;
                closestObstacleLocalPosition = localPosition;
            }

            if (closestObstacle == null)
            {
                _targetPosition = Vector2.Zero;
                return _targetPosition;
            }


            // The closer the agent is to an object, the stronger the steering force should be
            var multiplier = 1.0f + (_detectionBoxLength - closestObstacleLocalPosition.X) /
                             _detectionBoxLength;

            const float brakingWeight = 0.2f;

            var steeringForce = new Vector2(
                (closestObstacle.BoundingRadius - closestObstacleLocalPosition.X) * brakingWeight,
                (closestObstacle.BoundingRadius - closestObstacleLocalPosition.Y) * multiplier
            );

            _targetPosition = _movingEntity.VectorToWorldSpace(steeringForce);
            return _targetPosition;
        }

        public void Render(Graphics graphics)
        {
            if (_targetPosition == Vector2.Zero)
            {
                return;
            }

            graphics.DrawLine(
                Pens.Black,
                _movingEntity.Position.ToPoint(),
                (_movingEntity.Position + _targetPosition).ToPoint()
            );
        }
    }
}