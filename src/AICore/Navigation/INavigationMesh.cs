using System.Collections.Generic;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Graph;

namespace AICore.Navigation
{
    public interface INavigationMesh
    {
        Graph<Vector2> Mesh { get; set; }

        IEnumerable<IObstacle> Obstacles { get; set; }

        IRenderable MeshHelper { get; set; }
    }
}