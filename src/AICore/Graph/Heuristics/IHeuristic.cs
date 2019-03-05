
namespace AICore.Graph.Heuristics
{
    public interface IHeuristic<T>
    {
        double Calculate(T startVertex, T targetVertex);
    }
}
