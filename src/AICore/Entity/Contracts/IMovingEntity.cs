using System.Numerics;
using AICore.Behaviour;
using AICore.Util;

namespace AICore.Entity.Contracts
{
    public interface IMovingEntity : IEntity
    {
        ISteeringBehaviour SteeringBehaviour { get; set; }
        
        Vector2 Velocity { get; set; }
        Vector2 Heading { get; set; }
        Vector2 Side { get; set; }

        float MaxSpeed { get; set; }

        void Update(float timeDelta);
    }
    
    public static class MovingEntityExtensionMethods
    {
        public static Vector2 GetPointToWorldSpace(this IMovingEntity movingEntity, Vector2 localTarget)
        {
            var matrix = new Matrix3()
                .Rotate(movingEntity.Heading, movingEntity.Side)
                .Translate(movingEntity.Position);

            // Transform the vector to world space
            return localTarget.ApplyMatrix(matrix);
        }
    }
}