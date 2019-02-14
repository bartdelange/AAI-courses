namespace AICore.Graph
{
    public class Edge<T>
    {
        public double Cost;
        public Vertex<T> Destination;

        public Edge(Vertex<T> destination, double cost)
        {
            Destination = destination;
            Cost = cost;
        }
    }
}