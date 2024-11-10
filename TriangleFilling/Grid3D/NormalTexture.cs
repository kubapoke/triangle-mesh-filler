using System.Numerics;

namespace TriangleFilling.Grid3D
{
    internal class NormalTexture : Texture
    {
        public NormalTexture(string imagePath) : base(imagePath) { }

        public Vector3 GetPixelNormal(float u, float v)
        {
            Vector3 color = GetPixelVector(u, v);
            Vector3 normal = new Vector3(color.X / 127.5f - 1, color.Y / 127.5f - 1, color.Z / 255f);

            return Vector3.Normalize(normal);
        }
    }
}
