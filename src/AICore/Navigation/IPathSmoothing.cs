using System.Collections.Generic;
using System.Numerics;

namespace AICore.Navigation
{
    public interface IPathSmoothing
    {
        IEnumerable<Vector2> CreateSmoothPath(INavigationMesh navigationMesh, List<Vector2> path);
    }
}