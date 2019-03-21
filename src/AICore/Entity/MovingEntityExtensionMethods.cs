using System.Numerics;
using AICore.Util;

namespace AICore.Entity.Contracts
{
    public static class MovingEntityExtensionMethods
    {
        public static Vector2 GetPointToWorldSpace(this IMovingEntity movingEntity, Vector2 localSpaceTarget)
        {
            var matrix = new Matrix3()
                .Rotate(movingEntity.Heading, movingEntity.Side)
                .Translate(movingEntity.Position);

            // Transform the vector to world space
            return localSpaceTarget.ApplyMatrix(matrix);
        }

        public static Vector2 GetPointToLocalSpace(this IMovingEntity movingEntity, Vector2 worldSpaceTarget)
        {
            // Create a transformation matrix
            var tx = -Vector2.Dot(movingEntity.Heading, movingEntity.Position);
            var ty = -Vector2.Dot(movingEntity.Side, movingEntity.Position);

            var transformationMatrix = new Matrix3(
                movingEntity.Heading.X, movingEntity.Side.X, tx,
                movingEntity.Heading.Y, movingEntity.Side.Y, ty,
                0, 0, 0
            );

            return worldSpaceTarget.ApplyMatrix(transformationMatrix);
        }

        /// <summary>
        /// Transforms a vector from the agent's local space into world space 
        /// </summary>
        /// <param name="movingEntity"></param>
        /// <param name="localVector"></param>
        /// <returns></returns>
        public static Vector2 VectorToWorldSpace(this IMovingEntity movingEntity, Vector2 localVector)
        {
            var transformationMatrix = new Matrix3()
                .Rotate(movingEntity.Heading, movingEntity.Side);

            return localVector.ApplyMatrix(transformationMatrix);
        }
    }
}