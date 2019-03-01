using System;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Individual
{
    public class ArriveBehaviour : SteeringBehaviour
    {
        private const double DecelerationSpeed = 1;
        private const double DecelerationTweaker = 0.3;

        public ArriveBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2 Calculate(float deltaTime)
        {
            var toTarget = Target.Pos - MovingEntity.Pos;

            //calculate the distance to the target
            var dist = toTarget.Length();

            if (!(dist > 0)) return new Vector2();

            //because Deceleration is enumerated as an int, this value is required
            //to provide fine tweaking of the deceleration..

            //calculate the speed required to reach the target given the desired
            //deceleration
            var speed = (float) (dist / (DecelerationSpeed * DecelerationTweaker));

            //make sure the velocity does not exceed the max
            speed = Math.Min(speed, MovingEntity.MaxSpeed);

            //from here proceed just like Seek except we don't need to normalize 
            //the ToTarget vector because we have already gone to the trouble
            //of calculating its length: dist. 
            var desiredVelocity = toTarget * speed / dist;

            return desiredVelocity - MovingEntity.Velocity;
        }
    }
}