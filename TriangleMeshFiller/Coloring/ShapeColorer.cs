﻿using System.Numerics;
using TriangleFilling.Grid3D;
using TriangleFilling.Lighting;

namespace TriangleFilling.Coloring
{
    internal class ShapeColorer
    {
        public static void ColorShape(Graphics g, List<Vector2> vertices, Color color)
        {
            ActiveEdgeTable AET = new ActiveEdgeTable(new List<Vector2>() { vertices[0], vertices[1], vertices[2] });

            using (var pen = new Pen(color))
            {
                foreach (var line in AET.GetLines())
                {
                    g.DrawLine(pen, new Point(line.p1.x, line.p1.y), new Point(line.p2.x, line.p2.y));
                }
            }
        }

        public static void ColorShapeWithLighting(Graphics g, Color[,] colors, List<Vector3> vertices, float kd, float ks, int m,
            Texture texture, LightSource light, List<Vector3> normals, List<Vector3> tangentUs, List<Vector3> tangentVs,
            List<Vector2> gridCoordinates, NormalTexture? normalTexture = null)
        {
            Vector2 V0 = new Vector2(vertices[0].X, vertices[0].Y);
            Vector2 V1 = new Vector2(vertices[1].X, vertices[1].Y);
            Vector2 V2 = new Vector2(vertices[2].X, vertices[2].Y);

            ActiveEdgeTable AET = new ActiveEdgeTable(new List<Vector2>() { V0, V1, V2 });

            Parallel.ForEach(AET.GetPoints(), point =>
            {
                Vector3 lightPosition = light.Position;
                Vector3 lightColor = new Vector3((float)light.Color.R / 255f, (float)light.Color.G / 255f, (float)light.Color.B / 255f);
                Vector3 coords = CalculateBarycentricCoords(new Vector2(point.x, point.y), V0, V1, V2);

                float u = coords[0] * gridCoordinates[0].X + coords[1] * gridCoordinates[1].X + coords[2] * gridCoordinates[2].X;
                float v = coords[0] * gridCoordinates[0].Y + coords[1] * gridCoordinates[1].Y + coords[2] * gridCoordinates[2].Y;

                Vector3 objectColor = texture.GetPixelVector(u, v);

                Vector3 N = Vector3.Normalize(coords[0] * normals[0] + coords[1] * normals[1] + coords[2] * normals[2]);

                if (normalTexture != null)
                {
                    Vector3 normal = N;
                    Vector3 tangentU = Vector3.Normalize(coords[0] * tangentUs[0] + coords[1] * tangentUs[1] + coords[2] * tangentUs[2]);
                    Vector3 tangentV = Vector3.Normalize(coords[0] * tangentVs[0] + coords[1] * tangentVs[1] + coords[2] * tangentVs[2]);
                    Vector3 mapNormal = normalTexture.GetPixelNormal(u, v);

                    mapNormal = Vector3.Normalize(mapNormal);

                    N.X = tangentU.X * mapNormal.X + tangentV.X * mapNormal.Y + normal.X * mapNormal.Z;
                    N.Y = tangentU.Y * mapNormal.X + tangentV.Y * mapNormal.Y + normal.Y * mapNormal.Z;
                    N.Z = tangentU.Z * mapNormal.X + tangentV.Z * mapNormal.Y + normal.Z * mapNormal.Z;

                    N = Vector3.Normalize(N);
                }

                float Z = coords[0] * vertices[0].Z + coords[1] * vertices[1].Z + coords[2] * vertices[2].Z;

                Vector3 L = lightPosition - new Vector3(point.x, point.y, Z);
                L = Vector3.Normalize(L);

                Vector3 V = new Vector3(0, 0, 1);

                Vector3 R = 2 * Vector3.Dot(N, L) * N - L;
                R = Vector3.Normalize(R);

                Vector3 diffuse = kd * Vector3.Multiply(lightColor, objectColor) * CosineAngle(N, L);
                Vector3 specular = ks * Vector3.Multiply(lightColor, objectColor) * (float)Math.Pow(CosineAngle(V, R), m);

                Vector3 finalColor = (diffuse + specular) * 255f;

                finalColor = Vector3.Clamp(finalColor, new Vector3(0), new Vector3(255));

                int x = point.x + colors.GetLength(0) / 2, y = point.y + colors.GetLength(1) / 2;

                if (x >= 0 && x < colors.GetLength(0) && y >= 0 && y < colors.GetLength(1))
                {
                    colors[x, y] = Color.FromArgb((int)finalColor.X, (int)finalColor.Y, (int)finalColor.Z);
                }
            });
        }

        private static Vector3 CalculateBarycentricCoords(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
        {
            float detT = (b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y);
            float l1 = ((b.Y - c.Y) * (p.X - c.X) + (c.X - b.X) * (p.Y - c.Y)) / detT;
            float l2 = ((c.Y - a.Y) * (p.X - c.X) + (a.X - c.X) * (p.Y - c.Y)) / detT;
            float l3 = 1 - l1 - l2;

            return new Vector3(l1, l2, l3);
        }

        private static float CosineAngle(Vector3 a, Vector3 b)
        {
            return (float)Math.Max(Vector3.Dot(a, b), 0);
        }
    }
}