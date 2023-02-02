using System;
using System.Windows.Forms;

namespace Tone_detector_GUI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            for (int i = 0; i < 35; i++)
            {
                string[] text = { Form1.detection_result[i + 1], Form1.detection_result[i + 2] };
                listView1.Items.Add(Form1.detection_result[i]).SubItems.AddRange(text);
                //listView1.Items.Add("Note " + Form1.detection_result[i] + " is at the frequency of " +Form1.detection_result[i+1]+" Hz with an amplitude of " + Form1.detection_result[i+2]);
                i = i + 2;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
