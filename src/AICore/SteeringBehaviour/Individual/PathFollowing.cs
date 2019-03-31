using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Navigation;

namespace AICore.SteeringBehaviour.Individual
{
    public class PathFollowing : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Determines how close an agent must be to a waypoint before it seeks the next waypoint
        /// </summary>
        private const int SeekDistance = 5;

        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerator<Vector2> _pathEnumerator;
        private readonly NavigationHelper _navigationHelper;

        public readonly Vector2 FinalWaypoint;

        /// <summary>
        /// Creates a new instance for PathFollowing
        /// </summary>
        /// <param name="path"></param>
        /// <param name="movingEntity"></param>
        public PathFollowing(PathValues<Vector2> pathValues, IMovingEntity movingEntity)
        {
            _movingEntity = movingEntity;
            
            var path = pathValues.SmoothPath ?? pathValues.Path;

            FinalWaypoint = path.Last();
            _pathEnumerator = path.GetEnumerator();

            _navigationHelper = new NavigationHelper
            {
                PathValues = pathValues
            };

            _pathEnumerator.MoveNext();
        }

        public Vector2 Calculate(float deltaTime)
        {
            if (_pathEnumerator.Current == FinalWaypoint)
            {
                var arriveBehaviour = new Arrive(_movingEntity, _pathEnumerator.Current);
                return arriveBehaviour.Calculate(deltaTime);
            }

            var distanceToWayPoint = (_movingEntity.Position - _pathEnumerator.Current).LengthSquared();

            // Set new waypoint
            if (distanceToWayPoint < SeekDistance) _pathEnumerator.MoveNext();

            var seekBehaviour = new Seek(_movingEntity, _pathEnumerator.Current);
            return seekBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
            _navigationHelper.RenderIfVisible(graphics);
        }
    }
}