using System.Numerics;

namespace TriangleFilling.Coloring
{
    internal class ActiveEdgeTable
    {
        private List<Vector2> Vertices;
        private List<int> Ind;
        private List<AETNode> Nodes = new List<AETNode>();
        private int Y, MaxY, Idx;

        private class AETNode : IComparable<AETNode>
        {
            public int Ymax;
            public float X;
            public float Move;

            public AETNode(int ymax, float x, float move)
            {
                Ymax = ymax;
                X = x;
                Move = move;
            }

            public void MoveUp()
            {
                this.X += this.Move;
            }

            public int CompareTo(AETNode other)
            {
                int compare = X.CompareTo(other.X);
                return compare;
            }
        }

        public ActiveEdgeTable(List<Vector2> vertices)
        {
            Vertices = vertices;

            Ind = new List<int>();

            for (int i = 0; i < Vertices.Count; i++)
            {
                Ind.Add(i);
            }

            Ind.Sort((i1, i2) => Vertices[i1].Y.CompareTo(Vertices[i2].Y));

            Y = (int)Vertices[Ind[0]].Y;
            MaxY = (int)Vertices[Ind[Vertices.Count - 1]].Y;
            Idx = 0;
        }

        public IEnumerable<((int x, int y) p1, (int x, int y) p2)> GetLines()
        {
            Y = (int)Vertices[Ind[0]].Y;
            Idx = 0;

            for (; Y <= MaxY; Y++)
            {
                Nodes.RemoveAll(node => (int)node.Ymax <= Y);

                while (Idx < Vertices.Count && Y == (int)Vertices[Ind[Idx]].Y)
                {
                    Vector2 Next = Vertices[(Ind[Idx] + 1) % Vertices.Count];
                    Vector2 Prev = Vertices[(Ind[Idx] - 1 + Vertices.Count) % Vertices.Count];

                    float X = Vertices[Ind[Idx]].X;
                    float NextY = Next.Y;
                    float PrevY = Prev.Y;
                    float NextX = Next.X;
                    float PrevX = Prev.X;

                    if ((int)NextY > Y)
                    {
                        AETNode node = new AETNode((int)NextY, X, (NextX - X) / (NextY - Y));
                        Nodes.Add(node);
                    }
                    if ((int)PrevY > Y)
                    {
                        AETNode node = new AETNode((int)PrevY, X, (PrevX - X) / (PrevY - Y));
                        Nodes.Add(node);
                    }

                    Idx++;
                }

                Nodes.Sort((node1, node2) => node1.X.CompareTo(node2.X));

                for (int i = 0; i < Nodes.Count - 1; i += 2)
                {
                    yield return (((int)Nodes[i].X, Y), ((int)Nodes[i + 1].X, Y));
                }
                foreach (var node in Nodes)
                {
                    node.MoveUp();
                }
            }

            yield break;
        }

        public IEnumerable<(int x, int y)> GetPoints()
        {
            Y = (int)Vertices[Ind[0]].Y;
            Idx = 0;

            for (; Y <= MaxY; Y++)
            {
                Nodes.RemoveAll(node => (int)node.Ymax <= Y);

                while (Idx < Vertices.Count && Y == (int)Vertices[Ind[Idx]].Y)
                {
                    Vector2 Next = Vertices[(Ind[Idx] + 1) % Vertices.Count];
                    Vector2 Prev = Vertices[(Ind[Idx] - 1 + Vertices.Count) % Vertices.Count];

                    float X = Vertices[Ind[Idx]].X;
                    float NextY = Next.Y;
                    float PrevY = Prev.Y;
                    float NextX = Next.X;
                    float PrevX = Prev.X;

                    if ((int)NextY > Y)
                    {
                        AETNode node = new AETNode((int)NextY, X, (NextX - X) / (NextY - Y));
                        Nodes.Add(node);
                    }
                    if ((int)PrevY > Y)
                    {
                        AETNode node = new AETNode((int)PrevY, X, (PrevX - X) / (PrevY - Y));
                        Nodes.Add(node);
                    }

                    Idx++;
                }

                Nodes.Sort((node1, node2) => node1.X.CompareTo(node2.X));

                for (int i = 0; i < Nodes.Count - 1; i += 2)
                {
                    for (int x = (int)Nodes[i].X; x <= (int)Nodes[i + 1].X; x++)
                        yield return (x, Y);
                }
                foreach (var node in Nodes)
                {
                    node.MoveUp();
                }     
            }

            yield break;
        }
    }
}
