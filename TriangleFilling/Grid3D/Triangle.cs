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
    }
}
