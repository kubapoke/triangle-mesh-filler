using System.Numerics;
using TriangleFilling.FastBitmap;
using TriangleFilling.Lighting;

namespace TriangleFilling.Grid3D
{
    internal class Grid
    {
        private float Size, RotationAlpha = 0, RotationBeta = 0;
        private List<Vertex> Vertices;
        private List<Triangle> Triangles;
        public Texture Texture { get; set; }
        public NormalTexture NormalTexture { get; set; }
        public float Kd { get; set; }
        public float Ks { get; set; }
        public int M { get; set; }

        internal Grid(Vector3[,] V, float width, float heigth, int precision, float kd, float ks, int m, Texture texture, NormalTexture normalTexture)
        {
            Size = Math.Min(width, heigth);
            float step = 1f / (float)(precision);

            AddVertices(V, precision, step);
            AddTrinagles(precision);
            Texture = texture;
            NormalTexture = normalTexture;
            Kd = kd;
            Ks = ks;
            M = m;
        }

        private void AddVertices(Vector3[,] V, int precision, float step)
        {
            Vertices = new List<Vertex>();

            for (int i = 0; i <= precision; i++)
            {
                float u = step * i;
                for (int j = 0; j <= precision; j++)
                {
                    float v = step * j;
                    Vector3 coords = CalculateCoords(u, v, V);
                    Vector3 tangentU = CalculateTangentU(u, v, V);
                    Vector3 tangentV = CalculateTangentV(u, v, V);
                    Vector3 normal = CalculateNormal(tangentU, tangentV);
                    Vector2 gridCoordinates = new Vector2(u, v);

                    Vertices.Add(new Vertex(coords, normal, tangentU, tangentV, gridCoordinates));
                }
            }
        }

        private void AddTrinagles(int precision)
        {
            Triangles = new List<Triangle>();
            precision++;

            for (int i = precision - 2; i >= 0; i--)
            {
                for (int j = precision - 1; j >= 0; j--)
                {
                    if (j != precision - 1)
                    {
                        Triangles.Add(new Triangle(Vertices[i + j * precision], Vertices[i + j * precision + 1], Vertices[i + j * precision + precision]));
                    }
                    if (j != 0)
                    {
                        Triangles.Add(new Triangle(Vertices[i + j * precision], Vertices[i + j * precision + 1], Vertices[i + j * precision - precision + 1]));
                    }
                }
            }
        }

        private Vector3 CalculateCoords(float u, float v, Vector3[,] V)
        {
            Vector3 res = new Vector3(0, 0, 0);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    float Bin = Bernstein(i, 3, u);
                    float Bjm = Bernstein(j, 3, v);

                    res += V[i, j] * Bin * Bjm;
                }
            }

            return res;
        }

        private Vector3 CalculateNormal(Vector3 tangentU, Vector3 tangentV)
        {
            Vector3 res = Vector3.Cross(tangentU, tangentV);
            return Vector3.Normalize(res);
        }

        private Vector3 CalculateTangentU(float u, float v, Vector3[,] V)
        {
            Vector3 res = new Vector3(0, 0, 0);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    float Bin = Bernstein(i, 2, u);
                    float Bjm = Bernstein(j, 3, v);

                    res += (V[i + 1, j] - V[i, j]) * Bin * Bjm;
                }
            }
            return 3 * res;
        }

        private Vector3 CalculateTangentV(float u, float v, Vector3[,] V)
        {
            Vector3 res = new Vector3(0, 0, 0);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float Bin = Bernstein(i, 3, u);
                    float Bjm = Bernstein(j, 2, v);

                    res += (V[i, j + 1] - V[i, j]) * Bin * Bjm;
                }
            }
            return 3 * res;
        }

        private float Bernstein(int i, int n, float t)
        {
            return BinomialCoefficient(n, i) * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }

        private int BinomialCoefficient(int n, int k)
        {
            int result = 1;
            for (int d = 1; d <= k; d++)
            {
                result *= n--;
                result /= d;
            }
            return result;
        }

        public void Rotate(float? rotationAlpha, float? rotationBeta)
        {
            if (rotationAlpha != null)
                RotationAlpha = (float)rotationAlpha;
            if (rotationBeta != null)
                RotationBeta = (float)rotationBeta;

            float alphaRad = ((float)Math.PI / 180f) * RotationAlpha;
            float betaRad = ((float)Math.PI / 180f) * RotationBeta;

            foreach (var vertex in Vertices)
            {
                vertex.Rotate(alphaRad, betaRad);
            }
        }

        public void Draw(Graphics g, DirectBitmap b, LightSource light, bool shouldDrawOutline = true,
            bool shouldDrawFill = true, bool shouldDrawLight = false, bool shouldUseNormalTexture = false)
        {
            if (shouldDrawFill)
            {
                Color[,] colors = new Color[b.Width, b.Height];
                Parallel.ForEach(Triangles, triangle =>
                {
                    triangle.Fill(g, colors, Texture, Kd, Ks, M, light, shouldUseNormalTexture ? NormalTexture : null);
                });

                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        b.SetPixel(x, y, colors[x, y]);
                    }
                }
            }
            if (shouldDrawOutline)
            {
                Parallel.ForEach(Triangles, triangle =>
                {
                    triangle.Draw(g);
                });
            }

            if (shouldDrawLight)
            {
                light.Draw(g);
            }
        }
    }
}
