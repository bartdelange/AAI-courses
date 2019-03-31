using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class Arrive : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private const double DecelerationSpeed = 1;
        private const double DecelerationTweaker = 0.3;

        private readonly IMovingEntity _movingEntity;
        private readonly Vector2 _targetPosition;

        public Arrive(IMovingEntity movingEntity, Vector2 targetPosition)
        {
            _movingEntity = movingEntity;
            _targetPosition = targetPosition;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var toTarget = _targetPosition - _movingEntity.Position;

            //calculate the distance to the target
            var dist = toTarget.Length();

            if (dist <= 0)
            {
                return Vector2.Zero;
            }

            //because Deceleration is enumerated as an int, this value is required
            //to provide fine tweaking of the deceleration..

            //calculate the speed required to reach the target given the desired
            //deceleration
            var speed = Math.Ceiling(dist / (DecelerationSpeed * DecelerationTweaker));

            //make sure the velocity does not exceed the max
            speed = Math.Min(speed, _movingEntity.MaxSpeed);

            //from here proceed just like Seek except we don't need to normalize 
            //the ToTarget vector because we have already gone to the trouble
            //of calculating its length: dist. 
            var desiredVelocity = toTarget * (float) speed / dist;

            return desiredVelocity - _movingEntity.Velocity;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}