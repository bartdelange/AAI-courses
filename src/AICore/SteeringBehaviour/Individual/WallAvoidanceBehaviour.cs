using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.SteeringBehaviour.Individual
{
    public class WallAvoidanceBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly float _feelerLength = 40;

        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerable<IWall> _walls;

        private IEnumerable<Vector2> _feelers;

        public WallAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IWall> walls, float feelerLength = 40)
        {
            _movingEntity = entity;
            _walls = walls;
            _feelerLength = feelerLength;

            _feelers = CreateFeelers();
        }

        public Vector2 Calculate(float deltaTime)
        {
            _feelers = CreateFeelers();

            IWall closestWall = null;
            double? closestDistance = null;
            Vector2? closestPoint = null;
            Vector2? intersectingFeeler = null;

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
                    intersectingFeeler = feeler;
                    closestPoint = intersectPoint;
                    closestWall = wall;
                }
            }

            if (closestWall == null || closestPoint == null || intersectingFeeler == null)
            {
                return Vector2.Zero;
            }

            var overShoot = (Vector2) (intersectingFeeler - closestPoint);

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
            var speed = _movingEntity.Velocity.Length();
            
            var sideFeelerLength = _feelerLength / 2;
            var feelers = new Vector2[3];

            // Forward pointing feeler
            feelers[0] = _movingEntity.Position +
                         _feelerLength * _movingEntity.Heading * speed;

            // Left pointing feeler
            feelers[1] = _movingEntity.Position +
                         sideFeelerLength * _movingEntity.Heading.RotateAroundOrigin(35) * speed;

            // Right pointing feeler
            feelers[2] = _movingEntity.Position +
                         sideFeelerLength * _movingEntity.Heading.RotateAroundOrigin(-35) * speed;

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