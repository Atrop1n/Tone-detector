namespace Tone_detector_GUI
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.TextBox_high_pass = new System.Windows.Forms.TextBox();
            this.label_high_pass = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox_high_pass
            // 
            this.TextBox_high_pass.Location = new System.Drawing.Point(99, 86);
            this.TextBox_high_pass.Name = "TextBox_high_pass";
            this.TextBox_high_pass.Size = new System.Drawing.Size(184, 26);
            this.TextBox_high_pass.TabIndex = 0;
            this.TextBox_high_pass.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_high_pass
            // 
            this.label_high_pass.AutoSize = true;
            this.label_high_pass.Location = new System.Drawing.Point(145, 42);
            this.label_high_pass.Name = "label_high_pass";
            this.label_high_pass.Size = new System.Drawing.Size(84, 20);
            this.label_high_pass.TabIndex = 1;
            this.label_high_pass.Text = "Value (Hz)";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(55, 137);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(81, 36);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(244, 137);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 36);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 201);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_high_pass);
            this.Controls.Add(this.TextBox_high_pass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter (high pass)";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_high_pass;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_cancel;
        public System.Windows.Forms.TextBox TextBox_high_pass;
    }
}