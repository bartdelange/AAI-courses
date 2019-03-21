using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Entity
{
    public static class MovingEntityExtensionMethods
    {
        public static Vector2 GetPointToWorldSpace(this IMovingEntity movingEntity, Vector2 localSpaceTarget)
        {
            var matrix = new Matrix3()
                .Rotate(movingEntity.Heading, movingEntity.Heading.Perpendicular())
                .Translate(movingEntity.Position);

            // Transform the vector to world space
            return localSpaceTarget.ApplyMatrix(matrix);
        }

        public static Vector2 GetPointToLocalSpace(this IMovingEntity movingEntity, Vector2 worldSpaceTarget)
        {
            var side = movingEntity.Heading.Perpendicular();

            // Create a transformation matrix
            var tx = -Vector2.Dot(movingEntity.Heading, movingEntity.Position);
            var ty = -Vector2.Dot(side, movingEntity.Position);

            var transformationMatrix = new Matrix3(
                movingEntity.Heading.X, side.X, tx,
                movingEntity.Heading.Y, side.Y, ty,
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
                .Rotate(movingEntity.Heading, movingEntity.Heading.Perpendicular());

            return localVector.ApplyMatrix(transformationMatrix);
        }
    }
}