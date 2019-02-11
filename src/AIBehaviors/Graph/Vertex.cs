using System.Collections.Generic;

namespace AIBehaviors.Graph
{
    public class Vertex<T> : IVertex
    {
        public List<Edge<T>> _AdjacentVertices = new List<Edge<T>>();
        public T _Data;
        public double _Distance;
        public Vertex<T> _PreviousVertex; // Previous vertex on *shortest* path

        // Variable used in path finding algorithms
        public bool _Visited;

        public Vertex(T data)
        {
            _Data = data;
        }

        public void Reset()
        {
            _Distance = double.MaxValue;
            _PreviousVertex = default;
            _Visited = false;
        }
    }
}