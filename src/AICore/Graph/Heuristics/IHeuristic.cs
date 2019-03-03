
namespace AICore.Graph.Heuristic
{
    public interface IHeuristic<T>
    {
        double Calculate(T startVertex, T targetVertex);
    }
}
