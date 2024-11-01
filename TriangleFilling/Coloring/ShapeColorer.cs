using System.Numerics;

namespace TriangleFilling.Coloring
{
    internal class ShapeColorer
    {
        public static void ColorShape(Graphics g, List<Vector2> vertices, Color? color = null)
        {
            Color _color = color ?? Color.LightGray;

            var pen = new Pen(_color);
            var brush = new SolidBrush(_color);

            ActiveEdgeTable AET = new ActiveEdgeTable(vertices);

            foreach (var line in AET.GetLines())
            {
                g.DrawLine(pen, new Point(line.p1.x, line.p1.y), new Point(line.p2.x, line.p2.y));
            }
            //foreach (var point in AET.GetPoints())
            //{
            //    g.FillRectangle(brush, point.x, point.y, 1, 1);
            //}
        }
    }
}
