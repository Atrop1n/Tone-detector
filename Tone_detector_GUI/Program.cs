using FftSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Tone_detector_GUI
{

    internal static class Program
    {
        static long sample_data_size;
        static short header_size;
        static int sampling_frequency;
        static short bits_per_sample;
        static short channels;
        static byte[] sample_raw;
        static double[] fft_power;
        static double[] fft_frequency;
        static double[] sample_data;
        static double[] sample_data_left;
        static double[] sample_data_right;
        static public bool draw = true;
        static int high_pass = 20;
        static Dictionary<string, double> notes = new Dictionary<string, double>()
        {{"C",16.35},{"C#",17.32},{"D",18.35},{"D#",19.45},{"E",20.60},{"F",21.83},{"F#",23.12},{"G",24.50},{"G#",25.96},{"A",27.50},{"A#",29.14},{"B",30.87}};
        static FftSharp.Window window = new FftSharp.Windows.Hanning();
        static byte y_axis = 0;

        //decides whether we are displaying magnitude or power
        public static void set_y_axis(byte y)
        {
            if (y == 0)
            {
                y_axis = 0;
            }
            else if (y == 1)
            {
                y_axis = 1;
            }
        }
        public static float hex_to_float(string hex)
        {
            uint num = uint.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] floatVals = BitConverter.GetBytes(num);
            float result = BitConverter.ToSingle(floatVals, 0);
            return result;
        }
        public static void set_window_function(string w)
        {
            switch (w)
            {
                case "Hanning":
                    {
                        window = new FftSharp.Windows.Hanning();
                        break;
                    }
                case "Hamming":
                    {
                        window = new FftSharp.Windows.Hamming();
                        break;
                    }
                case "Blackman":
                    {
                        window = new FftSharp.Windows.Blackman();
                        break;
                    }
                case "Rectangular":
                    {
                        window = new FftSharp.Windows.Rectangular();
                        break;
                    }
                case "Cosine":
                    {
                        window = new FftSharp.Windows.Cosine();
                        break;
                    }
                case "Kaiser":
                    {
                        window = new FftSharp.Windows.Kaiser();
                        break;
                    }
                default:
                    {
                        window = null;
                        break;
                    }
            }
        }
        public static void set_high_pass(int hp)
        {
            high_pass = hp;
        }


        //finds out which note is closest to a given frequency
        public static string find_pitch(double frequency)
        {
            double difference = 20000.0;
            string pitch = "nothing";
            foreach (KeyValuePair<string, double> note in notes)
            {
                double compared_value = note.Value;
                for (int i = 1; i <= 11; i++)
                {
                    if (Math.Abs(frequency - compared_value) < difference)
                    {
                        difference = Math.Abs(frequency - compared_value);
                        pitch = note.Key;
                    }
                    compared_value = compared_value * 2;
                }
            }
            return pitch;
        }
        //reads Wave file header
        public static string[] get_file_info(string sample_path)
        {
            sample_raw = File.ReadAllBytes(sample_path);
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(i + " " + sample_raw[i].ToString("X2"));
            }
            string sampling_frequency_hex = sample_raw[27].ToString("X2") + sample_raw[26].ToString("X2") + sample_raw[25].ToString("X2") + sample_raw[24].ToString("X2");
            sampling_frequency = Int32.Parse(sampling_frequency_hex, System.Globalization.NumberStyles.HexNumber);
            string bits_per_sample_hex = sample_raw[35].ToString("X2") + sample_raw[34].ToString("X2");
            bits_per_sample = Int16.Parse(bits_per_sample_hex, System.Globalization.NumberStyles.HexNumber);
            string channels_hex = sample_raw[23].ToString("X2") + sample_raw[22].ToString("X2");
            channels = Int16.Parse(channels_hex, System.Globalization.NumberStyles.HexNumber);
            string[] file_info = { sample_raw.Length.ToString(), sampling_frequency.ToString(), bits_per_sample.ToString(), channels.ToString() };
            if (sample_raw[16] == 16)
            {
                //44 byte header
                string sample_data_size_hex = sample_raw[43].ToString("X2") + sample_raw[42].ToString("X2") + sample_raw[41].ToString("X2") + sample_raw[40].ToString("X2");
                sample_data_size = Int32.Parse(sample_data_size_hex, System.Globalization.NumberStyles.HexNumber);
                int remainder = sample_raw.Length - Convert.ToInt32(sample_data_size);
                header_size = 44;
                Console.WriteLine("header size:" + header_size + ", total non-data bytes: " + remainder);
                return file_info;
            }
            else if (sample_raw[16] == 18)
            {
                //46 byte header
                string sample_data_size_hex = sample_raw[45].ToString("X2") + sample_raw[44].ToString("X2") + sample_raw[43].ToString("X2") + sample_raw[42].ToString("X2");
                sample_data_size = Int32.Parse(sample_data_size_hex, System.Globalization.NumberStyles.HexNumber);
                int remainder = sample_raw.Length - Convert.ToInt32(sample_data_size);
                header_size = 46;
                Console.WriteLine("header size:" + header_size + ", total non-data bytes: " + remainder);
                return file_info;
            }
            else
            {
                MessageBox.Show("Possibly corrupted file, header is neither 44 or 46 bytes long.");
                Application.Restart();
                return null;
            }

        }
        public static string[] detect_pitch(string sample_path)
        {
            if (channels == 1)
            {
                if (bits_per_sample == 16)
                {
                    sample_data = new double[sample_data_size / 2];
                    int index = 0;
                    int byte_reader = 0;
                    while (byte_reader < sample_data_size)
                    {
                        string sample_data_hex;
                        if (header_size == 44)
                        {
                            sample_data_hex = sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                        }
                        else
                        {
                            sample_data_hex = sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                        }
                        int sample_data_int = Int32.Parse(sample_data_hex, System.Globalization.NumberStyles.HexNumber);
                        sample_data[index] = Convert.ToDouble(sample_data_int);
                        byte_reader = byte_reader + 2;
                        index++;
                    }
                }
                else if (bits_per_sample == 24)
                {
                    sample_data = new double[sample_data_size / 3];
                    int index = 0;
                    int byte_reader = 0;
                    while (byte_reader < sample_data_size)
                    {
                        string sample_data_hex;
                        if (header_size == 44)
                        {
                            sample_data_hex = sample_raw[byte_reader + 46].ToString("X2") + sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                        }
                        else
                        {
                            sample_data_hex = sample_raw[byte_reader + 48].ToString("X2") + sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                        }
                        int sample_data_int = Int32.Parse(sample_data_hex, System.Globalization.NumberStyles.HexNumber);
                        sample_data[index] = Convert.ToDouble(sample_data_int);
                        byte_reader = byte_reader + 3;
                        index++;
                    }
                }
                else if (bits_per_sample == 32)
                {
                    sample_data = new double[sample_data_size / 4];
                    int index = 0;
                    int byte_reader = 0;
                    while (byte_reader < sample_data_size)
                    {
                        string sample_data_hex;
                        if (header_size == 44)
                        {
                            sample_data_hex = sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2") + sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                        }
                        else
                        {
                            sample_data_hex = sample_raw[byte_reader + 49].ToString("X2") + sample_raw[byte_reader + 48].ToString("X2") + sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                        }
                        float sample_data_float = hex_to_float(sample_data_hex);
                        sample_data[index] = Convert.ToDouble(sample_data_float);
                        byte_reader = byte_reader + 4;
                        index++;
                    }
                }

            }
            else if (channels == 2)
            {
                int index_left = 0;
                int index_right = 0;
                int byte_reader = 0;
                if (bits_per_sample == 16)
                {
                    sample_data_left = new double[sample_data_size / 4];
                    sample_data_right = new double[sample_data_size / 4];
                    while (byte_reader < sample_data_size)
                    {
                        if (byte_reader % 4 == 0)
                        {
                            //left channel
                            string sample_data_left_hex;
                            if (header_size == 44)
                            {
                                sample_data_left_hex = sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                            }
                            else
                            {
                                sample_data_left_hex = sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                            }
                            int sample_data_left_int = Int32.Parse(sample_data_left_hex, System.Globalization.NumberStyles.HexNumber);
                            sample_data_left[index_left] = Convert.ToDouble(sample_data_left_int);
                            index_left++;
                        }
                        else if (byte_reader % 4 == 2)
                        {
                            //right channel
                            string sample_data_right_hex;
                            if (header_size == 44)
                            {
                                sample_data_right_hex = sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                            }
                            else
                            {
                                sample_data_right_hex = sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                            }
                            int sample_data_right_int = Int32.Parse(sample_data_right_hex, System.Globalization.NumberStyles.HexNumber);
                            sample_data_right[index_right] = Convert.ToDouble(sample_data_right_int);
                            index_right++;
                        }
                        byte_reader = byte_reader + 2;
                    }
                }
                else if (bits_per_sample == 24)
                {
                    sample_data_left = new double[sample_data_size / 6];
                    sample_data_right = new double[sample_data_size / 6];
                    while (byte_reader < sample_data_size)
                    {
                        if (byte_reader % 6 == 0)
                        {
                            //left channel
                            string sample_data_left_hex;
                            if (header_size == 44)
                            {
                                sample_data_left_hex = sample_raw[byte_reader + 46].ToString("X2") + sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                            }
                            else
                            {
                                sample_data_left_hex = sample_raw[byte_reader + 48].ToString("X2") + sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                            }
                            int sample_data_left_int = Int32.Parse(sample_data_left_hex, System.Globalization.NumberStyles.HexNumber);
                            sample_data_left[index_left] = Convert.ToDouble(sample_data_left_int);
                            index_left++;
                        }
                        else if (byte_reader % 6 == 3)
                        {
                            //right channel
                            string sample_data_right_hex;
                            if (header_size == 44)
                            {
                                sample_data_right_hex = sample_raw[byte_reader + 46].ToString("X2") + sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                            }
                            else
                            {
                                sample_data_right_hex = sample_raw[byte_reader + 48].ToString("X2") + sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                            }
                            int sample_data_right_int = Int32.Parse(sample_data_right_hex, System.Globalization.NumberStyles.HexNumber);
                            sample_data_right[index_right] = Convert.ToDouble(sample_data_right_int);
                            index_right++;
                        }
                        byte_reader = byte_reader + 3;
                    }
                }
                else if (bits_per_sample == 32)
                {
                    sample_data_left = new double[sample_data_size / 8];
                    sample_data_right = new double[sample_data_size / 8];
                    while (byte_reader < sample_data_size)
                    {
                        if (byte_reader % 8 == 0)
                        {
                            //left channel
                            string sample_data_left_hex;
                            if (header_size == 44)
                            {
                                sample_data_left_hex = sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2") + sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                            }
                            else
                            {
                                sample_data_left_hex = sample_raw[byte_reader + 49].ToString("X2") + sample_raw[byte_reader + 48].ToString("X2") + sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                            }
                            float sample_data_left_float = hex_to_float(sample_data_left_hex);
                            sample_data_left[index_left] = Convert.ToDouble(sample_data_left_float);
                            index_left++;
                        }
                        else if (byte_reader % 8 == 4)
                        {
                            //right channel
                            string sample_data_right_hex;
                            if (header_size == 44)
                            {
                                sample_data_right_hex = sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2") + sample_raw[byte_reader + 45].ToString("X2") + sample_raw[byte_reader + 44].ToString("X2");
                            }
                            else
                            {
                                sample_data_right_hex = sample_raw[byte_reader + 49].ToString("X2") + sample_raw[byte_reader + 48].ToString("X2") + sample_raw[byte_reader + 47].ToString("X2") + sample_raw[byte_reader + 46].ToString("X2");
                            }
                            float sample_data_right_float = hex_to_float(sample_data_right_hex);
                            sample_data_left[index_right] = Convert.ToDouble(sample_data_right_float);
                            index_right++;
                        }
                        byte_reader = byte_reader + 4;
                    }
                }
                if (sample_data_left.Length == sample_data_right.Length)
                {
                    sample_data = new double[sample_data_left.Length];
                }
                else MessageBox.Show("Something went wrong, right and left channel are not the same length");
                //make average out of both channels
                for (int i = 0; i < sample_data.Length; i++)
                {
                    sample_data[i] = (sample_data_left[i] + sample_data_right[i]) / 2.0;
                }
            }
            else
            {
                MessageBox.Show("File must have maximum 2 channels");
            }
            if (window != null)
            {
                window.ApplyInPlace(sample_data);
                System.Diagnostics.Debug.WriteLine(window.ToString());
            }
            //apply zero padding
            System.Diagnostics.Debug.WriteLine("Before zero padding: " + sample_data.Length);
            sample_data = Pad.ZeroPad(sample_data);
            System.Diagnostics.Debug.WriteLine("After zero padding: " + sample_data.Length);

            //apply Fourier transform
            if (y_axis == 0)
            {
                fft_power = Transform.FFTmagnitude(sample_data);
            }
            else
            {
                fft_power = Transform.FFTpower(sample_data);
            }
            fft_frequency = Transform.FFTfreq(sampling_frequency, fft_power.Length);


            Dictionary<double, double> frequency = new Dictionary<double, double>();
            if (Form1.real_time_analysis == false)
            {
                Form1.progress_bar_detecting.Visible = true;
                Form1.progress_bar_detecting.Value = 0;
                Form1.progress_bar_detecting.Minimum = 0;
                Form1.progress_bar_detecting.Maximum = fft_frequency.Length;
            }
            for (int i = 0; i < fft_frequency.Length; i++)
            {
                if (Form1.real_time_analysis == false)
                {
                    Form1.progress_bar_detecting.Value++;
                }
                //ignore frequencies lower than filter cutoff
                if (fft_frequency[i] > high_pass && fft_frequency[i] < sampling_frequency / 2)
                {
                    frequency.Add(fft_frequency[i], fft_power[i]);             
                }
            }
            if (frequency.Count == 0)
            {
                frequency.Add(0, 0);
            }
            var frequencies_sorted = from entry in frequency orderby entry.Value descending select entry;
            if (draw == true)
            {
                //delete old spectrum image
                File.Delete("periodogram.png");
                ScottPlot.Plot plt = new ScottPlot.Plot();
                plt.AddScatterLines(fft_frequency, fft_power);
                if (y_axis == 0)
                {
                    plt.YLabel("Magnitude (V/Hz)");
                }
                else
                {
                    plt.YLabel("Power (dB)");
                }
                plt.XLabel("Frequency (Hz)");
                plt.Margins(0);
                //save new spectrum image
                plt.SaveFig("periodogram.png");
            }
            Dictionary<string, double> main_peaks = new Dictionary<string, double>();
            int count = 1;
            List<double> amplitudes = new List<double>();
            //find peak for each note
            foreach (KeyValuePair<double, double> kvp in frequencies_sorted)
            {
                if (count == 12)
                {
                    break;
                }
                var this_note = find_pitch(kvp.Key);
                if (!main_peaks.ContainsKey(this_note))
                {
                    if (bits_per_sample != 32)
                    {
                        amplitudes.Add(Math.Round(kvp.Value, 3));
                    }
                    else
                    {
                        amplitudes.Add(Math.Round(kvp.Value, 8));
                    }
                    main_peaks.Add(this_note, kvp.Key);
                }
            }
            Form1.progress_bar_detecting.Visible = false;
            try
            {
                //combine all gathered info into an array
                string[] details = {
                //notes and their corresponding peaks, ordered from high to low
                main_peaks.ElementAt(0).Key.ToString(),
                Math.Round(main_peaks.ElementAt(0).Value,5).ToString(),
                amplitudes[0].ToString(),
                main_peaks.ElementAt(1).Key.ToString(),
                Math.Round(main_peaks.ElementAt(1).Value,5).ToString(),
                amplitudes[1].ToString(),
                main_peaks.ElementAt(2).Key.ToString(),
                Math.Round(main_peaks.ElementAt(2).Value,5).ToString(),
                amplitudes[2].ToString(),
                main_peaks.ElementAt(3).Key.ToString(),
                Math.Round(main_peaks.ElementAt(3).Value,5).ToString(),
                amplitudes[3].ToString(),
                main_peaks.ElementAt(4).Key.ToString(),
                Math.Round(main_peaks.ElementAt(4).Value,5).ToString(),
                amplitudes[4].ToString(),
                main_peaks.ElementAt(5).Key.ToString(),
                Math.Round(main_peaks.ElementAt(5).Value,5).ToString(),
                amplitudes[5].ToString(),
                main_peaks.ElementAt(6).Key.ToString(),
                Math.Round(main_peaks.ElementAt(6).Value,5).ToString(),
                amplitudes[6].ToString(),
                main_peaks.ElementAt(7).Key.ToString(),
                Math.Round(main_peaks.ElementAt(7).Value,5).ToString(),
                amplitudes[7].ToString(),
                main_peaks.ElementAt(8).Key.ToString(),
                Math.Round(main_peaks.ElementAt(8).Value,5).ToString(),
                amplitudes[8].ToString(),
                main_peaks.ElementAt(9).Key.ToString(),
                Math.Round(main_peaks.ElementAt(9).Value,5).ToString(),
                amplitudes[9].ToString(),
                main_peaks.ElementAt(10).Key.ToString(),
                Math.Round(main_peaks.ElementAt(10).Value,5).ToString(),
                amplitudes[10].ToString(),
                main_peaks.ElementAt(11).Key.ToString(),
                Math.Round(main_peaks.ElementAt(11).Value,5).ToString(),
                amplitudes[11].ToString(),
                //file info
                sample_raw.Length.ToString(),
                sampling_frequency.ToString(),
                bits_per_sample.ToString(),
                channels.ToString()
            };
                return details;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Form1.timer_real_time.Enabled = false;
                Form1.timer_real_time.Stop();
                MessageBox.Show("No values detected because filter cutoff frequency was higher than the maximum frequency of analyzed sound. \nLower the filter cutoff.");
                Application.Restart();
                return null;
            }



        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
