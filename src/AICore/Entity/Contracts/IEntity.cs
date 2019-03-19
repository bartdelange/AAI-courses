using System.Drawing;
using System.Numerics;

namespace AICore.Entity.Contracts
{
    public interface IEntity : IRenderable
    {
        Vector2 Position { get; set; }
        
        int BoundingRadius { get; set; }
    }
}