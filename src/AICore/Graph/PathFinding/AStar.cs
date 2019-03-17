using System;
using System.Collections.Generic;
using System.Linq;
using AICore.Graph.Heuristics;
using AICore.Navigation;
using AICore.Util;

namespace AICore.Graph.PathFinding
{
    public class AStar<T> : IPathFinding<T>
    {
        public PathValues<T> FindPath(Graph<T> graph, T start, T target, IHeuristic<T> heuristic)
        {
            graph.ClearAll();

            var visitedVertices = new Dictionary<T, Vertex<T>>();

            if (!graph.Vertices.TryGetValue(start, out var startVertex))
                throw new NoSuchElementException();

            // TODO Just return the value when path has been found
            var found = false;

            var priorityQueue = new PriorityQueue<Path<T>>();

            // Add start path to priority queue
            priorityQueue.Enqueue(new Path<T>(startVertex, 0));
            startVertex.Distance = 0;

            while (!found && !priorityQueue.IsEmpty())
            {
                var vertexRecord = priorityQueue.Dequeue();
                var vertex = vertexRecord.Destination;

                vertex.Visited = true;

                #region traverse edges

                foreach (var edge in vertex.AdjacentVertices)
                {
                    var adjacentVertex = edge.Value.Destination;
                    var edgeCost = edge.Value.Cost;
                    var heuristics = heuristic.Calculate(adjacentVertex.Value, target);

                    // Don't revisit vertex
                    if (adjacentVertex.Visited)
                    {
                        continue;
                    }

                    if (edgeCost < 0)
                    {
                        throw new GraphException("Graph has negative edges");
                    }

                    // Don't update the distance of to the adjacent vertex when the distance is higher
                    if (!(vertex.Distance + edgeCost < adjacentVertex.Distance))
                    {
                        continue;
                    }

                    adjacentVertex.Distance = vertex.Distance + edgeCost;
                    adjacentVertex.PreviousVertex = vertex;

                    priorityQueue.Enqueue(
                        new Path<T>(adjacentVertex, adjacentVertex.Distance + heuristics)
                    );

                    visitedVertices[adjacentVertex.Value] = adjacentVertex;

                    if (adjacentVertex.Value.Equals(target))
                    {
                        found = true;
                    }
                }

                #endregion
            }

            // Check if
            if (!visitedVertices.TryGetValue(target, out var targetVertex))
            {
                throw new CannotFindPathException();
            }

            var path = new List<T>();
            while (targetVertex.PreviousVertex != null)
            {
                path.Add(targetVertex.Value);
                targetVertex = targetVertex.PreviousVertex;
            }

            return new PathValues<T>(
                Enumerable.Reverse(path),
                visitedVertices.Values
            );
        }
    }

    public class CannotFindPathException : Exception
    {
    }
}