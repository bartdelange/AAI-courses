using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Graph;
using AICore.Model;

namespace AICore.Navigation
{
    public class FineMesh : INavigationMesh
    {
        public Graph<Vector2> Mesh { get; set; } = new Graph<Vector2>();

        public IEnumerable<IObstacle> Obstacles { get; set; }

        public IRenderable MeshHelper { get; set; }

        private readonly float _density;
        private readonly Bounds _bounds;

        public FineMesh(float density, Bounds bounds, IEnumerable<IObstacle> obstacles)
        {
            Obstacles = obstacles;

            _density = density;
            _bounds = bounds;

            GenerateEdges(_bounds.Center());
        }

        #region Floodfilling

        /// <summary>
        /// TODO Add documentation
        /// </summary>
        /// <param name="currentPosition"></param>
        private void GenerateEdges(Vector2 currentPosition)
        {
            if (Mesh.Vertices.ContainsKey(currentPosition))
            {
                return;
            }

            // Add given vector to list
            Mesh.CreateVertexIfNotExists(currentPosition);

            // Horizontal edges
            if (!(currentPosition.X + _density >= _bounds.Max.X))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X + _density, currentPosition.Y)
                );
            }

            if (!(currentPosition.X - _density <= _bounds.Min.X))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X - _density, currentPosition.Y)
                );
            }

            // Vertical edges
            if (!(currentPosition.Y + _density >= _bounds.Max.Y))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X, currentPosition.Y + _density)
                );
            }

            if (!(currentPosition.Y - _density <= _bounds.Min.Y))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X, currentPosition.Y - _density)
                );
            }
        }

        /// <summary>
        /// TODO Add documentation
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void TryAddEdge(Vector2 start, Vector2 end)
        {
            if (Obstacles.Any(obstacle => obstacle.IntersectsWithLine(start, end))) return;
            
            GenerateEdges(end);
                
            Mesh.AddEdge(start, end, Vector2.Distance(start, end));
            Mesh.AddEdge(end, start, Vector2.Distance(start, end));
        }

        #endregion
    }
}