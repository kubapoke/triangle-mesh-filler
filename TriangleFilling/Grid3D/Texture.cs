using System.Numerics;

namespace TriangleFilling.Grid3D
{
    internal class Texture
    {
        public Bitmap Bitmap;
        private int Width, Height;
        private Color[,] Colors;

        public Texture(string imagePath)
        {
            Bitmap = (Bitmap)Image.FromFile(imagePath);
            Width = Bitmap.Width;
            Height = Bitmap.Height;
            Colors = new Color[Width, Height];

            for(int x = 0;  x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    Colors[x, y] = Bitmap.GetPixel(x, y);
                }
            }
        }

        public Color GetPixelColor(float u, float v)
        {
            int x = (int)(Width * u);
            int y = (int)(Height * (1 - v));

            if (x < 0) x = 0;
            else if (x >= Width) x = Width - 1;

            if (y < 0) y = 0;
            else if (y >= Height) y = Height - 1;

                return Colors[x, y];     
        }

        public Vector3 GetPixelVector(float u, float v)
        {
            Color pixelColor = GetPixelColor(u, v);
            return new Vector3(pixelColor.R / 255f, pixelColor.G / 255f, pixelColor.B / 255f);
        }
    }
}
