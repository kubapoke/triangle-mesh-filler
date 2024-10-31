using System.Globalization;
using System.Numerics;
using TriangleFilling.Grid3D;

namespace TriangleFilling
{
    public partial class MainForm : Form
    {
        private Grid Grid;
        private Vector3[,] Coordinates = new Vector3[4, 4];
        private int Precision
        {
            get
            {
                return precisionTrackBar.Value;
            }
        }
        public MainForm()
        {
            InitializeComponent();

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            float multiplier = Math.Min(mainPictureBox.Width, mainPictureBox.Height) * 0.5f;

            using (StreamReader sr = new StreamReader(".\\input.txt"))
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        string? line = sr.ReadLine();

                        if (line == null)
                        {
                            return;
                        }

                        string[] parts = line.Split(',');
                        float x = float.Parse(parts[0], CultureInfo.InvariantCulture) * multiplier;
                        float y = float.Parse(parts[1], CultureInfo.InvariantCulture) * multiplier;
                        float z = float.Parse(parts[2], CultureInfo.InvariantCulture) * multiplier;

                        Coordinates[i, j] = new Vector3(x, y, z);
                    }
                }
            }

            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision);
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.ScaleTransform(1, -1);
            g.TranslateTransform(mainPictureBox.Width / 2, -mainPictureBox.Height / 2);

            Grid.Draw(e.Graphics);
        }

        private void Repaint()
        {
            mainPictureBox.Invalidate();
        }

        private void precisionTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision);
            Repaint();
        }
    }
}
