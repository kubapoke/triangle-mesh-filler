using System.Numerics;
using TriangleFilling.Lighting;

namespace TriangleFilling.Coloring
{
    internal class ShapeColorer
    {
        public static void ColorShape(Graphics g, List<Vector2> vertices, Color? color = null, LightSource? lightSource = null)
        {
            Color _color = color ?? Color.LightGray;

            ActiveEdgeTable AET = new ActiveEdgeTable(vertices);

            if(lightSource == null)
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
