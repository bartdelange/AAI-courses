using System;
using System.Collections.Generic;
using System.Linq;
using AICore.Graph.Heuristics;
using AICore.Util;

namespace AICore.Graph
{
    public abstract class Graph<T> : IGraph<T>
    {
        protected readonly Dictionary<T, Vertex<T>> VertexMap = new Dictionary<T, Vertex<T>>();

        /// <summary>
        ///     Try to get the vertex by vertex name, if it does not exist create a
        ///     new one and add it to the vertex map
        /// </summary>
        /// <param name="vertexData"></param>
        public Vertex<T> GetVertex(T vertexData)
        {
            if (VertexMap.TryGetValue(vertexData, out var vertex)) return vertex;

            vertex = new Vertex<T>(vertexData);
            VertexMap.Add(vertexData, vertex);

            return vertex;
        }

        /// <summary>
        ///     Adds a new edge to the current graph
        /// </summary>
        /// <param name="sourceVertexData"></param>
        /// <param name="destinationVertexData"></param>
        /// <param name="cost"></param>
        public void AddEdge(T sourceVertexData, T destinationVertexData, double cost)
        {
            var sourceVertex = GetVertex(sourceVertexData);
            var destinationVertex = GetVertex(destinationVertexData);

            sourceVertex.AdjacentVertices[destinationVertexData] = new Edge<T>(destinationVertex, cost);
        }

        /// <summary>
        ///     Prints vertex with its adjacent vertices
        ///     V0 --> V1(2) V3(1)
        ///     V1 --> V3(3) V4(10)
        ///     V2 --> V0(4) V5(5)
        ///     V3 --> V2(2) V5(8) V6(4) V4(2)
        ///     V4 --> V6(6)
        ///     V5 -->
        ///     V6 --> V5(1)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var output = "";

            foreach (var vertex in VertexMap.Values)
            {
                var adjacentVertexes = vertex.AdjacentVertices.Aggregate(
                    "",
                    (accumulator, edge) => accumulator + $" {edge.Value.Destination.Data}({edge.Value.Cost})"
                );

                output += $"{vertex.Data} -->{adjacentVertexes}\n";
            }

            return output;
        }

        /// <summary>
        ///     Initializes the vertex output info prior to running any shortest path algorithm
        /// </summary>
        protected void ClearAll()
        {
            foreach (var vertex in VertexMap.Values)
                vertex.Reset();
        }

        /// <summary>
        ///     Method used to print vertices in a path
        /// </summary>
        /// <param name="destination"></param>
        protected string PathToString(Vertex<T> destination)
        {
            var result = "";

            if (destination.PreviousVertex != null)
                result += " to " + PathToString(destination.PreviousVertex);

            return destination.Data + result;
        }

        /// <summary>
        ///     Method used to print a path
        /// </summary>
        /// <param name="destination"></param>
        /// <exception cref="NoSuchElementException"></exception>
        public string PathToString(T destination)
        {
            if (!VertexMap.TryGetValue(destination, out var vertex))
                throw new NoSuchElementException();

            if (vertex.Distance == double.MaxValue)
                return destination + " is unreachable";

            return $"(Cost is: {vertex.Distance}) {PathToString(vertex)}";
        }

        /// <summary>
        ///     Checks whether all vertices in this graph are connected
        /// </summary>
        public bool IsConnected()
        {
            Unweighted(VertexMap.First().Value.Data);

            foreach (var vertex in VertexMap.Values)
                // Return false when any vertex has not been visited
                if (!vertex.Visited)
                    return false;

            return true;
        }

        #region Path finding algorithms

        /// <summary>
        ///     Single source unweighted shortest-path algorithm
        /// </summary>
        /// <param name="startVertex"></param>
        /// <exception cref="NoSuchElementException"></exception>
        public void Unweighted(T startVertex)
        {
            ClearAll();

            if (!VertexMap.TryGetValue(startVertex, out var start))
                throw new NoSuchElementException();

            var queue = new Queue<Vertex<T>>();

            // Add start vertex to queue
            queue.Enqueue(start);
            start.Distance = 0;

            while (queue.Count != 0)
            {
                var currentVertex = queue.Dequeue();

                // Set visited to true so we can check if the graph is connected or not
                currentVertex.Visited = true;


                foreach (var edge in currentVertex.AdjacentVertices)
                {
                    var adjacentVertex = edge.Value.Destination;

                    if (Math.Abs(adjacentVertex.Distance - double.MaxValue) > 0.000000001) continue;

                    adjacentVertex.Distance = currentVertex.Distance + 1;
                    adjacentVertex.PreviousVertex = currentVertex;

                    queue.Enqueue(adjacentVertex);
                }
            }
        }

