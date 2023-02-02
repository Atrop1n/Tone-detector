using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;


namespace Tone_detector_GUI
{
    public partial class Form1 : Form
    {
        public int high_pass = 20;
        string filename;
        string temp_file_name;
        string frequency_detected;
        WaveInEvent waveIn;
        WaveFileWriter writer = null;
        string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tone Detector GUI - Recordings");
        string recorded_file_path;
        public static string[] detection_result = null;
        bool is_recording = false;
        public static bool real_time_analysis = false;
        public Form1()
        {

            InitializeComponent();
            drawSpectrumGraphToolStripMenuItem.Checked = true;
            magnitudeToolStripMenuItem.Checked = true;
            powerToolStripMenuItem.Checked = false;
            label_file_path.Text = "Click here to select a file";
            label_file_path.Visible = true;
            label_file_path.MouseDown += label_file_path_RightClick;
            timer_real_time.Interval = 1000;
        }





        private void button_detect_pitch_Click(object sender, EventArgs e)
        {
            if (is_recording == false)
            {
                if (real_time_analysis == false)
                {
                    if (filename != null)
                    {
                        label_result.Text = "";
                        label_result.Visible = false;
                        progress_bar_detecting.Minimum = 0;
                        progress_bar_detecting.Maximum = 20;
                        detection_result = Program.detect_pitch(filename);
                        label_result.Text = detection_result[0];
                        frequency_detected = detection_result[1];
                        label_result.Visible = true;
                        tooltip_frequency.Active = true;
                        tooltip_frequency.SetToolTip(label_result, frequency_detected + " Hz");
                        if (Program.draw == true)
                        {
                            Form3 f3 = new Form3();
                            if (f3.ShowDialog() == DialogResult.OK)
                            {
                                f3.Dispose();
                                f3.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No file selected.");
                    }
                }
                else
                {
                    MessageBox.Show("Real time analysis in progress!");
                }
            }
            else
            {
                MessageBox.Show("Recording in progress!");
            }
        }

        private void button_record_stop_recording(object sender, EventArgs e)
        {
            is_recording = false;
            Console.WriteLine("Stopped");
            label_file_path.Text = "";
            recordToolStripMenuItem.Text = "Record";
            waveIn.StopRecording();
            writer?.Dispose();
            writer = null;
            waveIn.Dispose();
            waveIn.StopRecording();
            filename = recorded_file_path;
            button_detect_pitch.Visible = true;
            label_file_path.Text = filename;
            label_file_path.Visible = true;
            recordToolStripMenuItem.Click -= button_record_stop_recording;
            recordToolStripMenuItem.Click += recordToolStripMenuItem_Click;
            label_file_path.Click -= button_record_stop_recording;
            label_file_path.MouseDown += label_file_path_RightClick;
            label_result.Visible = false;
            string[] file_info = Program.get_file_info(filename);
            if (filename != null)
            {
                if (file_info[3] == "1")
                {
                    toolTip_file_info.SetToolTip(label_file_path, file_info[0] + " B, " + file_info[1] + " Hz, " + file_info[2] + " bit, mono");
                }
                else if (file_info[3] == "2")
                {
                    toolTip_file_info.SetToolTip(label_file_path, file_info[0] + " B, " + file_info[1] + " Hz, " + file_info[2] + " bit, stereo");
                }
                toolTip_file_info.Active = true;
            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void progress_bar_detecting_Click(object sender, EventArgs e)
        {

        }

        private void label_result_Click(object sender, EventArgs e)
        {
            if (detection_result != null)
            {
                Form2 f2 = new Form2();
                if (f2.ShowDialog() == DialogResult.OK)
                {
                    f2.Dispose();
                    f2.Close();
                }
            }
        }



        private void drawSpectrumGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawSpectrumGraphToolStripMenuItem.Checked)
            {
                Program.draw = false;
                drawSpectrumGraphToolStripMenuItem.Checked = false;
                System.Diagnostics.Debug.WriteLine("draw graph set to false");
            }
            else
            {
                Program.draw = true;
                drawSpectrumGraphToolStripMenuItem.Checked = true;
                System.Diagnostics.Debug.WriteLine("draw graph set to true");
            }
        }

        private void windowFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (real_time_analysis == false)
            {
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
                is_recording = true;
                waveIn = new WaveInEvent();
                writer?.Dispose();
                recorded_file_path = Path.Combine(outputFolder, DateTime.Now.ToString("MM_dd_yyyy_H_mm_ss") + ".wav");
                writer = new WaveFileWriter(recorded_file_path, waveIn.WaveFormat);
                waveIn.StartRecording();
                Console.WriteLine("Started recording");
                button_detect_pitch.Visible = false;
                toolTip_file_info.Active = false;
                label_result.Visible = false;
                label_file_path.Visible = true;
                tooltip_frequency.Active = false;
                label_file_path.Text = "Recording, click here to stop...";
                recordToolStripMenuItem.Text = "Stop recording";
                recordToolStripMenuItem.Click -= recordToolStripMenuItem_Click;
                recordToolStripMenuItem.Click += button_record_stop_recording;
                label_file_path.MouseDown -= label_file_path_RightClick;
                label_file_path.Click += button_record_stop_recording;
                waveIn.DataAvailable += (s, a) =>
                {
                    writer.Write(a.Buffer, 0, a.BytesRecorded);
                    if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 10)
                    {
                        waveIn.StopRecording();
                    }
                };
            }
            else
            {
                MessageBox.Show("Real time analysis in progress!");
            }
        }

