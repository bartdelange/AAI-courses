namespace AAITests
{
    public class GraphTests
    {
        public GraphTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        private Graph<string> CreateGraph()
        {
            var graph = new Graph<string>();

            // V0
            graph.AddEdge("V0", "V1", 2);
            graph.AddEdge("V0", "V3", 1);

            // V1
            graph.AddEdge("V1", "V3", 3);
            graph.AddEdge("V1", "V4", 10);

            // V2
            graph.AddEdge("V2", "V0", 4);
            graph.AddEdge("V2", "V5", 5);

            // V3
            graph.AddEdge("V3", "V2", 2);
            graph.AddEdge("V3", "V4", 2);
            graph.AddEdge("V3", "V5", 8);
            graph.AddEdge("V3", "V6", 4);

            // V4
            graph.AddEdge("V4", "V6", 6);

            // V5 - No edges
            // graph.AddEdge("V5", "?", ?);

            // V6
            graph.AddEdge("V6", "V5", 1);

            return graph;
        }

        [Fact]
        public void Should_AddEdge()
        {
            var myGraph = new Graph<string>();
            myGraph.AddEdge("V0", "V1", 5);

            var myVertex = myGraph.GetVertex("V0");

            Assert.Equal(5, myVertex._AdjacentVertices[0]._Cost);
        }

        [Fact]
        public void Should_AStar()
        {
            var myGraph = CreateGraph();

            myGraph.AStar("V0");

            Assert.Equal(0, myGraph.GetVertex("V0")._Distance);
            Assert.Equal(2, myGraph.GetVertex("V1")._Distance);
            Assert.Equal(3, myGraph.GetVertex("V2")._Distance);
            Assert.Equal(1, myGraph.GetVertex("V3")._Distance);
            Assert.Equal(3, myGraph.GetVertex("V4")._Distance);
            Assert.Equal(6, myGraph.GetVertex("V5")._Distance);
            Assert.Equal(5, myGraph.GetVertex("V6")._Distance);
        }

        [Fact]
        public void Should_Dijkstra()
        {
            var myGraph = CreateGraph();

            myGraph.Dijkstra("V0");

            Assert.Equal(0, myGraph.GetVertex("V0")._Distance);
            Assert.Equal(2, myGraph.GetVertex("V1")._Distance);
            Assert.Equal(3, myGraph.GetVertex("V2")._Distance);
            Assert.Equal(1, myGraph.GetVertex("V3")._Distance);
            Assert.Equal(3, myGraph.GetVertex("V4")._Distance);
            Assert.Equal(6, myGraph.GetVertex("V5")._Distance);
            Assert.Equal(5, myGraph.GetVertex("V6")._Distance);
        }

        [Fact]
        public void Should_GetVertex()
        {
            var myGraph = new Graph<string>();
            myGraph.AddEdge("V0", "V1", 5);

            var myVertex = myGraph.GetVertex("V0");

            Assert.Equal("V0", myVertex._Data);
        }

        [Fact]
        public void Should_PathToString()
        {
            var myGraph = CreateGraph();

            myGraph.Unweighted("V0");

            Assert.Equal("(Cost is: 2) V6 to V3 to V0", myGraph.PathToString("V6"));
            Assert.Equal("(Cost is: 1) V1 to V0", myGraph.PathToString("V1"));
        }

        [Fact]
        public void Should_ReturnFalse_When_IsNotConnected()
        {
            var myGraph = new Graph<string>();

            myGraph.AddEdge("V0", "V1", 2);
            myGraph.AddEdge("V2", "V3", 3);

            Assert.False(myGraph.IsConnected());
        }

        [Fact]
        public void Should_ReturnTrue_When_IsConnected()
        {
            var myGraph = CreateGraph();

            Assert.True(myGraph.IsConnected());
        }

        [Fact]
        public void Should_ToString()
        {
            var myGraph = CreateGraph();

            myGraph.Dijkstra("V0");

            _output.WriteLine(myGraph.ToString());

            Assert.Equal(
                "V0 --> V1(2) V3(1)\nV1 --> V3(3) V4(10)\nV3 --> V2(2) V4(2) V5(8) V6(4)\nV4 --> V6(6)\nV2 --> V0(4) V5(5)\nV5 -->\nV6 --> V5(1)\n",
                myGraph.ToString()
            );
        }

        [Fact]
        public void Should_Unweighted()
        {
            var myGraph = CreateGraph();

            myGraph.Unweighted("V0");

            Assert.Equal(0, myGraph.GetVertex("V0")._Distance);
            Assert.Equal(1, myGraph.GetVertex("V1")._Distance);
            Assert.Equal(2, myGraph.GetVertex("V2")._Distance);
            Assert.Equal(1, myGraph.GetVertex("V3")._Distance);
            Assert.Equal(2, myGraph.GetVertex("V4")._Distance);
            Assert.Equal(2, myGraph.GetVertex("V5")._Distance);
            Assert.Equal(2, myGraph.GetVertex("V6")._Distance);
        }
    }
}