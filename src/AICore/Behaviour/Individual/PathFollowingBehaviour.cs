using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class PathFollowingBehaviour : SteeringBehaviour
    {
        private readonly IEnumerator<Vector2> _pathEnumerator;
        private readonly Vector2 _finalWaypoint;

        /// <summary>
        /// Determines how close an agent must be to a waypoint before it seeks the next waypoint
        /// </summary>
        private const int SeekDistance = 5;

        /// <summary>
        /// Creates a new instance for PathFollowingBehaviour
        /// </summary>
        /// <param name="path"></param>
        /// <param name="movingEntity"></param>
        /// <param name="weight"></param>
        public PathFollowingBehaviour(IEnumerable<Vector2> path, MovingEntity movingEntity, float weight) : 
            base(movingEntity, weight)
        {
            if (!path.Any())
            {
                return;
            }
            
            _finalWaypoint = path.Last();
            _pathEnumerator = path.GetEnumerator();

            _pathEnumerator.MoveNext();
        }

        public override Vector2 Calculate(float deltaTime)
        {
            if (_pathEnumerator.Current == _finalWaypoint)
            {
                var arriveBehaviour = new ArriveBehaviour(MovingEntity, _pathEnumerator.Current, Weight);                
                return arriveBehaviour.Calculate(deltaTime);
            }
            
            var distanceToWayPoint = (MovingEntity.Pos - _pathEnumerator.Current).LengthSquared();
        
            // Set new waypoint
            if (distanceToWayPoint < SeekDistance)
            {
                _pathEnumerator.MoveNext();
            }

            var seekBehaviour = new SeekBehaviour(MovingEntity, _pathEnumerator.Current, Weight);
            return seekBehaviour.Calculate(deltaTime);
        }

        public override void Render(Graphics g)
        {
        }
    }
}