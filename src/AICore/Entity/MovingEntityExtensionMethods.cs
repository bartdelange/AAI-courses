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
            var tx = -Vector2.Dot(movingEntity.Position, movingEntity.Heading);
            var ty = -Vector2.Dot(movingEntity.Position, movingEntity.Side);

            var transformationMatrix = new Matrix3(
                movingEntity.Heading.X, movingEntity.Side.X, tx,
                movingEntity.Heading.Y, movingEntity.Side.Y, ty,
                0, 0, 0
            );

            return worldSpaceTarget.ApplyMatrix(transformationMatrix);
        }
    }
}