using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.SteeringBehaviour.Individual
{
    public class WallAvoidanceBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private const float FeelerLength = 50;
        private const float HalfPi = (float) (Math.PI / 2);

        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerable<IWall> _walls;

        private IEnumerable<Vector2> _feelers;

        public WallAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IWall> walls)
        {
            _movingEntity = entity;
            _walls = walls;
            
            _feelers = CreateFeelers();
        }

        public Vector2 Calculate(float deltaTime)
        {
            _feelers = CreateFeelers();

            IWall closestWall = null;
            double? closestDistance = null;
            Vector2? closestPoint = null;

            foreach (var feeler in _feelers)
            {
                foreach (var wall in _walls)
                {
                    if (!wall.IntersectsWithLine(
                        _movingEntity.Position,
                        feeler,
                        out var distance,
                        out var intersectPoint
                    ))
                    {
                        continue;
                    }

                    // Ignore it intersection if distance is longer than previous feeler
                    if (distance >= closestDistance) continue;

                    closestDistance = distance;
                    closestPoint = intersectPoint;
                    closestWall = wall;
                }
            }

            if (closestWall == null || closestPoint == null)
            {
                return Vector2.Zero;
            }

            var overShoot = (Vector2) (_movingEntity.Position - closestPoint);

            //create a force in the direction of the wall normal, with a 
            //magnitude of the overshoot
            return closestWall.Normal * overShoot.Length();
        }

        /// <summary>
        /// Creates the antenna utilized by WallAvoidance
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Vector2> CreateFeelers()
        {
            const float sideFeelerLength = FeelerLength / 2.0f;

            var feelers = new Vector2[3];

            // Forward pointing feeler
            feelers[0] = _movingEntity.Position +
                         (FeelerLength * _movingEntity.Heading * _movingEntity.Velocity.Length());

            // Left pointing feeler
            feelers[1] = _movingEntity.Position +
                         (sideFeelerLength * _movingEntity.Heading.RotateAroundOrigin(HalfPi * 3.5f));

            // Right pointing feeler
            feelers[2] = _movingEntity.Position +
                         (sideFeelerLength * _movingEntity.Heading.RotateAroundOrigin(HalfPi * .5f));

            return feelers;
        }

        public void Render(Graphics graphics)
        {
            foreach (var feeler in _feelers)
            {
                graphics.DrawLine(
                    new Pen(Color.Red),
                    _movingEntity.Position.ToPoint(),
                    feeler.ToPoint()
                );
            }
        }
    }
}