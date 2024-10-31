using System.Numerics;

namespace TriangleFilling.Grid3D
{
    internal class Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 TangentU;
        public Vector3 TangentV;

        public Vertex(Vector3 position, Vector3 normal, Vector3 tangetntU, Vector3 tangentV)
        {
            Position = position;
            Normal = normal;
            TangentU = tangetntU;
            TangentV = tangentV;
        }

        public static implicit operator Point(Vertex vertex)
        {
            return new Point((int)vertex.Position.X, (int)vertex.Position.Y);
        }
    }
}
