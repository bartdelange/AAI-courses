using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Behaviour.Individual
{
    public class WallAvoidanceBehaviour : ISteeringBehaviour
    {
        private const float FeelerLength = 10;
        private const float HalfPi = (float) (Math.PI / 2);
        
        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerable<IWall> _obstacles;
        
        private IEnumerable<Vector2> _feelers;

        public WallAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IWall> obstacles)
        {
            _movingEntity = entity;
            _obstacles = obstacles;
        }

        public Vector2 Calculate(float deltaTime)
        {
            _feelers = CreateFeelers();

            return Vector2.Zero;
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
            feelers[0] = _movingEntity.Position + (FeelerLength * _movingEntity.Heading * _movingEntity.Velocity.Length());

            // Left pointing feeler
            feelers[1] = _movingEntity.Position + (sideFeelerLength * _movingEntity.Heading.RotateAroundOrigin(HalfPi * 3.5f));

            // Right pointing feeler
            feelers[3] = _movingEntity.Position + (sideFeelerLength * _movingEntity.Heading.RotateAroundOrigin(HalfPi * .5f));

            return feelers;
        }

        public void Draw(Graphics g)
        {
            foreach (var feeler in _feelers)
            {
                g.DrawLine(
                    new Pen(Color.Chartreuse), 
                    _movingEntity.Position.ToPoint(), 
                    feeler.ToPoint()
                );
            }
        }
    }
}