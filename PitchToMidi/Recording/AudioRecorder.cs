using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    // https://www.youtube.com/watch?v=q9cRZuosrOs
    public static class AudioRecorder {

        #region fields
        private static WaveInEvent audio;

        public static float Volume { get; private set; }

        private static BufferedWaveProvider waveBuffer;

        private static int RATE = 44100; // sample rate of the sound card
        private static int BUFFERSIZE = (int)Math.Pow(2, 11); // must be a multiple of 2
        #endregion

        public static void StartRecording() {
            audio = new WaveInEvent();
            audio.DataAvailable += new EventHandler<WaveInEventArgs>(OnAudioDataAvailable);
            waveBuffer = new BufferedWaveProvider(audio.WaveFormat);
            waveBuffer.BufferLength = BUFFERSIZE * 2;
            waveBuffer.DiscardOnBufferOverflow = true;

            audio.StartRecording();
        }
        public static void StopRecording() {
            audio.StopRecording();
            audio = null;
        }

        private static void OnAudioDataAvailable(object sender, WaveInEventArgs args) {
            waveBuffer.AddSamples(args.Buffer, waveBuffer.BufferedBytes, args.BytesRecorded);
            Volume = VolumeFromBuffer(args.Buffer, args.BytesRecorded);
            PlotData();
        }

        private static void PlotData() {
            // check the incoming microphone audio
            int frameSize = BUFFERSIZE;
            var audioBytes = new byte[frameSize];
            waveBuffer.Read(audioBytes, 0, frameSize);

            // return if there's nothing new to plot
            if (audioBytes.Length == 0)
                return;
            if (audioBytes[frameSize - 2] == 0)
                return;

            // incoming data is 16-bit (2 bytes per audio point)
            int BYTES_PER_POINT = 2;

            // create a (32-bit) int array ready to fill with the 16-bit data
            int graphPointCount = audioBytes.Length / BYTES_PER_POINT;

            // create double arrays to hold the data we will graph
            double[] pcm = new double[graphPointCount];
            double[] fft = new double[graphPointCount];
            double[] fftReal = new double[graphPointCount / 2];


        }

        private static float VolumeFromBuffer(byte[] bufferOld, int length) {
            byte[] buffer = new byte[1024];
            for (int i = 0; i < 1024; i++) {
                buffer[i] = bufferOld[i];
            }

            List<short> samples = new List<short>();

            float max = 0;
            // interpret as 16 bit audio
            for (int index = 0; index < length; index += 2) {
                short sample = (short)((buffer[index + 1] << 8) |
                                        buffer[index + 0]);

                samples.Add(sample);

                // to floating point
                var sample32 = sample / 32768f;

                // absolute value 
                if (sample32 < 0) sample32 = -sample32;
                // is this the max value?
                if (sample32 > max) max = sample32;
            }

            try {
                double freq = TestThing.GetFrequency(samples.ToArray());
                Console.WriteLine("Received freq: " + freq);
            } catch (Exception) {
                Console.WriteLine("Failed to find freq");
            }

            return max;
        }

        public delegate void Callback(float average);
        public static async Task<double> AverageVolume(int milliseconds, Callback cb = null) {

            audio = new WaveInEvent();

            double volumes = 0;
            int timesRecorded = 0;

            void VolumeCounter(object sender, WaveInEventArgs args) {
                volumes += VolumeFromBuffer(args.Buffer, args.BytesRecorded);
                timesRecorded++;
            }

            audio.DataAvailable += new EventHandler<WaveInEventArgs>(VolumeCounter);
            audio.StartRecording();

            await Task.Delay(milliseconds);

            audio.StopRecording();
            audio = null;

            double average = volumes / timesRecorded;

            cb?.Invoke((float)average);
            return (float)average;
        }
    }

}
