using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Graph;

namespace AICore.Navigation
{
    public class FineMesh : INavigationMesh
    {
        public Graph<Vector2> Mesh { get; set; } = new Graph<Vector2>();

        public IEnumerable<IObstacle> Obstacles { get; set; }

        public IRenderable MeshHelper { get; set; }

        private readonly int _density;
        private readonly Vector2 _bounds;

        public FineMesh(int density, Vector2 bounds, IEnumerable<IObstacle> obstacles)
        {
            Obstacles = obstacles;

            _density = density;
            _bounds = bounds;

            var margin = density / 2;

            var topLeft = new Vector2(margin);

            GenerateEdges(topLeft);
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
            if (!(currentPosition.X + _density >= _bounds.X))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X + _density, currentPosition.Y)
                );
            }

            if (!(currentPosition.X - _density <= 0))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X - _density, currentPosition.Y)
                );
            }

            // Vertical edges
            if (!(currentPosition.Y + _density >= _bounds.Y))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X, currentPosition.Y + _density)
                );
            }

            if (!(currentPosition.Y - _density <= 0))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X, currentPosition.Y - _density)
                );
            }

            /*
            // Diagonal edges
            if (!(currentPosition.X + _density >= _bounds.X) && !(currentPosition.Y + _density >= _bounds.Y))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X + _density, currentPosition.Y + _density)
                );
            }

            if (!(currentPosition.X - _density <= 0) && !(currentPosition.Y + _density >= _bounds.Y))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X - _density, currentPosition.Y + _density)
                );
            }

            if (!(currentPosition.X - _density <= 0) && !(currentPosition.Y - _density <= 0))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X - _density, currentPosition.Y - _density)
                );
            }

            if (!(currentPosition.X + _density >= _bounds.X) && !(currentPosition.Y - _density <= 0))
            {
                TryAddEdge(
                    currentPosition,
                    new Vector2(currentPosition.X + _density, currentPosition.Y - _density)
                );
            }
            */
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