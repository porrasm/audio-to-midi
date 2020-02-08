using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi.SoundAnalysis {
    public class SpectrumAnalyzer {

        #region fields
        private double resolution;

        private double[] spectrum;
        private int samples;

        public double Frequency { get; private set; }
        public double Amplitude { get; private set; }

        public int PeakWidth { get; set; } = 1;
        //public float MinAmplitude { get; set; } = 0.05f;
        public double MinAmplitude { get; set; } = 0.05;
        #endregion

        public SpectrumAnalyzer(double resolution) {
            this.resolution = resolution;
        }

        public void SetSpectrum(double[] spectrum) {
            this.spectrum = spectrum;
            this.samples = spectrum.Length;
        }

        public bool FrequencyWasFound() {
            return Amplitude > 0 && Frequency > 0;
        }

        public void CalculateFrequency() {

            int greatesIndex = -1;
            double greatesAmplitude = MinAmplitude;

            for (int i = 0; i < samples; i++) {
                if (spectrum[i] > greatesAmplitude) {
                    greatesAmplitude = spectrum[i];
                    greatesIndex = i;
                }
            }

            if (greatesIndex == -1) {
                Amplitude = 0;
                Frequency = -1;
                return;
            }

            if (PeakWidth > 1) {
                throw new NotImplementedException();
            } else {
                Amplitude = spectrum[greatesIndex];
            }

            Frequency = ResolutionStepToFrequency(greatesIndex);
        }

        private double ResolutionStepToFrequency(int step) {
            return step * resolution;
        }
    }
}