        /// <summary>
        ///     Single-source weighted shortest-path algorithm
        /// </summary>
        /// <param name="startValue"></param>
        /// <exception cref="NoSuchElementException"></exception>
        /// <exception cref="GraphException"></exception>
        public void Dijkstra(T startValue)
        {
            ClearAll();

            if (!VertexMap.TryGetValue(startValue, out var startVertex))
                throw new NoSuchElementException();

            var priorityQueue = new PriorityQueue<Path<T>>();

            // Add start path to priority queue
            priorityQueue.Enqueue(new Path<T>(startVertex, 0));
            startVertex.Distance = 0;

            var nodesSeen = 0;
            while (!priorityQueue.IsEmpty() && nodesSeen < VertexMap.Count)
            {
                var vertexRecord = priorityQueue.Dequeue();
                var vertex = vertexRecord.Destination;

                // Don't revisit vertex
                if (vertex.Visited) continue;
                vertex.Visited = true;

                nodesSeen++;

                foreach (var edge in vertex.AdjacentVertices)
                {
                    var adjacentVertex = edge.Value.Destination;
                    var edgeCost = edge.Value.Cost;

                    if (edgeCost < 0)
                        throw new GraphException("Graph has negative edges");

                    // Don't update the distance of to the adjacent vertex when the distance is higher
                    if (!(vertex.Distance + edgeCost < adjacentVertex.Distance))
                        return;

                    adjacentVertex.Distance = vertex.Distance + edgeCost;
                    adjacentVertex.PreviousVertex = vertex;

                    priorityQueue.Enqueue(new Path<T>(adjacentVertex, adjacentVertex.Distance));
                }
            }
        }

        public Tuple<IEnumerable<T>, Dictionary<T, Vertex<T>>> AStar(
            T startValue,
            T targetValue,
            IHeuristic<T> heuristic
        )
        {
            ClearAll();

            var visitedVertexMap = new Dictionary<T, Vertex<T>>();

            if (!VertexMap.TryGetValue(startValue, out var startVertex))
                throw new NoSuchElementException();

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
                foreach (var edge in vertex.AdjacentVertices)
                {
                    var adjacentVertex = edge.Value.Destination;
                    var edgeCost = edge.Value.Cost;
                    var heuristics = heuristic.Calculate(adjacentVertex.Data, targetValue);

                    // Don't revisit vertex
                    if (adjacentVertex.Visited)
                        continue;

                    if (edgeCost < 0)
                        throw new GraphException("Graph has negative edges");

                    // Don't update the distance of to the adjacent vertex when the distance is higher
                    if (!(vertex.Distance + edgeCost < adjacentVertex.Distance))
                        continue;

                    adjacentVertex.Distance = vertex.Distance + edgeCost;
                    adjacentVertex.PreviousVertex = vertex;

                    priorityQueue.Enqueue(new Path<T>(adjacentVertex, adjacentVertex.Distance + heuristics));
                    visitedVertexMap[adjacentVertex.Data] = adjacentVertex;

                    if (adjacentVertex.Data.Equals(targetValue)) found = true;
                }
            }

            var path = new List<T>();

            if (visitedVertexMap.TryGetValue(targetValue, out var targetVertex))
                while (targetVertex.PreviousVertex != null)
                {
                    path.Add(targetVertex.Data);
                    targetVertex = targetVertex.PreviousVertex;
                }

            path = Enumerable.Reverse(path).ToList();

            return new Tuple<IEnumerable<T>, Dictionary<T, Vertex<T>>>(path, visitedVertexMap);
        }

        #endregion
    }

    #region Custom error classes

    public class NoSuchElementException : Exception
    {
        public NoSuchElementException()
        {
        }

        public NoSuchElementException(string message) : base(message)
        {
        }

        public NoSuchElementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class GraphException : Exception
    {
        public GraphException()
        {
        }

        public GraphException(string message) : base(message)
        {
        }

        public GraphException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    #endregion
}