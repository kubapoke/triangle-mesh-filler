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
    }
}
