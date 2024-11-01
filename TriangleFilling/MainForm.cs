using System.Globalization;
using System.Numerics;
using TriangleFilling.Grid3D;
using TriangleFilling.Lighting;

namespace TriangleFilling
{
    public partial class MainForm : Form
    {
        private Grid Grid;
        private Vector3[,] Coordinates = new Vector3[4, 4];
        private LightSource Light;
        private int Precision
        {
            get
            {
                return precisionTrackBar.Value;
            }
        }

        private bool ShouldDrawOutline
        {
            get
            {
                return outlineCheckbox.Checked;
            }
        }

        private bool ShouldDrawFill
        {
            get
            {
                return fillCheckBox.Checked;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            InitializeGrid();

            Light = new LightSource(new Vector3(0, 0, 0));
            calculateLightPosition();

            Repaint();
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
            Grid.Rotate(alphaDegreeTrackBar.Value, betaDegreeTrackBar.Value);
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.ScaleTransform(1, -1);
            g.TranslateTransform(mainPictureBox.Width / 2, -mainPictureBox.Height / 2);

            Grid.Draw(e.Graphics, Light, ShouldDrawOutline, ShouldDrawFill);
        }

        private void calculateLightPosition()
        {
            if (Light == null) return;

            float x = (float)Math.Cos((float)lightRotationTrackBar.Value * 2.0 * Math.PI / (float)lightRotationTrackBar.Maximum) * 250f;
            float y = (float)Math.Sin((float)lightRotationTrackBar.Value * 2.0 * Math.PI / (float)lightRotationTrackBar.Maximum) * 250f;
            float z = lightHeightTrackBar.Value;

            Light.SetPosition(new Vector3(x, y, z));
        }

        private void Repaint()
        {
            mainPictureBox.Invalidate();
        }

        private void precisionTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision);
            Grid.Rotate(alphaDegreeTrackBar.Value, betaDegreeTrackBar.Value);
            Repaint();
        }

        private void alphaDegreeTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.Rotate(alphaDegreeTrackBar.Value, null);
            Repaint();
        }

        private void betaDegreeTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.Rotate(null, betaDegreeTrackBar.Value);
            Repaint();
        }

        private void fillCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!fillCheckBox.Checked)
            {
                outlineCheckbox.Checked = true;
            }

            Repaint();
        }

        private void outlineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!outlineCheckbox.Checked)
            {
                fillCheckBox.Checked = true;
            }

            Repaint();
        }

        private void kdTrackBar_Scroll(object sender, EventArgs e)
        {
            Repaint();
        }

        private void ksTrackBar_Scroll(object sender, EventArgs e)
        {
            Repaint();
        }

        private void mTrackBar_Scroll(object sender, EventArgs e)
        {
            Repaint();
        }

        private void lightHeightTrackBar_Scroll(object sender, EventArgs e)
        {
            calculateLightPosition();
            Repaint();
        }

        private void lightRotationTrackBar_Scroll(object sender, EventArgs e)
        {
            calculateLightPosition();
            Repaint();
        }
    }
}
