using System.Numerics;

namespace TriangleFilling.Lighting
{
    internal class LightSource
    {
        public Vector3 Position;
        public Color Color;

        public LightSource(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void Draw(Graphics g)
        {
            int r = 15;
            Brush brush = new SolidBrush(Color);
            Pen pen = new Pen(Color.FromArgb(Color.ToArgb() ^ 0xffffff));

            Rectangle rectangle = new Rectangle((int)Position.X - r, (int)Position.Y - r, 2 * r, 2 * r);
            g.FillEllipse(brush, rectangle);
            g.DrawEllipse(pen, rectangle);
        }
    }
}
