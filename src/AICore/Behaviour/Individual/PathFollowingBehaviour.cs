using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;

namespace AICore.Behaviour.Individual
{
    public class PathFollowingBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        /// <summary>
        /// Determines how close an agent must be to a waypoint before it seeks the next waypoint
        /// </summary>
        private const int SeekDistance = 5;

        private readonly Vector2 _finalWaypoint;
        private readonly IMovingEntity _movingEntity;

        private readonly IEnumerator<Vector2> _pathEnumerator;

        /// <summary>
        /// Creates a new instance for PathFollowingBehaviour
        /// </summary>
        /// <param name="path"></param>
        /// <param name="movingEntity"></param>
        public PathFollowingBehaviour(IEnumerable<Vector2> path, IMovingEntity movingEntity)
        {
            _movingEntity = movingEntity;

            _finalWaypoint = path.Last();

            _pathEnumerator = path.GetEnumerator();
            _pathEnumerator.MoveNext();
        }

        public Vector2 Calculate(float deltaTime)
        {
            if (_pathEnumerator.Current == _finalWaypoint)
            {
                var arriveBehaviour = new ArriveBehaviour(_movingEntity, _pathEnumerator.Current);
                return arriveBehaviour.Calculate(deltaTime);
            }

            var distanceToWayPoint = (_movingEntity.Position - _pathEnumerator.Current).LengthSquared();

            // Set new waypoint
            if (distanceToWayPoint < SeekDistance) _pathEnumerator.MoveNext();

            var seekBehaviour = new SeekBehaviour(_movingEntity, _pathEnumerator.Current);
            return seekBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}