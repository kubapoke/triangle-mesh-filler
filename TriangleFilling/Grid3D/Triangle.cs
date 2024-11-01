using System.Numerics;
using TriangleFilling.Coloring;
using TriangleFilling.Lighting;

namespace TriangleFilling.Grid3D
{
    internal class Triangle
    {
        public Vertex V0;
        public Vertex V1;
        public Vertex V2;

        public Triangle(Vertex v0, Vertex v1, Vertex v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
        }

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);

            g.DrawLine(p, V0, V1);
            g.DrawLine(p, V1, V2);
            g.DrawLine(p, V2, V0);
        }

        public void Fill(Graphics g, Color color, float kd, float ks, float m, LightSource light)
        {
            List<Vector3> vertices = new List<Vector3>() { V0, V1, V2 };
            List<Vector3> normals = new List<Vector3>() { V0.NormalRotated, V1.NormalRotated, V2.NormalRotated };
            List<Vector3> tangentsU = new List<Vector3>() { V0.TangentURotated, V1.TangentURotated, V2.TangentURotated };
            List<Vector3> tangentsV = new List<Vector3>() { V0.TangentVRotated, V1.TangentVRotated, V2.TangentVRotated };
            ShapeColorer.ColorShape(g, vertices, kd, ks, m, color, light, normals, tangentsU, tangentsV);
        }
    }
}
