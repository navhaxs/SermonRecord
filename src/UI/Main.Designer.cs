using System.Windows.Forms;
using Sermon_Record.UTIL;

namespace Sermon_Record.UI
{

    partial class Main
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPreferences = new System.Windows.Forms.Button();
            this.elapsedTimeLbl = new System.Windows.Forms.Label();
            this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
            this.soundMeterGTimer = new System.Windows.Forms.Timer(this.components);
            this.soundMeterTTimer = new System.Windows.Forms.Timer(this.components);
            this.soundMeterT = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.fileSize = new System.Windows.Forms.Label();
            this.fileSizeTimer = new System.Windows.Forms.Timer(this.components);
            this.soundMeterG = new Sermon_Record.UTIL.VerticalProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRecord.FlatAppearance.BorderSize = 0;
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Location = new System.Drawing.Point(601, 89);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(165, 100);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "START";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.RecordStartStop_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(87, 217);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(165, 53);
            this.btnExit.TabIndex = 2;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnPreferences
            // 
            this.btnPreferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreferences.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPreferences.FlatAppearance.BorderSize = 0;
            this.btnPreferences.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreferences.Location = new System.Drawing.Point(262, 217);
            this.btnPreferences.Margin = new System.Windows.Forms.Padding(0);
            this.btnPreferences.Name = "btnPreferences";
            this.btnPreferences.Size = new System.Drawing.Size(184, 53);
            this.btnPreferences.TabIndex = 3;
            this.btnPreferences.TabStop = false;
            this.btnPreferences.Text = "Preferences";
            this.btnPreferences.UseVisualStyleBackColor = false;
            this.btnPreferences.Click += new System.EventHandler(this.BtnPreferences_Click);
            // 
            // elapsedTimeLbl
            // 
            this.elapsedTimeLbl.AutoSize = true;
            this.elapsedTimeLbl.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elapsedTimeLbl.Location = new System.Drawing.Point(83, 76);
            this.elapsedTimeLbl.Name = "elapsedTimeLbl";
            this.elapsedTimeLbl.Size = new System.Drawing.Size(392, 94);
            this.elapsedTimeLbl.TabIndex = 4;
            this.elapsedTimeLbl.Text = "00:00:00";
            this.elapsedTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // elapsedTimeTimer
            // 
            this.elapsedTimeTimer.Interval = 700;
            this.elapsedTimeTimer.Tick += new System.EventHandler(this.ElapsedTimeTimer_Tick);
            // 
            // soundMeterGTimer
            // 
            this.soundMeterGTimer.Enabled = true;
            this.soundMeterGTimer.Interval = 30;
            this.soundMeterGTimer.Tick += new System.EventHandler(this.SoundMeterGTimer_Tick);
            // 
            // soundMeterTTimer
            // 
            this.soundMeterTTimer.Enabled = true;
            this.soundMeterTTimer.Interval = 1000;
            this.soundMeterTTimer.Tick += new System.EventHandler(this.SoundMeterTTimer_Tick);
            // 
            // soundMeterT
            // 
            this.soundMeterT.AutoSize = true;
            this.soundMeterT.BackColor = System.Drawing.Color.Red;
            this.soundMeterT.Location = new System.Drawing.Point(0, 235);
            this.soundMeterT.Name = "soundMeterT";
            this.soundMeterT.Size = new System.Drawing.Size(20, 28);
            this.soundMeterT.TabIndex = 6;
            this.soundMeterT.Text = "..";
            this.soundMeterT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(94, 170);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(91, 28);
            this.lblFileSize.TabIndex = 19;
            this.lblFileSize.Text = "File Size: ";
            this.lblFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFileSize.Visible = false;
            // 
            // fileSize
            // 
            this.fileSize.AutoSize = true;
            this.fileSize.Location = new System.Drawing.Point(192, 170);
            this.fileSize.Name = "fileSize";
            this.fileSize.Size = new System.Drawing.Size(0, 28);
            this.fileSize.TabIndex = 20;
            this.fileSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileSize.Visible = false;
            // 
            // fileSizeTimer
            // 
            this.fileSizeTimer.Interval = 1500;
            this.fileSizeTimer.Tick += new System.EventHandler(this.FileSizeTimer_Tick);
            // 
            // soundMeterG
            // 
            this.soundMeterG.Location = new System.Drawing.Point(0, 0);
            this.soundMeterG.Margin = new System.Windows.Forms.Padding(0);
            this.soundMeterG.MarqueeAnimationSpeed = 0;
            this.soundMeterG.Maximum = 66;
            this.soundMeterG.Name = "soundMeterG";
            this.soundMeterG.Size = new System.Drawing.Size(40, 224);
            this.soundMeterG.Step = 0;
            this.soundMeterG.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.soundMeterG.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.soundMeterT);
            this.panel1.Controls.Add(this.soundMeterG);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(73, 274);
            this.panel1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(89, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 59);
            this.label1.TabIndex = 22;
            this.label1.Text = "Now Recording...";
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fileSize);
            this.Controls.Add(this.elapsedTimeLbl);
            this.Controls.Add(this.btnPreferences);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.lblFileSize);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Main";
            this.Size = new System.Drawing.Size(799, 283);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPreferences;
        private System.Windows.Forms.Timer elapsedTimeTimer;
        public System.Windows.Forms.Label elapsedTimeLbl;
        private VerticalProgressBar soundMeterG;
        private System.Windows.Forms.Timer soundMeterGTimer;
        private System.Windows.Forms.Timer soundMeterTTimer;
        private System.Windows.Forms.Label soundMeterT;
        private Label lblFileSize;
        private Label fileSize;
        private Timer fileSizeTimer;
        private Panel panel1;
        private Label label1;
    }
}
