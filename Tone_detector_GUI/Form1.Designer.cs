namespace Tone_detector_GUI
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label_main = new System.Windows.Forms.Label();
            this.label_result = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label_file_path = new System.Windows.Forms.Label();
            this.button_detect_pitch = new System.Windows.Forms.Button();
            progress_bar_detecting = new System.Windows.Forms.ProgressBar();
            this.tooltip_frequency = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hammingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hanningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackmanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cosineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kaiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawSpectrumGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amplitudeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magnitudeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highPassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip_file_info = new System.Windows.Forms.ToolTip(this.components);
            timer_real_time = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_main
            // 
            this.label_main.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label_main.Location = new System.Drawing.Point(-5, 51);
            this.label_main.Name = "label_main";
            this.label_main.Size = new System.Drawing.Size(793, 49);
            this.label_main.TabIndex = 0;
            this.label_main.Text = "Tone detector";
            this.label_main.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_result
            // 
            this.label_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_result.Location = new System.Drawing.Point(325, 196);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(137, 40);
            this.label_result.TabIndex = 2;
            this.label_result.Text = "label1";
            this.label_result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_result.Visible = false;
            this.label_result.Click += new System.EventHandler(this.label_result_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label_file_path
            // 
            this.label_file_path.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_file_path.Location = new System.Drawing.Point(-13, 100);
            this.label_file_path.Name = "label_file_path";
            this.label_file_path.Size = new System.Drawing.Size(801, 31);
            this.label_file_path.TabIndex = 4;
            this.label_file_path.Text = "label1";
            this.label_file_path.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_file_path.Visible = false;
            this.label_file_path.Click += new System.EventHandler(this.label_file_path_Click);
            // 
            // button_detect_pitch
            // 
            this.button_detect_pitch.Location = new System.Drawing.Point(315, 140);
            this.button_detect_pitch.Name = "button_detect_pitch";
            this.button_detect_pitch.Size = new System.Drawing.Size(147, 35);
            this.button_detect_pitch.TabIndex = 5;
            this.button_detect_pitch.Text = "detect note";
            this.button_detect_pitch.UseVisualStyleBackColor = true;
            this.button_detect_pitch.Click += new System.EventHandler(this.button_detect_pitch_Click);
            // 
            // progress_bar_detecting
            // 
            progress_bar_detecting.Location = new System.Drawing.Point(294, 196);
            progress_bar_detecting.Name = "progress_bar_detecting";
            progress_bar_detecting.Size = new System.Drawing.Size(195, 23);
            progress_bar_detecting.TabIndex = 6;
            progress_bar_detecting.Visible = false;
            progress_bar_detecting.Click += new System.EventHandler(this.progress_bar_detecting_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(778, 33);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSampleToolStripMenuItem,
            this.recordToolStripMenuItem,
            this.realTimeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(89, 29);
            this.fileToolStripMenuItem.Text = "Analyze";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // loadSampleToolStripMenuItem
            // 
            this.loadSampleToolStripMenuItem.Name = "loadSampleToolStripMenuItem";
            this.loadSampleToolStripMenuItem.Size = new System.Drawing.Size(215, 34);
            this.loadSampleToolStripMenuItem.Text = "Load sample";
            this.loadSampleToolStripMenuItem.Click += new System.EventHandler(this.loadSampleToolStripMenuItem_Click);
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(215, 34);
            this.recordToolStripMenuItem.Text = "Record";
            this.recordToolStripMenuItem.Click += new System.EventHandler(this.recordToolStripMenuItem_Click);
            // 
            // realTimeToolStripMenuItem
            // 
            this.realTimeToolStripMenuItem.Name = "realTimeToolStripMenuItem";
            this.realTimeToolStripMenuItem.Size = new System.Drawing.Size(215, 34);
            this.realTimeToolStripMenuItem.Text = "Real time";
            this.realTimeToolStripMenuItem.Click += new System.EventHandler(this.realTimeToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowFunctionToolStripMenuItem,
            this.drawSpectrumGraphToolStripMenuItem,
            this.amplitudeToolStripMenuItem,
            this.highPassToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(118, 29);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // windowFunctionToolStripMenuItem
            // 
            this.windowFunctionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.hammingToolStripMenuItem,
            this.hanningToolStripMenuItem,
            this.blackmanToolStripMenuItem,
            this.cosineToolStripMenuItem,
            this.kaiserToolStripMenuItem,
            this.rectangularToolStripMenuItem});
            this.windowFunctionToolStripMenuItem.Name = "windowFunctionToolStripMenuItem";
            this.windowFunctionToolStripMenuItem.Size = new System.Drawing.Size(286, 34);
            this.windowFunctionToolStripMenuItem.Text = "Window function";
            this.windowFunctionToolStripMenuItem.Click += new System.EventHandler(this.windowFunctionToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.noneToolStripMenuItem.Text = "none";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // hammingToolStripMenuItem
            // 
            this.hammingToolStripMenuItem.Name = "hammingToolStripMenuItem";
            this.hammingToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.hammingToolStripMenuItem.Text = "Hamming";
            this.hammingToolStripMenuItem.Click += new System.EventHandler(this.hammingToolStripMenuItem_Click);
            // 
            // hanningToolStripMenuItem
            // 
            this.hanningToolStripMenuItem.Checked = true;
            this.hanningToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hanningToolStripMenuItem.Name = "hanningToolStripMenuItem";
            this.hanningToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.hanningToolStripMenuItem.Text = "Hanning";
            this.hanningToolStripMenuItem.Click += new System.EventHandler(this.hanningToolStripMenuItem_Click);
            // 
            // blackmanToolStripMenuItem
            // 
            this.blackmanToolStripMenuItem.Name = "blackmanToolStripMenuItem";
            this.blackmanToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.blackmanToolStripMenuItem.Text = "Blackman";
            this.blackmanToolStripMenuItem.Click += new System.EventHandler(this.blackmanToolStripMenuItem_Click);
            // 
            // cosineToolStripMenuItem
            // 
            this.cosineToolStripMenuItem.Name = "cosineToolStripMenuItem";
            this.cosineToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.cosineToolStripMenuItem.Text = "Cosine";
            this.cosineToolStripMenuItem.Click += new System.EventHandler(this.cosineToolStripMenuItem_Click);
            // 
            // kaiserToolStripMenuItem
            // 
            this.kaiserToolStripMenuItem.Name = "kaiserToolStripMenuItem";
            this.kaiserToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.kaiserToolStripMenuItem.Text = "Kaiser";
            this.kaiserToolStripMenuItem.Click += new System.EventHandler(this.kaiserToolStripMenuItem_Click);
            // 
            // rectangularToolStripMenuItem
            // 
            this.rectangularToolStripMenuItem.Name = "rectangularToolStripMenuItem";
            this.rectangularToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.rectangularToolStripMenuItem.Text = "Rectangular";
            this.rectangularToolStripMenuItem.Click += new System.EventHandler(this.rectangularToolStripMenuItem_Click);
            // 
            // drawSpectrumGraphToolStripMenuItem
            // 
            this.drawSpectrumGraphToolStripMenuItem.Checked = true;
            this.drawSpectrumGraphToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawSpectrumGraphToolStripMenuItem.Name = "drawSpectrumGraphToolStripMenuItem";
            this.drawSpectrumGraphToolStripMenuItem.Size = new System.Drawing.Size(286, 34);
            this.drawSpectrumGraphToolStripMenuItem.Text = "Draw spectrum graph";
            this.drawSpectrumGraphToolStripMenuItem.Click += new System.EventHandler(this.drawSpectrumGraphToolStripMenuItem_Click);
            // 
            // amplitudeToolStripMenuItem
            // 
            this.amplitudeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.magnitudeToolStripMenuItem,
            this.powerToolStripMenuItem});
            this.amplitudeToolStripMenuItem.Name = "amplitudeToolStripMenuItem";
            this.amplitudeToolStripMenuItem.Size = new System.Drawing.Size(286, 34);
            this.amplitudeToolStripMenuItem.Text = "Y-axis";
            // 
            // magnitudeToolStripMenuItem
            // 
            this.magnitudeToolStripMenuItem.Name = "magnitudeToolStripMenuItem";
            this.magnitudeToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.magnitudeToolStripMenuItem.Text = "Magnitude";
            this.magnitudeToolStripMenuItem.Click += new System.EventHandler(this.magnitudeToolStripMenuItem_Click);
            // 
            // powerToolStripMenuItem
            // 
            this.powerToolStripMenuItem.Name = "powerToolStripMenuItem";
            this.powerToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.powerToolStripMenuItem.Text = "Power";
            this.powerToolStripMenuItem.Click += new System.EventHandler(this.powerToolStripMenuItem_Click);
            // 
            // highPassToolStripMenuItem
            // 
            this.highPassToolStripMenuItem.Name = "highPassToolStripMenuItem";
            this.highPassToolStripMenuItem.Size = new System.Drawing.Size(286, 34);
            this.highPassToolStripMenuItem.Text = "High pass";
            this.highPassToolStripMenuItem.Click += new System.EventHandler(this.highPassToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolTip_file_info
            // 
            this.toolTip_file_info.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // timer_real_time
            // 
            timer_real_time.Interval = 500;
            timer_real_time.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 261);
            this.Controls.Add(progress_bar_detecting);
            this.Controls.Add(this.button_detect_pitch);
            this.Controls.Add(this.label_file_path);
            this.Controls.Add(this.label_result);
            this.Controls.Add(this.label_main);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tone detector";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_main;
        private System.Windows.Forms.Label label_result;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label_file_path;
        private System.Windows.Forms.Button button_detect_pitch;
        private System.Windows.Forms.ToolTip tooltip_frequency;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hammingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hanningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackmanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cosineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kaiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawSpectrumGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amplitudeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem magnitudeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highPassToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip_file_info;
        private System.Windows.Forms.ToolStripMenuItem rectangularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realTimeToolStripMenuItem;
        public static System.Windows.Forms.Timer timer_real_time;
        public static System.Windows.Forms.ProgressBar progress_bar_detecting;
    }
}

