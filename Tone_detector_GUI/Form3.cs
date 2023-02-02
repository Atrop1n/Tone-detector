using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tone_detector_GUI
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile("periodogram.png");
            FormClosing += new FormClosingEventHandler(Form3_FormClosing);
        }



        private void Form3_FormClosing(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            Dispose();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

    }
}
