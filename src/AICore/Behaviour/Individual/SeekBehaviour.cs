using System.Drawing;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class SeekBehaviour : SteeringBehaviour
    {
        private Vector2 _targetPosition;
        
        public SeekBehaviour(MovingEntity movingEntity, Vector2 targetPosition, float weight)
            : base(movingEntity, null, weight)
        {
            _targetPosition = targetPosition;
        }

        /// <summary>
        ///     Set a velocity that will make the agent move the world target
        /// </summary>
        public override Vector2 Calculate(float deltaTime)
        {
            return Vector2.Normalize(_targetPosition - MovingEntity.Pos) * MovingEntity.MaxSpeed;
        }

        public override void Render(Graphics g)
        {
        }
    }
}