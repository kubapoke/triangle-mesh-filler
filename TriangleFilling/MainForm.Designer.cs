namespace TriangleFilling
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainSplitContainer = new SplitContainer();
            mainPictureBox = new PictureBox();
            label3 = new Label();
            betaDegreeTrackBar = new TrackBar();
            label2 = new Label();
            alphaDegreeTrackBar = new TrackBar();
            label1 = new Label();
            precisionTrackBar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)betaDegreeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alphaDegreeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)precisionTrackBar).BeginInit();
            SuspendLayout();
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel2;
            mainSplitContainer.IsSplitterFixed = true;
            mainSplitContainer.Location = new Point(0, 0);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(mainPictureBox);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(label3);
            mainSplitContainer.Panel2.Controls.Add(betaDegreeTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label2);
            mainSplitContainer.Panel2.Controls.Add(alphaDegreeTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label1);
            mainSplitContainer.Panel2.Controls.Add(precisionTrackBar);
            mainSplitContainer.Size = new Size(800, 450);
            mainSplitContainer.SplitterDistance = 550;
            mainSplitContainer.TabIndex = 0;
            // 
            // mainPictureBox
            // 
            mainPictureBox.Dock = DockStyle.Fill;
            mainPictureBox.Location = new Point(0, 0);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new Size(550, 450);
            mainPictureBox.TabIndex = 0;
            mainPictureBox.TabStop = false;
            mainPictureBox.Paint += mainPictureBox_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 117);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 5;
            label3.Text = "beta deg:";
            // 
            // betaDegreeTrackBar
            // 
            betaDegreeTrackBar.Location = new Point(69, 114);
            betaDegreeTrackBar.Minimum = -10;
            betaDegreeTrackBar.Name = "betaDegreeTrackBar";
            betaDegreeTrackBar.Size = new Size(174, 45);
            betaDegreeTrackBar.TabIndex = 4;
            betaDegreeTrackBar.Scroll += betaDegreeTrackBar_Scroll;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 66);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 3;
            label2.Text = "alpha deg:";
            // 
            // alphaDegreeTrackBar
            // 
            alphaDegreeTrackBar.Location = new Point(69, 63);
            alphaDegreeTrackBar.Maximum = 45;
            alphaDegreeTrackBar.Minimum = -45;
            alphaDegreeTrackBar.Name = "alphaDegreeTrackBar";
            alphaDegreeTrackBar.Size = new Size(174, 45);
            alphaDegreeTrackBar.TabIndex = 2;
            alphaDegreeTrackBar.Scroll += alphaDegreeTrackBar_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 15);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 1;
            label1.Text = "precision:";
            // 
            // precisionTrackBar
            // 
            precisionTrackBar.Location = new Point(69, 12);
            precisionTrackBar.Maximum = 64;
            precisionTrackBar.Minimum = 1;
            precisionTrackBar.Name = "precisionTrackBar";
            precisionTrackBar.Size = new Size(174, 45);
            precisionTrackBar.TabIndex = 0;
            precisionTrackBar.Value = 16;
            precisionTrackBar.Scroll += precisionTrackBar_Scroll;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainSplitContainer);
            MinimumSize = new Size(816, 489);
            Name = "MainForm";
            Text = "Triangle Filler";
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            mainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)betaDegreeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)alphaDegreeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)precisionTrackBar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer mainSplitContainer;
        private PictureBox mainPictureBox;
        private TrackBar precisionTrackBar;
        private Label label1;
        private Label label3;
        private TrackBar betaDegreeTrackBar;
        private Label label2;
        private TrackBar alphaDegreeTrackBar;
    }
}
