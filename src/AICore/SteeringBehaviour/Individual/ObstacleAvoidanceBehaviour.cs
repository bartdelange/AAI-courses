using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
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
        private readonly Pen _detectionBoxPen = Pens.Black;
        private Vector2 _behaviourTarget;

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
            var boxLength = _detectionBoxLength + (currentSpeed / currentSpeed) * _detectionBoxLength;

            IObstacle closestIntersectingObstacle = null;

            var closestIntersectingObstacleLocalPosition = Vector2.Zero;
            var closestIntersectingPoint = double.MaxValue;

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
                if (intersectionPoint >= closestIntersectingPoint)
                {
                    continue;
                }

                closestIntersectingPoint = intersectionPoint;
                closestIntersectingObstacle = obstacle;
                closestIntersectingObstacleLocalPosition = localPosition;
            }

            if (closestIntersectingObstacle == null)
            {
                return Vector2.Zero;
            }

            const float brakingWeight = 0.2f;

            // The closer the agent is to an object, the stronger the steering force should be
            var multiplier = 1.0f + (_detectionBoxLength - closestIntersectingObstacleLocalPosition.X) /
                             _detectionBoxLength;

            var steeringForce = new Vector2(
                (closestIntersectingObstacle.BoundingRadius - closestIntersectingObstacleLocalPosition.X) *
                brakingWeight,
                (closestIntersectingObstacle.BoundingRadius - closestIntersectingObstacleLocalPosition.Y) *
                multiplier
            );

            _behaviourTarget = _movingEntity.GetPointToWorldSpace(steeringForce);

            return _behaviourTarget;
        }

        public void Render(Graphics graphics)
        {
            var detectionBoxPosition = _movingEntity.GetPointToWorldSpace(new Vector2(_detectionBoxLength, 0));

            graphics.DrawLine(
                _detectionBoxPen,
                _movingEntity.Position.ToPoint(),
                detectionBoxPosition.ToPoint()
            );
        }
    }
}