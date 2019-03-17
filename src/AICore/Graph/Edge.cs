namespace AICore.Graph
{
    public struct Edge<T>
    {
        public readonly double Cost;
        public readonly Vertex<T> Destination;

        public Edge(Vertex<T> destination, double cost)
        {
            Destination = destination;
            Cost = cost;
        }
    }
}