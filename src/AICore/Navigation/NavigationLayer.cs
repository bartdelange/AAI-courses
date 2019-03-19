using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Exceptions;
using AICore.Graph;
using AICore.Graph.Heuristics;
using AICore.Graph.PathFinding;
using AICore.Util;

namespace AICore.Navigation
{
    public class PathValues<T>
    {
        public IEnumerable<Vertex<T>> VisitedVertices { get; set; }

        public IEnumerable<T> Path { get; set; }

        public IEnumerable<T> SmoothPath { get; set; }

        public PathValues(IEnumerable<T> path, IEnumerable<Vertex<T>> visitedVertices)
        {
            Path = path;
            VisitedVertices = visitedVertices;
            SmoothPath = null;
        }
    }

    public class NavigationLayer : IRenderable
    {
        // Render properties
        public bool Visible { get; set; } = true;

        private readonly INavigationMesh _navigationMesh;
        private readonly NavigationHelper _navigationHelper;

        // Drawing properties
        private readonly Pen _edgePen = new Pen(Color.FromArgb(50, Color.Black));
        private readonly Brush _vertexBrush = new SolidBrush(Color.FromArgb(50, Color.Black));

        public NavigationLayer(INavigationMesh navigationMesh)
        {
            _navigationMesh = navigationMesh;

            _navigationHelper = new NavigationHelper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="targetPosition"></param>
        /// <param name="pathFinding"></param>
        /// <param name="pathSmoothing"></param>
        /// <returns></returns>
        public IEnumerable<Vector2> FindPath(
            Vector2 startPosition,
            Vector2 targetPosition,
            IPathFinding<Vector2> pathFinding,
            IPathSmoothing pathSmoothing = null
        )
        {
            var startVertexPosition = _navigationMesh.FindVertex(startPosition);
            var targetVertexPosition = _navigationMesh.FindVertex(targetPosition);

            // Just return a path with one vertex When start and destination are the same vertex
            if (startVertexPosition == targetVertexPosition)
            {
                return new[] {targetVertexPosition};
            }

            // Try to find a path with given path finding algorithm
            var fullPath = new List<Vector2>();

            try
            {
                var pathValues = pathFinding.FindPath(
                    _navigationMesh.Mesh,
                    startVertexPosition,
                    targetVertexPosition,
                    new Manhattan()
                );

                fullPath = pathValues.Path.ToList();

                // Add start and destination to path
                fullPath.Insert(0, startPosition);
                fullPath.Add(targetPosition);
                
                // Try to create a smooth path
                pathValues.SmoothPath = pathSmoothing?.CreateSmoothPath(_navigationMesh, fullPath);

                // Save values to NavigationHelper                
                _navigationHelper.PathValues = pathValues;

                // Apply path smoothing if argument is not null
                return pathValues.SmoothPath ?? pathValues.Path;
            }
            catch (NoSuchElementException noSuchElementException)
            {
                Console.WriteLine(noSuchElementException);
            }

            return fullPath;
        }

        public void Render(Graphics graphics)
        {
            // Draw all vertices in navigation mesh
            foreach (var edge in _navigationMesh.Mesh.Vertices)
            {
                foreach (var adjacentEdge in edge.Value.AdjacentVertices)
                {
                    graphics.DrawLine(
                        _edgePen,
                        adjacentEdge.Value.Destination.Value.ToPoint(), edge.Value.Value.ToPoint()
                    );
                }

                graphics.FillEllipse(
                    _vertexBrush,
                    new Rectangle(edge.Value.Value.Minus(2).ToPoint(), new Size(4, 4))
                );
            }

            _navigationHelper.RenderIfVisible(graphics);
        }
    }
}