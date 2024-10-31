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

            // alpha rotation
            vectorRotated.X = vector.X * cosBeta - vector.Y * sinBeta;
            vectorRotated.Y = vector.X * sinBeta + vector.Y * cosBeta;

            // beta rotation
            vectorRotated.Y = vector.Y * cosAlpha - vector.Z * sinAlpha;
            vectorRotated.Z = vector.Y * cosAlpha + vector.Z * sinAlpha;
        }

        public static implicit operator Point(Vertex vertex)
        {
            return new Point((int)vertex.PositionRotated.X, (int)vertex.PositionRotated.Y);
        }
    }
}
