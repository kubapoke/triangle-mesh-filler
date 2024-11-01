using System.Numerics;
using TriangleFilling.Lighting;

namespace TriangleFilling.Coloring
{
    internal class ShapeColorer
    {
        public static void ColorShape(Graphics g, List<Vector3> vertices, Color? color = null, LightSource? light = null,
            List<Vector3>? normals = null, List<Vector3>? tangentsU = null, List<Vector3>? tangentsV = null)
        {
            Color _color = color ?? Color.LightGray;

            Vector2 V0 = new Vector2(vertices[0].X, vertices[0].Y);
            Vector2 V1 = new Vector2(vertices[1].X, vertices[1].Y);
            Vector2 V2 = new Vector2(vertices[2].X, vertices[2].Y);

            ActiveEdgeTable AET = new ActiveEdgeTable(new List<Vector2>() { V0, V1, V2 });

            if(light == null)
            {
                var pen = new Pen(_color);
                foreach (var line in AET.GetLines())
                {
                    g.DrawLine(pen, new Point(line.p1.x, line.p1.y), new Point(line.p2.x, line.p2.y));
                }
            }
            else
            {
                var brush = new SolidBrush(_color);
                foreach (var point in AET.GetPoints())
                {
                    g.FillRectangle(brush, point.x, point.y, 1, 1);
                }
            }
        }
    }
}
