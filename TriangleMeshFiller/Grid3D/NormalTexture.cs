using System.Numerics;

namespace TriangleFilling.Grid3D
{
    internal class NormalTexture : Texture
    {
        public NormalTexture(string imagePath) : base(imagePath) { }

        public Vector3 GetPixelNormal(float u, float v)
        {
            Vector3 color = Vector3.Normalize(GetPixelVector(u, v));
            Vector3 normal = color * 2f - new Vector3(1, 1, 1);
            return Vector3.Normalize(normal);
        }
    }
}
