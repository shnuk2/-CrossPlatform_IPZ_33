using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5Lib
{
    public class GraphVertexInfo
    {
        public GraphVertex Vertex { get; set; }
        public bool IsUnvisited { get; set; }
        public int EdgesWeightSum { get; set; }
        public GraphVertex PreviousVertex { get; set; }

        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }

    public class GraphVertex
    {
        public string Name { get; }
        public List<GraphEdge> Edges { get; }

        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }

        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }

        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }

        public override string ToString() => Name;
    }

    public class GraphEdge
    {
        public GraphVertex ConnectedVertex { get; }
        public int EdgeWeight { get; }

        public GraphEdge(GraphVertex connectedVertex, int weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
    }

    public class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }

        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
        }

        public GraphVertex FindVertex(string vertexName)
        {
            return Vertices.FirstOrDefault(v => v.Name.Equals(vertexName));
        }

        public void AddEdge(string firstName, string secondName, int weight)
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v1 != null && v2 != null)
            {
                v1.AddEdge(v2, weight);
            }
        }
    }

    public class Dijkstra
    {
        Graph graph;
        List<GraphVertexInfo> infos;

        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        void InitInfo()
        {
            infos = new List<GraphVertexInfo>();
            foreach (var v in graph.Vertices)
            {
                infos.Add(new GraphVertexInfo(v));
            }
        }

        GraphVertexInfo GetVertexInfo(GraphVertex v)
        {
            return infos.FirstOrDefault(i => i.Vertex.Equals(v));
        }

        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }
            return minVertexInfo;
        }

        public int FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }
                SetSumToNextVertex(current);
            }
            return GetPathWeight(startVertex, finishVertex);
        }

        void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                if (info.EdgesWeightSum + e.EdgeWeight < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = info.EdgesWeightSum + e.EdgeWeight;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        int GetPathWeight(GraphVertex startVertex, GraphVertex endVertex)
        {
            var endInfo = GetVertexInfo(endVertex);
            if (endInfo.EdgesWeightSum == int.MaxValue)
            {
                return -1;
            }
            return endInfo.EdgesWeightSum;
        }
    }

    public class Lab3Lib
    {
        public static int SetResult(string input)
        {
            string[] data = input.Trim().Split(" ");

            // Зчитуємо перший рядок (N, S, F)
            int index = 0;
            int N = int.Parse(data[index++]);
            int S = int.Parse(data[index++]);
            int F = int.Parse(data[index++]);

            // Ініціалізуємо граф
            var g = new Graph();
            for (int i = 0; i < N; i++)
            {
                g.AddVertex((i + 1).ToString());
            }

            // Зчитуємо матрицю суміжності
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int weight = int.Parse(data[index++]);
                    if (weight != -1 && i != j)
                    {
                        g.AddEdge((i + 1).ToString(), (j + 1).ToString(), weight);
                    }
                }
            }

            // Використовуємо алгоритм Дейкстри для пошуку найкоротшого шляху
            var dijkstra = new Dijkstra(g);
            int shortestPathWeight = dijkstra.FindShortestPath(
                g.FindVertex(S.ToString()),
                g.FindVertex(F.ToString())
            );

            return shortestPathWeight;
        }

        public static string Lab3Res(string inputText)
        {
            int res = SetResult(inputText);
            return res.ToString();
        }
    }
}
