using System.Collections.Generic;

namespace AIBehaviours.Graph
{
    public class Vertex<T> : IVertex
    {
        public T Data;
        public double Distance;

        public List<Edge<T>> AdjacentVertices = new List<Edge<T>>();
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