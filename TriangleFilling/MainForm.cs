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
        private Task AnimationTask;
        private bool ShouldAnimate = true;
        private Color SurfaceColor, LightColor;
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

        private int M
        {
            get
            {
                return mTrackBar.Value;
            }
        }

        private float Kd
        {
            get
            {
                return (float)kdTrackBar.Value / 100f;
            }
        }

        private float Ks
        {
            get
            {
                return (float)ksTrackBar.Value / 100f;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            InitializeGrid();

            Light = new LightSource(new Vector3(0, 0, 0), Color.White);
            calculateLightPosition();

            SurfaceColor = LightColor = Color.White;

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

            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision, Kd, Ks, M, Color.White);
            Grid.Rotate(alphaDegreeTrackBar.Value, betaDegreeTrackBar.Value);
            AnimationTask = Task.Run(() => AnimateRotation());
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

            float x = (float)Math.Cos((float)lightRotationTrackBar.Value * 2.0 * Math.PI / (float)lightRotationTrackBar.Maximum) * 500f;
            float y = (float)Math.Sin((float)lightRotationTrackBar.Value * 2.0 * Math.PI / (float)lightRotationTrackBar.Maximum) * 500f;
            float z = lightHeightTrackBar.Value;

            Light.SetPosition(new Vector3(x, y, z));
        }

        private void Repaint()
        {
            mainPictureBox.Invalidate();
        }

        private void AnimateRotation()
        {
            while (ShouldAnimate)
            {
                Thread.Sleep(20);

                lightRotationTrackBar.Value++;
                if (lightRotationTrackBar.Value >= lightRotationTrackBar.Maximum)
                    lightRotationTrackBar.Value = lightRotationTrackBar.Minimum;

                calculateLightPosition();
                Repaint();
            }
        }

        private void precisionTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision, Kd, Ks, M, Color.White);
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
            Grid.SetKd(Kd);
            Repaint();
        }

        private void ksTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.SetKs(Ks);
            Repaint();
        }

        private void mTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.SetM(M);
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

        private void animationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (animationCheckBox.Checked)
            {
                ShouldAnimate = true;
                lightRotationTrackBar.Enabled = false;
                AnimationTask = Task.Run(() => AnimateRotation());
            }
            else
            {
                ShouldAnimate = false;
                lightRotationTrackBar.Enabled = true;
                AnimationTask.Wait();
            }
        }

        private void surfaceColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AllowFullOpen = false;
            dialog.ShowHelp = true;
            dialog.Color = SurfaceColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                surfaceColorPanel.BackColor = SurfaceColor = dialog.Color;
                Grid.SetColor(SurfaceColor);
            }

            Repaint();
        }

        private void lightColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AllowFullOpen = false;
            dialog.ShowHelp = true;
            dialog.Color = LightColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                lightColorPanel.BackColor = LightColor = dialog.Color;
                Light.Color = LightColor;
            }

            Repaint();
        }
    }
}
