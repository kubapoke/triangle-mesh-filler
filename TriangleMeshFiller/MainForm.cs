using System.Globalization;
using System.Numerics;
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
        private System.Windows.Forms.Timer AnimationTimer, RepaintTimer;
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
        }

        private void InitializeGrid()
        {
            float multiplier = Math.Min(mainPictureBox.Width, mainPictureBox.Height) * 0.4f;

            using (StreamReader sr = new StreamReader(".\\Assets\\Inputs\\input1.txt"))
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

            Texture = new Texture(".\\Assets\\Textures\\texture1.png");
            NormalTexture = new NormalTexture(".\\Assets\\NormalMaps\\normalmap1.png");

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

        private void InitializeTimers()
        {
            AnimationTimer = new System.Windows.Forms.Timer();
            AnimationTimer.Tick += new EventHandler(onAnimationTimer_Tick);
            AnimationTimer.Interval = 20;

            if (animationCheckBox.Checked)
            {
                AnimationTimer.Enabled = true;
                lightRotationTrackBar.Enabled = false;
            }

            RepaintTimer = new System.Windows.Forms.Timer();
            RepaintTimer.Tick += new EventHandler(onRepaintTimer_Tick);
            RepaintTimer.Interval = 20;
            RepaintTimer.Enabled = true;
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
            mainPictureBox.Update();
        }

        public void onAnimationTimer_Tick(object source, EventArgs e)
        {
            AnimateRotation();
        }

        public void onRepaintTimer_Tick(object source, EventArgs e)
        {
            Repaint();
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
        }

        private void precisionTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid = new Grid(Coordinates, mainPictureBox.Width, mainPictureBox.Height, Precision, Kd, Ks, M, Texture, NormalTexture);
            Grid.Rotate(alphaDegreeTrackBar.Value, betaDegreeTrackBar.Value);
        }

        private void alphaDegreeTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.Rotate(alphaDegreeTrackBar.Value, null);
        }

        private void betaDegreeTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.Rotate(null, betaDegreeTrackBar.Value);
        }

        private void fillCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!fillCheckBox.Checked)
            {
                outlineCheckbox.Checked = true;
                DirectBitmap.Clear();
            }
        }

        private void outlineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!outlineCheckbox.Checked)
            {
                fillCheckBox.Checked = true;
            }
        }

        private void kdTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.Kd = Kd;
        }

        private void ksTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.Ks = Ks;
        }

        private void mTrackBar_Scroll(object sender, EventArgs e)
        {
            Grid.M = M;
        }

        private void lightHeightTrackBar_Scroll(object sender, EventArgs e)
        {
            calculateLightPosition();
        }

        private void lightRotationTrackBar_Scroll(object sender, EventArgs e)
        {
            calculateLightPosition();
        }

        private void lightRadiusTrackBar_Scroll(object sender, EventArgs e)
        {
            calculateLightPosition();
        }

        private void animationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (animationCheckBox.Checked)
            {
                lightRotationTrackBar.Enabled = false;
                AnimationTimer.Enabled = true;
            }
            else
            {
                lightRotationTrackBar.Enabled = true;
                AnimationTimer.Enabled = false;
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
        }

        private void showLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            InitializeLighting();

            InitializeTimers();
        }

        private void surfaceTextureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Path.GetFullPath(".\\Assets\\Textures\\"),
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp",
            };
            openFileDialog.AutoUpgradeEnabled = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                Texture = new Texture(path);

                Grid.Texture = Texture;
            }
        }

        private void normalTextureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Path.GetFullPath(".\\Assets\\NormalMaps\\"),
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp",
            };
            openFileDialog.AutoUpgradeEnabled = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                NormalTexture = new NormalTexture(path);

                Grid.NormalTexture = NormalTexture;
            }

            useNormalTextureCheckbox.Checked = true;
        }

        private void useNormalTextureCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
