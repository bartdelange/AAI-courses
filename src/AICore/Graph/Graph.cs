using System;
using System.Collections.Generic;
using System.Linq;
using AICore.Util;

namespace AICore.Graph
{
    public abstract class Graph<T> : IGraph<T>
    {
        protected Dictionary<T, Vertex<T>> _VertexMap = new Dictionary<T, Vertex<T>>();

        /// <summary>
        ///     Try to get the vertex by vertex name, if it does not exist create a
        ///     new one and add it to the vertex map
        /// </summary>
        /// <param name="vertexName"></param>
        public Vertex<T> GetVertex(T vertexData)
        {
            if (_VertexMap.TryGetValue(vertexData, out var vertex)) return vertex;

            vertex = new Vertex<T>(vertexData);
            _VertexMap.Add(vertexData, vertex);

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

            sourceVertex._AdjacentVertices.Add(new Edge<T>(destinationVertex, cost));
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

            foreach (var vertex in _VertexMap.Values)
            {
                var adjacentVertexes = vertex._AdjacentVertices.Aggregate(
                    "",
                    (accumulator, edge) => accumulator += $" {edge._Destination._Data}({edge._Cost})"
                );

                output += $"{vertex._Data} -->{adjacentVertexes}\n";
            }

            return output;
        }

        /// <summary>
        ///     Initializes the vertex output info prior to running any shortest path algorithm
        /// </summary>
        protected void ClearAll()
        {
            foreach (var vertex in _VertexMap.Values)
                vertex.Reset();
        }

        /// <summary>
        ///     Method used to print vertices in a path
        /// </summary>
        /// <param name="destination"></param>
        protected string PathToString(Vertex<T> destination)
        {
            var result = "";

            if (destination._PreviousVertex != null)
                result += " to " + PathToString(destination._PreviousVertex);

            return destination._Data + result;
        }

        /// <summary>
        ///     Method used to print a path
        /// </summary>
        /// <param name="destination"></param>
        /// <exception cref="NoSuchElementException"></exception>
        public string PathToString(T destination)
        {
            if (!_VertexMap.TryGetValue(destination, out var vertex))
                throw new NoSuchElementException();

            if (vertex._Distance == double.MaxValue)
                return destination + " is unreachable";

            return $"(Cost is: {vertex._Distance}) {PathToString(vertex)}";
        }

        /// <summary>
        ///     Checks whether all vertices in this graph are connected
        /// </summary>
        public bool IsConnected()
        {
            Unweighted(_VertexMap.First().Value._Data);

            foreach (var vertex in _VertexMap.Values)
                // Return false when any vertex has not been visited
                if (!vertex._Visited)
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

            if (!_VertexMap.TryGetValue(startVertex, out var start))
                throw new NoSuchElementException();

            var queue = new Queue<Vertex<T>>();

            // Add start vertex to queue
            queue.Enqueue(start);
            start._Distance = 0;

            while (queue.Count != 0)
            {
                var currentVertex = queue.Dequeue();

                // Set visited to true so we can check if the graph is connected or not
                currentVertex._Visited = true;

                currentVertex._AdjacentVertices.ForEach(edge =>
                {
                    var adjacentVertex = edge._Destination;

                    if (adjacentVertex._Distance != double.MaxValue) return;

                    adjacentVertex._Distance = currentVertex._Distance + 1;
                    adjacentVertex._PreviousVertex = currentVertex;

                    queue.Enqueue(adjacentVertex);
                });
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

            if (!_VertexMap.TryGetValue(startValue, out var startVertex))
                throw new NoSuchElementException();

            var priorityQueue = new PriorityQueue<Path<T>>();

            // Add start path to priority queue
            priorityQueue.Enqueue(new Path<T>(startVertex, 0));
            startVertex._Distance = 0;

            var nodesSeen = 0;
            while (!priorityQueue.IsEmpty() && nodesSeen < _VertexMap.Count)
            {
                var vertexRecord = priorityQueue.Dequeue();
                var vertex = vertexRecord._Destination;

                // Don't revisit vertex
                if (vertex._Visited) continue;

                vertex._Visited = true;
                nodesSeen++;

                vertex._AdjacentVertices.ForEach(edge =>
                {
                    var adjacentVertex = edge._Destination;
                    var edgeCost = edge._Cost;

                    if (edgeCost < 0)
                        throw new GraphException("Graph has negative edges");

                    // Don't update the distance of to the adjacent vertex when the distance is higher
                    if (!(vertex._Distance + edgeCost < adjacentVertex._Distance))
                        return;

                    adjacentVertex._Distance = vertex._Distance + edgeCost;
                    adjacentVertex._PreviousVertex = vertex;

                    priorityQueue.Enqueue(new Path<T>(adjacentVertex, adjacentVertex._Distance));
                });
            }
        }

        public void AStar(T startValue)
        {
            ClearAll();

            if (!_VertexMap.TryGetValue(startValue, out var startVertex))
                throw new NoSuchElementException();

            throw new NotImplementedException();
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