        private void loadSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (is_recording == false)
            {
                if (real_time_analysis == false)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Wave File (*.wav)|*.wav";
                    if (Directory.Exists("Example sounds"))
                    {
                        ofd.InitialDirectory = "Example sounds";
                    }
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        label_result.Visible = false;
                        filename = ofd.FileName;
                        label_file_path.Text = filename;
                        label_file_path.Visible = true;
                        string[] file_info = Program.get_file_info(filename);
                        if (filename != null)
                        {
                            if (file_info[3] == "1")
                            {
                                toolTip_file_info.SetToolTip(label_file_path, file_info[0] + " B, " + file_info[1] + " Hz, " + file_info[2] + " bit, mono");
                            }
                            else if (file_info[3] == "2")
                            {
                                toolTip_file_info.SetToolTip(label_file_path, file_info[0] + " B, " + file_info[1] + " Hz, " + file_info[2] + " bit, stereo");
                            }
                            toolTip_file_info.Active = true;
                        }
                    };
                }
                else
                {
                    MessageBox.Show("Real time analysis in progress!");
                }
            }
            else
            {
                MessageBox.Show("Recording in progress!");
            }
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = true;
            hammingToolStripMenuItem.Checked = false;
            blackmanToolStripMenuItem.Checked = false;
            hanningToolStripMenuItem.Checked = false;
            cosineToolStripMenuItem.Checked = false;
            kaiserToolStripMenuItem.Checked = false;
            rectangularToolStripMenuItem.Checked = false;
            Program.set_window_function("none");
        }

        private void hammingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = true;
            blackmanToolStripMenuItem.Checked = false;
            hanningToolStripMenuItem.Checked = false;
            cosineToolStripMenuItem.Checked = false;
            kaiserToolStripMenuItem.Checked = false;
            rectangularToolStripMenuItem.Checked = false;
            Program.set_window_function("Hamming");
        }

        private void blackmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = false;
            blackmanToolStripMenuItem.Checked = true;
            hanningToolStripMenuItem.Checked = false;
            cosineToolStripMenuItem.Checked = false;
            kaiserToolStripMenuItem.Checked = false;
            rectangularToolStripMenuItem.Checked = false;
            Program.set_window_function("Blackman");
        }

        private void hanningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = false;
            blackmanToolStripMenuItem.Checked = false;
            hanningToolStripMenuItem.Checked = true;
            cosineToolStripMenuItem.Checked = false;
            kaiserToolStripMenuItem.Checked = false;
            rectangularToolStripMenuItem.Checked = false;
            Program.set_window_function("Hanning");
        }
        private void rectangularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = false;
            blackmanToolStripMenuItem.Checked = false;
            hanningToolStripMenuItem.Checked = false;
            cosineToolStripMenuItem.Checked = false;
            kaiserToolStripMenuItem.Checked = false;
            rectangularToolStripMenuItem.Checked = true;
            Program.set_window_function("Rectangular");
        }
        private void cosineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = false;
            blackmanToolStripMenuItem.Checked = false;
            hanningToolStripMenuItem.Checked = false;
            cosineToolStripMenuItem.Checked = true;
            kaiserToolStripMenuItem.Checked = false;
            rectangularToolStripMenuItem.Checked = false;
            Program.set_window_function("Cosine");
        }

        private void kaiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = false;
            blackmanToolStripMenuItem.Checked = false;
            hanningToolStripMenuItem.Checked = false;
            cosineToolStripMenuItem.Checked = false;
            kaiserToolStripMenuItem.Checked = true;
            rectangularToolStripMenuItem.Checked = false;
            Program.set_window_function("Kaiser");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Load wave sample or record your own. There is also option for real-time analysis.\n2. In case you want to draw spectrum graph, check 'Draw spectrum graph' in Preferences. This will however cause analysis to take longer.\n3. Click 'detect pitch'.\n4. After the note is shown, click on it to display details. Resulting table shows frequency peaks of each note.", "Help");

        }

        private void label_file_path_Click(object sender, EventArgs e)
        {
        }

        private void label_file_path_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = filename;
                player.Play();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (is_recording == false)
                {
                    if (real_time_analysis == false)
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Filter = "Wave File (*.wav)|*.wav";
                        if (Directory.Exists("Example sounds"))
                        {
                            ofd.InitialDirectory = "Example sounds";
                        }
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            label_result.Visible = false;
                            filename = ofd.FileName;
                            label_file_path.Text = filename;
                            label_file_path.Visible = true;
                            string[] file_info = Program.get_file_info(filename);
                            if (filename != null)
                            {
                                if (file_info[3] == "1")
                                {
                                    toolTip_file_info.SetToolTip(label_file_path, file_info[0] + " B, " + file_info[1] + " Hz, " + file_info[2] + " bit, mono");
                                }
                                else if (file_info[3] == "2")
                                {
                                    toolTip_file_info.SetToolTip(label_file_path, file_info[0] + " B, " + file_info[1] + " Hz, " + file_info[2] + " bit, stereo");
                                }
                                toolTip_file_info.Active = true;
                            }
                        };
                    }
                    else
                    {
                        MessageBox.Show("Real time analysis in progress!");
                    }
                }
                else
                {
                    MessageBox.Show("Recording in progress!");
                }
            }
        }




        private void magnitudeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            magnitudeToolStripMenuItem.Checked = true;
            powerToolStripMenuItem.Checked = false;
            Program.set_y_axis(0);
        }

        private void powerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            magnitudeToolStripMenuItem.Checked = false;
            powerToolStripMenuItem.Checked = true;
            Program.set_y_axis(1);
        }

        private void highPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.TextBox_high_pass.Text = high_pass.ToString();
            if (f.ShowDialog() == DialogResult.OK)
            {
                high_pass = Convert.ToInt32(f.TextBox_high_pass.Text);
                Program.set_high_pass(high_pass);
                f.Dispose();
            }
            else
            {
                f.Dispose();
            }

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void realTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (is_recording == false)
            {
                real_time_analysis = true;
                timer_real_time.Enabled = true;
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
                waveIn = new WaveInEvent();
                writer?.Dispose();
                recorded_file_path = Path.Combine(outputFolder, DateTime.Now.ToString("MM_dd_yyyy_H_mm_ss") + ".wav");
                writer = new WaveFileWriter(recorded_file_path, waveIn.WaveFormat);
                waveIn.StartRecording();
                Console.WriteLine("Started real time analysis");
                button_detect_pitch.Visible = false;
                toolTip_file_info.Active = false;
                tooltip_frequency.Active = true;
                label_file_path.Text = "Real time analysis, click here to stop...";
                label_result.Text = "";
                realTimeToolStripMenuItem.Text = "Stop real time";
                realTimeToolStripMenuItem.Click -= realTimeToolStripMenuItem_Click;
                realTimeToolStripMenuItem.Click += stop_real_time_Click;
                label_file_path.MouseDown -= label_file_path_RightClick;
                label_file_path.Click += stop_real_time_Click;
                waveIn.DataAvailable += (s, a) =>
                {
                    writer.Write(a.Buffer, 0, a.BytesRecorded);
                    if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 10)
                    {
                        waveIn.StopRecording();
                    }
                };
            }
            else
            {
                MessageBox.Show("Recording in progress!");
            }
        }
        public void stop_real_time_Click(object sender, EventArgs e)
        {
            timer_real_time.Enabled = false;
            timer_real_time.Stop();
            real_time_analysis = false;
            waveIn.StopRecording();
            writer?.Dispose();
            writer = null;
            waveIn.Dispose();
            temp_file_name = recorded_file_path;
            if (System.IO.File.Exists(temp_file_name))
            {
                System.IO.File.Delete(temp_file_name);
            }
            temp_file_name = null;
            filename = null;
            label_result.Text = "";
            realTimeToolStripMenuItem.Click -= stop_real_time_Click;
            realTimeToolStripMenuItem.Click += realTimeToolStripMenuItem_Click;
            label_file_path.Click -= stop_real_time_Click;
            label_file_path.MouseDown += label_file_path_RightClick;
            button_detect_pitch.Visible = true;
            realTimeToolStripMenuItem.Text = "Real time";
            tooltip_frequency.Active = false;
            label_file_path.Text = "Click here to select a file";
            Console.WriteLine("Stopped real time analyis");
            //toolTip_file_info.Active=true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            waveIn.StopRecording();
            writer?.Dispose();
            writer = null;
            waveIn.Dispose();
            temp_file_name = recorded_file_path;
            Program.get_file_info(temp_file_name);
            detection_result = Program.detect_pitch(temp_file_name);
            if (System.IO.File.Exists(temp_file_name))
            {
                System.IO.File.Delete(temp_file_name);
            }
            temp_file_name = null;
            if (detection_result != null)
            {
                label_result.Text = detection_result[0];
                tooltip_frequency.SetToolTip(label_result, detection_result[1] + " Hz");
            }
            label_result.Visible = true;
            waveIn = null;
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            waveIn = new WaveInEvent();
            writer?.Dispose();
            recorded_file_path = Path.Combine(outputFolder, DateTime.Now.ToString("MM_dd_yyyy_H_mm_ss") + ".wav");
            writer = new WaveFileWriter(recorded_file_path, waveIn.WaveFormat);
            waveIn.StartRecording();
            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 10)
                {
                    waveIn.StopRecording();
                }
            };
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
