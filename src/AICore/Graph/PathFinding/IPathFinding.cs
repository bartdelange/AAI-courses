using AICore.Graph.Heuristics;
using AICore.Navigation;

namespace AICore.Graph.PathFinding
{
    public interface IPathFinding<T>
    {
        PathValues<T> FindPath(Graph<T> graph, T start, T target, IHeuristic<T> heuristic);
    }
}