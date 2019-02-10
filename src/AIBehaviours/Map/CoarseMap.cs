using AIBehaviours.Util;

namespace AIBehaviours.Map
{
    public class CoarseMap : BaseMap
    {
        public CoarseMap()
        {
            var v1 = new Vector2D(50, 50);
            var v2 = new Vector2D(450, 50);
            var v3 = new Vector2D(450, 450);
            var v4 = new Vector2D(50, 450);

            AddEdge(v1, v2, 5);
            AddEdge(v2, v3, 5);
            AddEdge(v3, v4, 5);
            AddEdge(v2, v1, 5);
            AddEdge(v2, v3, 5);
            AddEdge(v2, v4, 5);
            AddEdge(v3, v1, 5);
            AddEdge(v3, v2, 5);
            AddEdge(v3, v4, 5);
            AddEdge(v4, v1, 5);
            AddEdge(v4, v2, 5);
            AddEdge(v4, v3, 5);
        }
    }
}
