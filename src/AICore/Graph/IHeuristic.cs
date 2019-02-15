
namespace AICore.Graph
{
    public interface IHeuristic<T>
    {
        double Calculate(T startVertex, T targetVertex);
    }
}
