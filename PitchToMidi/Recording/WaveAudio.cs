using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    public class WaveAudio {

        #region fields
        private static string testFilePath = "P:/Stuff/Projects/PitchToMidi/PitchToMidi/Audio/c5.wav";

        private WaveFileReader reader;

        private double[] doubleBuffer;
        private double[] fftResult;
        #endregion

        public void Load() {
            reader = new WaveFileReader(testFilePath);
            Console.WriteLine("Wave format: " + reader.WaveFormat);

            
        }
        public void StartRead() {

            byte[] buffer = new byte[reader.Length];

            int read = reader.Read(buffer, 0, buffer.Length);

            doubleBuffer = new double[read / 2];
            for (int sampleIndex = 0; sampleIndex < read / 2; sampleIndex++) {
                var intSampleValue = BitConverter.ToInt16(buffer, sampleIndex * 2);
                doubleBuffer[sampleIndex] = intSampleValue / 32768.0;
            }

            Console.WriteLine("Read into double[] buffer: " + doubleBuffer.Length);
            double lengthInSeconds = 0.25 * buffer.Length / 44100;
            Console.WriteLine("File length: " + lengthInSeconds);
        }
        public void FFT() {
            fftResult = SoundAnalysis.FftAlgorithm.Calculate(doubleBuffer);

            Console.WriteLine("First val b: " + doubleBuffer[0]);
            Console.WriteLine("First val fft: " + fftResult[0]);

            //for (int i = fftResult.Length / 3; i < fftResult.Length; i++) {
            //    Console.WriteLine(fftResult[i]);
            //}
        }
    }
}
