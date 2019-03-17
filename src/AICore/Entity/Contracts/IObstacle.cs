using System.Numerics;

namespace AICore.Entity.Contracts
{
    public interface IObstacle : IEntity
    {
        bool IntersectsWithLine(Vector2 start, Vector2 target, int margin = 0);
        
        bool IntersectsWithPoint(Vector2 point, int margin = 0);
    }
}