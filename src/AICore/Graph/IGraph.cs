namespace AIBehaviours.Graph
{
    public interface IGraph<T>
    {
        Vertex<T> GetVertex(T vertexData);

        void AddEdge(T sourceVertexData, T destinationVertexData, double cost);

        string ToString();
    }
}