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

        private bool ShouldDrawLight
        {
            get
            {
                return showLightCheckBox.Checked;
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

        private int lightHeight
        {
            get
            {
                return lightHeightTrackBar.Value;
            }
        }

        private int lightRadius
        {
            get
            {
                return lightRadiusTrackBar.Value;
            }
        }

        private int lightRotation
        {
            get
            {
                return lightRotationTrackBar.Value;
            }
            set
            {
                lightRotationTrackBar.Value = value;
            }
        }

        private int lightRotationMaximum
        {
            get
            {
                return lightRotationTrackBar.Maximum;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            InitializeGrid();

            InitializeLighting();

            InitializeControls();

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

                        string[] parts = line.Split(' ');
                        float x = float.Parse(parts[0], CultureInfo.InvariantCulture) * multiplier;
                        float y = float.Parse(parts[1], CultureInfo.InvariantCulture) * multiplier;
                        float z = float.Parse(parts[2], CultureInfo.InvariantCulture) * multiplier;

                        Coordinates[i, j] = new Vector3(x, y, z);
                    }
                }
            }

            SurfaceColor = surfaceColorPanel.BackColor;
            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision, Kd, Ks, M, SurfaceColor);
            Grid.Rotate(alphaDegreeTrackBar.Value, betaDegreeTrackBar.Value);
        }

        private void InitializeLighting()
        {
            Light = new LightSource(new Vector3(0, 0, 0), Color.White);
            calculateLightPosition();

            LightColor = lightColorPanel.BackColor;
        }

        private void InitializeControls()
        {
            if (animationCheckBox.Checked)
            {
                AnimationTask = Task.Run(() => AnimateRotation());
                lightRotationTrackBar.Enabled = false;
            }
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.ScaleTransform(1, -1);
            g.TranslateTransform(mainPictureBox.Width / 2, -mainPictureBox.Height / 2);

            Grid.Draw(e.Graphics, Light, ShouldDrawOutline, ShouldDrawFill, ShouldDrawLight);
        }

        private void calculateLightPosition()
        {
            if (Light == null) return;

            float x = (float)Math.Cos((float)lightRotation * 2.0 * Math.PI / (float)lightRotationMaximum) * (float)lightRadius;
            float y = (float)Math.Sin((float)lightRotation * 2.0 * Math.PI / (float)lightRotationMaximum) * (float)lightRadius;
            float z = lightHeight;

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

                lightRotation = lightRotation + 1;
                if (lightRotation >= lightRotationMaximum)
                    lightRotation = 0;

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

        private void lightRadiusTrackBar_Scroll(object sender, EventArgs e)
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

        private void showLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Repaint();
        }


    }
}
