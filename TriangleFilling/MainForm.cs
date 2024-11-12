using System.Globalization;
using System.Numerics;
using System.Timers;
using TriangleFilling.FastBitmap;
using TriangleFilling.Grid3D;
using TriangleFilling.Lighting;

namespace TriangleFilling
{
    public partial class MainForm : Form
    {
        private Grid Grid;
        private Vector3[,] Coordinates = new Vector3[4, 4];
        private LightSource Light;
        private System.Windows.Forms.Timer Timer;
        private Color LightColor;
        private Texture Texture;
        private NormalTexture NormalTexture;
        private DirectBitmap DirectBitmap;
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

        private bool ShouldUseNormalVectorMap
        {
            get
            {
                return useNormalTextureCheckbox.Checked;
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

            InitializeDrawing();

            Repaint();
        }

        private void InitializeGrid()
        {
            float multiplier = Math.Min(mainPictureBox.Width, mainPictureBox.Height) * 0.4f;

            using (StreamReader sr = new StreamReader(".\\Inputs\\input1.txt"))
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

            Texture = new Texture(".\\Textures\\texture1.png");
            NormalTexture = new NormalTexture(".\\NormalMaps\\normalmap1.png");

            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision, Kd, Ks, M, Texture, NormalTexture);
            Grid.Rotate(alphaDegreeTrackBar.Value, betaDegreeTrackBar.Value);
        }

        private void InitializeDrawing()
        {
            DirectBitmap = new DirectBitmap(mainPictureBox.Width, mainPictureBox.Height);
            mainPictureBox.Image = DirectBitmap.Bitmap;
        }

        private void InitializeLighting()
        {
            Light = new LightSource(new Vector3(0, 0, 0), Color.White);
            calculateLightPosition();

            LightColor = lightColorPanel.BackColor;
        }

        private void InitializeTimer()
        {
            Timer = new System.Windows.Forms.Timer();
            Timer.Tick += new EventHandler(onTimer_Tick);
            Timer.Interval = 20;

            if (animationCheckBox.Checked)
            {
                Timer.Enabled = true;
                lightRotationTrackBar.Enabled = false;
            }
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.ScaleTransform(1, -1);
            g.TranslateTransform(mainPictureBox.Width / 2, -mainPictureBox.Height / 2);

            Grid.Draw(e.Graphics, DirectBitmap, Light, ShouldDrawOutline, ShouldDrawFill, ShouldDrawLight, ShouldUseNormalVectorMap);
        }

        private void calculateLightPosition()
        {
            if (Light == null) return;

            float radius = 0, rotation = 0, maxRotation = 0, height = 0;

            lightRadiusTrackBar.Invoke(() =>
            {
                radius = lightRadius;
            });

            lightRotationTrackBar.Invoke(() =>
            {
                rotation = lightRotation;
                maxRotation = lightRotationMaximum;
            });

            lightHeightTrackBar.Invoke(() =>
            {
                height = lightHeight;
            });

            float x = (float)Math.Cos(rotation * 2.0 * Math.PI / maxRotation) * radius;
            float y = (float)Math.Sin(rotation * 2.0 * Math.PI / maxRotation) * radius;
            float z = height;

            Light.SetPosition(new Vector3(x, y, z));
        }

        private void Repaint()
        {
            mainPictureBox.Invalidate();
        }

        public void onTimer_Tick(object source, EventArgs e)
        {
            AnimateRotation();
        }

        public void AnimateRotation()
        {
            lightRotationTrackBar.Invoke(() =>
            {
                if (lightRotation + 5 > lightRotationMaximum)
                    lightRotation = 0;
                else
                    lightRotation = lightRotation + 5;
            });

            calculateLightPosition();
            Repaint();
        }

        private void precisionTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision, Kd, Ks, M, Texture, NormalTexture);
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
                lightRotationTrackBar.Enabled = false;
                Timer.Enabled = true;
            }
            else
            {
                lightRotationTrackBar.Enabled = true;
                Timer.Enabled = false;
            }
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

        private void MainForm_Shown(object sender, EventArgs e)
        {
            InitializeLighting();

            InitializeTimer();
        }

        private void surfaceTextureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = ".\\Textures\\",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                Texture = new Texture(path);

                Grid.SetTexture(Texture);
            }

            Repaint();
        }

        private void normalTextureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = ".\\NormalMaps\\",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                NormalTexture = new NormalTexture(path);

                Grid.SetNormalTexture(NormalTexture);
            }

            Repaint();
        }

        private void useNormalTextureCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repaint();
        }

        private void mainPictureBox_Resize(object sender, EventArgs e)
        {

        }
    }
}
