using System;
using System.Windows.Forms;

namespace Tone_detector_GUI
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            int parsed;
            if (!int.TryParse(TextBox_high_pass.Text, out parsed) || parsed < 0 || parsed > 24000)
            {
                MessageBox.Show("Enter whole number 0-24000");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
