using System.Numerics;

namespace TriangleFilling.Grid3D
{
    internal class Vertex
    {
        public Vector3 Position, PositionRotated;
        public Vector3 Normal, NormalRotated;
        public Vector3 TangentU, TangentURotated;
        public Vector3 TangentV, TangentVRotated;

        public Vertex(Vector3 position, Vector3 normal, Vector3 tangetntU, Vector3 tangentV)
        {
            Position = PositionRotated = position;
            Normal = NormalRotated = normal;
            TangentU = TangentURotated = tangetntU;
            TangentV = TangentVRotated = tangentV;
        }

        public void Rotate(float rotationAlpha, float rotationBeta)
        {
            RotateVector(ref PositionRotated, Position, rotationAlpha, rotationBeta);
            RotateVector(ref NormalRotated, Normal, rotationAlpha, rotationBeta); ;
            RotateVector(ref TangentURotated, TangentU, rotationAlpha, rotationBeta);
            RotateVector(ref TangentVRotated, TangentV, rotationAlpha, rotationBeta);
        }

        private void RotateVector(ref Vector3 vectorRotated, Vector3 vector, float rotationAlpha, float rotationBeta)
        {
            float cosAlpha = (float)Math.Cos(rotationAlpha);
            float sinAlpha = (float)Math.Sin(rotationAlpha);
            float cosBeta = (float)Math.Cos(rotationBeta);
            float sinBeta = (float)Math.Sin(rotationBeta);
            float x1, y1, z1;

            // alpha rotation (Z axis)
            x1 = vector.X * cosAlpha - vector.Y * sinAlpha;
            y1 = vector.X * sinAlpha + vector.Y * cosAlpha;
            z1 = vector.Z;

            // beta rotation (X axis)
            vectorRotated.X = x1;
            vectorRotated.Y = y1 * cosBeta - z1 * sinBeta;
            vectorRotated.Z = y1 * sinBeta + z1 * cosBeta;
        }

        public static implicit operator Point(Vertex vertex)
        {
            return new Point((int)vertex.PositionRotated.X, (int)vertex.PositionRotated.Y);
        }

        public static implicit operator Vector3(Vertex vertex)
        {
            return new Vector3(vertex.PositionRotated.X, vertex.PositionRotated.Y, vertex.PositionRotated.Z);
        }

        public static implicit operator Vector2(Vertex vertex)
        {
            return new Vector2(vertex.PositionRotated.X, vertex.PositionRotated.Y);
        }
    }
}
