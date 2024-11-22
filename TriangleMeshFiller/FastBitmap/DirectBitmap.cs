using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TriangleFilling.FastBitmap
{
    // from https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
    internal class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        private (int x, int y) TransformCoords(int x, int y)
        {
            return (x, -y + Height);
        }

        public void SetPixel(int x, int y, Color color)
        {
            (x, y) = TransformCoords(x, y);

            int index = x + (y * Width);
            int col = color.ToArgb();

            if (index >= 0 && index < Bits.Length)
                Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            (x, y) = TransformCoords(x, y);

            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Clear()
        {
            for(int x = 0; x < Bitmap.Width; x++)
            {
                for (int y = 0; y < Bitmap.Height; y++)
                    SetPixel(x, y, Color.Transparent);
            }
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
