namespace AIBehaviours.Graph
{
    public class Edge<T>
    {
        public Vertex<T> Destination;
        public double Cost;

        public Edge(Vertex<T> destination, double cost)
        {
            Destination = destination;
            Cost = cost;
        }
    }
}