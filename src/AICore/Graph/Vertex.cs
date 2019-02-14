using System.Collections.Generic;

namespace AICore.Graph
{
    public class Vertex<T> : IVertex
    {
        public Dictionary<T, Edge<T>> AdjacentVertices = new Dictionary<T, Edge<T>>();
        public T Data;
        public double Distance;
        public Vertex<T> PreviousVertex; // Previous vertex on *shortest* path

        // Variable used in path finding algorithms
        public bool Visited;

        public Vertex(T data)
        {
            Data = data;
        }

        public void Reset()
        {
            Distance = double.MaxValue;
            PreviousVertex = default(Vertex<T>);
            Visited = false;
        }
    }
}