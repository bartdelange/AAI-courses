using System.Numerics;

namespace AICore.Entity.Contracts
{
    public interface IWall : IEntity
    {
        Vector2 Normal { get; set; }
        
        bool IntersectsWithLine(Vector2 start, Vector2 target, out double? distance, out Vector2? intersectPoint);

        bool IntersectsWithLine(Vector2 start, Vector2 target);
    }
}