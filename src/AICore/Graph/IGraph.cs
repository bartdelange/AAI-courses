namespace AICore.Graph
{
    public interface IGraph<T>
    {
        Vertex<T> CreateVertexIfNotExists(T vertexData);

        void AddEdge(T sourceVertexData, T destinationVertexData, double cost);

        string ToString();
    }
}