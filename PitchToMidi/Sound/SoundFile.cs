using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    public class SoundFile {

        #region fields
        public string Path { get; set; }

        public int Channels { get; private set; }
        public int SelectedChannel { get; private set; } = -1;
        public int SampleCount { get; private set; }
        public int ChannelSampleCount { get; private set; }
        public WaveFormat Format { get; private set; }

        private double[] rawData;

        public double[] ChannelSamples;
        #endregion

        public SoundFile(string path) {
            this.Path = path;
        }

        public double Length {
            get {
                return 1.0 * ChannelSampleCount / Format.SampleRate;
            }
        }

        public void SetChannel(int channel) {

            SelectedChannel = channel;

            ChannelSamples = new double[ChannelSampleCount];

            int sIndex = 0;
            for (int i = channel; i < SampleCount; i += Channels) {
                ChannelSamples[sIndex] = rawData[i];
                sIndex++;
            }
        }

        public bool LoadFile() {

            if (!File.Exists(Path)) {
                Console.WriteLine("File doesn't exist: " + Path);
                return false;
            }

            SampleCount = 0;

            try {
                Channels = GetChannelAmount();

                Console.WriteLine("Channels: " + Channels);

                WaveFileReader reader = GetStream();
                Format = reader.WaveFormat;
     
                byte[] buffer = new byte[reader.Length];

                int read = reader.Read(buffer, 0, buffer.Length);

                rawData = new double[read / 2];
                for (int sampleIndex = 0; sampleIndex < read / 2; sampleIndex++) {
                    var intSampleValue = BitConverter.ToInt16(buffer, sampleIndex * 2);
                    rawData[sampleIndex] = intSampleValue / 32768.0;
                }

                SampleCount = rawData.Length;
                ChannelSampleCount = SampleCount/ Channels;
                
                return true;

            } catch (Exception) {
                return false;
            }
        }

        private int GetChannelAmount() {
            byte[] wavInfo = new byte[23];
            using (BinaryReader reader = new BinaryReader(new FileStream(Path, FileMode.Open))) {
                reader.Read(wavInfo, 0, 23);
            }

            return wavInfo[22];
        }

        public double[] ReadSamples(int offset, int sampleCount, int channel) {

            double[] samples = new double[sampleCount];

            for (int i = 0; i < sampleCount; i++) {
                samples[i] = ChannelSamples[offset + i];
            }

            return samples;
        }

        public WaveFileReader GetStream() {
            if (!File.Exists(Path)) {
                Console.WriteLine("File doesn't exist: " + Path);
                return null;
            }
            return new WaveFileReader(Path);
        }

        public static string DebugFilePath(string filename) {
            return "P:/Stuff/Projects/PitchToMidi/PitchToMidi/Audio/" + filename + ".wav";
        }
    }
}
