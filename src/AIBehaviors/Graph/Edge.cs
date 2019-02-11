namespace AIBehaviors.Graph
{
    public class Edge<T>
    {
        public double _Cost;
        public Vertex<T> _Destination;

        public Edge(Vertex<T> destination, double cost)
        {
            _Destination = destination;
            _Cost = cost;
        }
    }
}