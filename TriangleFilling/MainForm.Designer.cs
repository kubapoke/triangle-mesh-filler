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
            animationCheckBox = new CheckBox();
            label7 = new Label();
            lightRotationTrackBar = new TrackBar();
            label8 = new Label();
            lightHeightTrackBar = new TrackBar();
            label4 = new Label();
            mTrackBar = new TrackBar();
            label5 = new Label();
            ksTrackBar = new TrackBar();
            label6 = new Label();
            kdTrackBar = new TrackBar();
            fillCheckBox = new CheckBox();
            outlineCheckbox = new CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)lightRotationTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lightHeightTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ksTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kdTrackBar).BeginInit();
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
            mainSplitContainer.Panel2.Controls.Add(animationCheckBox);
            mainSplitContainer.Panel2.Controls.Add(label7);
            mainSplitContainer.Panel2.Controls.Add(lightRotationTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label8);
            mainSplitContainer.Panel2.Controls.Add(lightHeightTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label4);
            mainSplitContainer.Panel2.Controls.Add(mTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label5);
            mainSplitContainer.Panel2.Controls.Add(ksTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label6);
            mainSplitContainer.Panel2.Controls.Add(kdTrackBar);
            mainSplitContainer.Panel2.Controls.Add(fillCheckBox);
            mainSplitContainer.Panel2.Controls.Add(outlineCheckbox);
            mainSplitContainer.Panel2.Controls.Add(label3);
            mainSplitContainer.Panel2.Controls.Add(betaDegreeTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label2);
            mainSplitContainer.Panel2.Controls.Add(alphaDegreeTrackBar);
            mainSplitContainer.Panel2.Controls.Add(label1);
            mainSplitContainer.Panel2.Controls.Add(precisionTrackBar);
            mainSplitContainer.Size = new Size(800, 521);
            mainSplitContainer.SplitterDistance = 550;
            mainSplitContainer.TabIndex = 0;
            // 
            // mainPictureBox
            // 
            mainPictureBox.Dock = DockStyle.Fill;
            mainPictureBox.Location = new Point(0, 0);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new Size(550, 521);
            mainPictureBox.TabIndex = 0;
            mainPictureBox.TabStop = false;
            mainPictureBox.Paint += mainPictureBox_Paint;
            // 
            // animationCheckBox
            // 
            animationCheckBox.AutoSize = true;
            animationCheckBox.Checked = true;
            animationCheckBox.CheckState = CheckState.Checked;
            animationCheckBox.Location = new Point(21, 445);
            animationCheckBox.Name = "animationCheckBox";
            animationCheckBox.Size = new Size(96, 19);
            animationCheckBox.TabIndex = 18;
            animationCheckBox.Text = "animate light";
            animationCheckBox.UseVisualStyleBackColor = true;
            animationCheckBox.CheckedChanged += animationCheckBox_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 372);
            label7.Name = "label7";
            label7.Size = new Size(52, 15);
            label7.TabIndex = 17;
            label7.Text = "light rot:";
            // 
            // lightRotationTrackBar
            // 
            lightRotationTrackBar.Enabled = false;
            lightRotationTrackBar.Location = new Point(69, 369);
            lightRotationTrackBar.Maximum = 200;
            lightRotationTrackBar.Name = "lightRotationTrackBar";
            lightRotationTrackBar.Size = new Size(174, 45);
            lightRotationTrackBar.TabIndex = 16;
            lightRotationTrackBar.TickFrequency = 20;
            lightRotationTrackBar.Scroll += lightRotationTrackBar_Scroll;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 321);
            label8.Name = "label8";
            label8.Size = new Size(48, 15);
            label8.TabIndex = 15;
            label8.Text = "light ht:";
            // 
            // lightHeightTrackBar
            // 
            lightHeightTrackBar.Location = new Point(69, 318);
            lightHeightTrackBar.Maximum = 750;
            lightHeightTrackBar.Minimum = 250;
            lightHeightTrackBar.Name = "lightHeightTrackBar";
            lightHeightTrackBar.Size = new Size(174, 45);
            lightHeightTrackBar.TabIndex = 14;
            lightHeightTrackBar.TickFrequency = 50;
            lightHeightTrackBar.Value = 600;
            lightHeightTrackBar.Scroll += lightHeightTrackBar_Scroll;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 270);
            label4.Name = "label4";
            label4.Size = new Size(21, 15);
            label4.TabIndex = 13;
            label4.Text = "m:";
            // 
            // mTrackBar
            // 
            mTrackBar.Location = new Point(69, 267);
            mTrackBar.Maximum = 100;
            mTrackBar.Minimum = 1;
            mTrackBar.Name = "mTrackBar";
            mTrackBar.Size = new Size(174, 45);
            mTrackBar.TabIndex = 12;
            mTrackBar.TickFrequency = 10;
            mTrackBar.Value = 20;
            mTrackBar.Scroll += mTrackBar_Scroll;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 219);
            label5.Name = "label5";
            label5.Size = new Size(21, 15);
            label5.TabIndex = 11;
            label5.Text = "ks:";
            // 
            // ksTrackBar
            // 
            ksTrackBar.Location = new Point(69, 216);
            ksTrackBar.Maximum = 100;
            ksTrackBar.Name = "ksTrackBar";
            ksTrackBar.Size = new Size(174, 45);
            ksTrackBar.TabIndex = 10;
            ksTrackBar.TickFrequency = 9;
            ksTrackBar.Value = 30;
            ksTrackBar.Scroll += ksTrackBar_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 168);
            label6.Name = "label6";
            label6.Size = new Size(23, 15);
            label6.TabIndex = 9;
            label6.Text = "kd:";
            // 
            // kdTrackBar
            // 
            kdTrackBar.Location = new Point(69, 165);
            kdTrackBar.Maximum = 100;
            kdTrackBar.Name = "kdTrackBar";
            kdTrackBar.Size = new Size(174, 45);
            kdTrackBar.TabIndex = 8;
            kdTrackBar.TickFrequency = 10;
            kdTrackBar.Value = 70;
            kdTrackBar.Scroll += kdTrackBar_Scroll;
            // 
            // fillCheckBox
            // 
            fillCheckBox.AutoSize = true;
            fillCheckBox.Checked = true;
            fillCheckBox.CheckState = CheckState.Checked;
            fillCheckBox.Location = new Point(144, 420);
            fillCheckBox.Name = "fillCheckBox";
            fillCheckBox.Size = new Size(70, 19);
            fillCheckBox.TabIndex = 7;
            fillCheckBox.Text = "show fill";
            fillCheckBox.UseVisualStyleBackColor = true;
            fillCheckBox.CheckedChanged += fillCheckBox_CheckedChanged;
            // 
            // outlineCheckbox
            // 
            outlineCheckbox.AutoSize = true;
            outlineCheckbox.Checked = true;
            outlineCheckbox.CheckState = CheckState.Checked;
            outlineCheckbox.Location = new Point(21, 420);
            outlineCheckbox.Name = "outlineCheckbox";
            outlineCheckbox.Size = new Size(94, 19);
            outlineCheckbox.TabIndex = 6;
            outlineCheckbox.Text = "show outline";
            outlineCheckbox.UseVisualStyleBackColor = true;
            outlineCheckbox.CheckedChanged += outlineCheckBox_CheckedChanged;
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
            betaDegreeTrackBar.Name = "betaDegreeTrackBar";
            betaDegreeTrackBar.Size = new Size(174, 45);
            betaDegreeTrackBar.TabIndex = 4;
            betaDegreeTrackBar.TickFrequency = 2;
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
            alphaDegreeTrackBar.TickFrequency = 9;
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
            precisionTrackBar.Maximum = 128;
            precisionTrackBar.Minimum = 1;
            precisionTrackBar.Name = "precisionTrackBar";
            precisionTrackBar.Size = new Size(174, 45);
            precisionTrackBar.TabIndex = 0;
            precisionTrackBar.TickFrequency = 16;
            precisionTrackBar.Value = 16;
            precisionTrackBar.Scroll += precisionTrackBar_Scroll;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 521);
            Controls.Add(mainSplitContainer);
            MinimumSize = new Size(816, 560);
            Name = "MainForm";
            Text = "Triangle Filler";
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            mainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)lightRotationTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)lightHeightTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)mTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)ksTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)kdTrackBar).EndInit();
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
        private CheckBox outlineCheckbox;
        private CheckBox fillCheckBox;
        private Label label4;
        private TrackBar mTrackBar;
        private Label label5;
        private TrackBar ksTrackBar;
        private Label label6;
        private TrackBar kdTrackBar;
        private Label label7;
        private TrackBar lightRotationTrackBar;
        private Label label8;
        private TrackBar lightHeightTrackBar;
        private CheckBox animationCheckBox;
    }
}
