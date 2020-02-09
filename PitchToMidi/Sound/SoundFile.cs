using NAudio.Wave;
using PitchToMidi.SoundAnalysis;
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

        public SoundFileType Type { get; private set; }

        public enum SoundFileType {
            File,
            Generated
        }
        #endregion

        public SoundFile(string path) {
            this.Path = path;
            Type = SoundFileType.File;
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

                WaveFileReader reader = GetFileReader();
                Format = reader.WaveFormat;

                byte[] buffer = new byte[reader.Length];

                int read = reader.Read(buffer, 0, buffer.Length);

                rawData = new double[read / 2];
                for (int sampleIndex = 0; sampleIndex < read / 2; sampleIndex++) {
                    var intSampleValue = BitConverter.ToInt16(buffer, sampleIndex * 2);
                    rawData[sampleIndex] = intSampleValue / 32768.0;
                }

                SampleCount = rawData.Length;
                ChannelSampleCount = SampleCount / Channels;

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

        public WaveStream GetStream() {
            if (Type == SoundFileType.File) {
                return GetFileReader();
            }
            if (Type == SoundFileType.Generated) {
                return GetRawStream();
            }
            return null;
        }
        private WaveFileReader GetFileReader() {
            if (!File.Exists(Path)) {
                Console.WriteLine("File doesn't exist: " + Path);
                return null;
            }
            return new WaveFileReader(Path);
        }
        private RawSourceWaveStream GetRawStream() {

            byte[] bytes = new byte[rawData.Length * 2];
            for (int i = 0; i < rawData.Length; i++) {
                short sampleValue = (short)(rawData[i] * 32768.0);
                byte[] values = BitConverter.GetBytes(sampleValue);
                bytes[i * 2] = values[0];
                bytes[i * 2 + 1] = values[1];
            }

            return new RawSourceWaveStream(bytes, 0, bytes.Length, Format);
        }

        public static string DebugFilePath(string filename) {
            return "P:/Stuff/Projects/PitchToMidi/PitchToMidi/Audio/" + filename + ".wav";

        }

        public static SoundFile GenerateFromFrequencyData(AudioFrequencyData audio) {

            double[] samples = new double[audio.SampleCount];

            List<SampleIndexFrequency> indexFreqs = new List<SampleIndexFrequency>();

            foreach (NoteEvent e in audio.Events) {

                if (e.Type == NoteEventType.End) {
                    continue;
                }

                Console.WriteLine("Adding from " + e.SampleStart + " with freq: " + e.Frequency);

                for (int i = 0; i < audio.SampleStep; i++) {

                    SampleIndexFrequency iFreq = new SampleIndexFrequency();
                    iFreq.SampleIndex = e.SampleStart + i;
                    iFreq.Frequency = e.Frequency;

                    indexFreqs.Add(iFreq);
                }
            }

            foreach (SampleIndexFrequency iFreq in indexFreqs) {
                int i = iFreq.SampleIndex;
                float frequency = (float)iFreq.Frequency;
                if (i >= audio.SampleCount) {
                    break;
                }
                samples[i] = Math.Sin(Math.PI * 2 * i * frequency / (audio.SampleRate));
            }

            SoundFile sound = new SoundFile(null);
            sound.Type = SoundFileType.Generated;

            sound.Channels = 1;
            sound.ChannelSampleCount = audio.SampleCount;
            sound.SampleCount = audio.SampleCount;
            sound.rawData = samples;
            sound.SetChannel(0);
            sound.Format = new WaveFormat(audio.SampleRate, 16, 1);

            return sound;
        }
        private struct SampleIndexFrequency {
            public int SampleIndex;
            public double Frequency;
        }
    }
}
