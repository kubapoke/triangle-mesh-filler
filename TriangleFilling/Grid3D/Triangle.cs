namespace TriangleFilling.Grid3D
{
    internal class Triangle
    {
        public Vertex V0;
        public Vertex V1;
        public Vertex V2;
        public Color Color;

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

        public void Fill(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                // Create an array of points to represent the triangle
                PointF[] points = new PointF[]
                {
                    new PointF(V0.PositionRotated.X, V0.PositionRotated.Y),
                    new PointF(V1.PositionRotated.X, V1.PositionRotated.Y),
                    new PointF(V2.PositionRotated.X, V2.PositionRotated.Y)
                };

                // Fill the triangle using the brush
                g.FillPolygon(brush, points);
            }
        }
    }
}
