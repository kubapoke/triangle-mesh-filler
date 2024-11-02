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
            Rectangle rectangle = new Rectangle((int)Position.X - r, (int)Position.Y - r, 2 * r, 2 * r);

            using (Brush brush = new SolidBrush(Color))
                g.FillEllipse(brush, rectangle);
            using (Pen pen = new Pen(Color.FromArgb(Color.ToArgb() ^ 0xffffff)))
                g.DrawEllipse(pen, rectangle);
        }
    }
}
