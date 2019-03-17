using System;
using System.Collections.Generic;

namespace AICore.Graph
{
    public class Vertex<T> : IVertex
    {
        public readonly Dictionary<T, Edge<T>> AdjacentVertices = new Dictionary<T, Edge<T>>();
        public readonly T Value;
        public double Distance = double.MaxValue;
        public Vertex<T> PreviousVertex; // Previous vertex on *shortest* path

        // Variable used in path finding algorithms
        public bool Visited;

        public Vertex(T data)
        {
            Value = data;
        }

        public void Reset()
        {
            Distance = double.MaxValue;
            PreviousVertex = default(Vertex<T>);
            Visited = false;
        }
    }
}