namespace TriangleFilling.Grid3D
{
    internal class Texture
    {
        public Bitmap Bitmap;
        private int Width, Height;

        public Texture(string imagePath)
        {
            Bitmap = (Bitmap)Image.FromFile(imagePath);
            Width = Bitmap.Width;
            Height = Bitmap.Height;
        }

        public Color GetPixel(float u, float v)
        {
            int x = (int)(Width * u);
            int y = (int)(Height * v);
            return Bitmap.GetPixel(x, y);
        }
    }
}
