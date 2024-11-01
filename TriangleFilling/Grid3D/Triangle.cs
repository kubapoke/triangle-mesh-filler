using System.Numerics;
using TriangleFilling.Coloring;

namespace TriangleFilling.Grid3D
{
    internal class Triangle
    {
        public Vertex V0;
        public Vertex V1;
        public Vertex V2;
        public Color Color;

        public Triangle(Vertex v0, Vertex v1, Vertex v2, Color? color = null)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
            Color = color ?? Color.LightGray;
        }

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);

            g.DrawLine(p, V0, V1);
            g.DrawLine(p, V1, V2);
            g.DrawLine(p, V2, V0);
        }

        public void Fill(Graphics g)
        {
            List<Vector2> vertices = new List<Vector2>() { V0, V1, V2 };
            ShapeColorer.ColorShape(g, vertices, Color);
        }
    }
}
