using PitchToMidi.SoundAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    public class TestThing {

        static int SampleRate = 44100;
        static int MinFreq = 60;
        static int MaxFreq = 1300;

        public static double GetFrequency(short[] samples) {
            double[] x = new double[samples.Length];
            for (int i = 0; i < x.Length; i++) {
                x[i] = samples[i] / 32768.0;
            }

            double freq = FindFundamentalFrequency(x, SampleRate, MinFreq, MaxFreq);
            return freq;
        }

        public static double FindFundamentalFrequency(double[] x, int sampleRate, double minFreq, double maxFreq) {
            double[] spectr = FftAlgorithm.Calculate(x);

            int usefullMinSpectr = Math.Max(0,
                (int)(minFreq * spectr.Length / sampleRate));
            int usefullMaxSpectr = Math.Min(spectr.Length,
                (int)(maxFreq * spectr.Length / sampleRate) + 1);

            // find peaks in the FFT frequency bins 
            const int PeaksCount = 5;
            int[] peakIndices;
            peakIndices = FindPeaks(spectr, usefullMinSpectr, usefullMaxSpectr - usefullMinSpectr,
                PeaksCount);

            if (Array.IndexOf(peakIndices, usefullMinSpectr) >= 0) {
                // lowest usefull frequency bin shows active
                // looks like is no detectable sound, return 0
                return 0;
            }

            // select fragment to check peak values: data offset
            const int verifyFragmentOffset = 0;
            // ... and half length of data
            int verifyFragmentLength = (int)(sampleRate / minFreq);

            // trying all peaks to find one with smaller difference value
            double minPeakValue = Double.PositiveInfinity;
            int minPeakIndex = 0;
            int minOptimalInterval = 0;
            for (int i = 0; i < peakIndices.Length; i++) {
                int index = peakIndices[i];
                int binIntervalStart = spectr.Length / (index + 1), binIntervalEnd = spectr.Length / index;
                int interval;
                double peakValue;
                // scan bins frequencies/intervals
                ScanSignalIntervals(x, verifyFragmentOffset, verifyFragmentLength,
                    binIntervalStart, binIntervalEnd, out interval, out peakValue);

                if (peakValue < minPeakValue) {
                    minPeakValue = peakValue;
                    minPeakIndex = index;
                    minOptimalInterval = interval;
                }
            }

            return (double)sampleRate / minOptimalInterval;
        }
        private static int[] FindPeaks(double[] values, int index, int length, int peaksCount) {
            double[] peakValues = new double[peaksCount];
            int[] peakIndices = new int[peaksCount];

            for (int i = 0; i < peaksCount; i++) {
                peakValues[i] = values[peakIndices[i] = i + index];
            }

            // find min peaked value
            double minStoredPeak = peakValues[0];
            int minIndex = 0;
            for (int i = 1; i < peaksCount; i++) {
                if (minStoredPeak > peakValues[i]) minStoredPeak = peakValues[minIndex = i];
            }

            for (int i = peaksCount; i < length; i++) {
                if (minStoredPeak < values[i + index]) {
                    // replace the min peaked value with bigger one
                    peakValues[minIndex] = values[peakIndices[minIndex] = i + index];

                    // and find min peaked value again
                    minStoredPeak = peakValues[minIndex = 0];
                    for (int j = 1; j < peaksCount; j++) {
                        if (minStoredPeak > peakValues[j]) minStoredPeak = peakValues[minIndex = j];
                    }
                }
            }

            return peakIndices;
        }

        private static void ScanSignalIntervals(double[] x, int index, int length,
            int intervalMin, int intervalMax, out int optimalInterval, out double optimalValue) {
            optimalValue = Double.PositiveInfinity;
            optimalInterval = 0;

            // distance between min and max range value can be big
            // limiting it to the fixed value
            const int MaxAmountOfSteps = 30;
            int steps = intervalMax - intervalMin;
            if (steps > MaxAmountOfSteps)
                steps = MaxAmountOfSteps;
            else if (steps <= 0)
                steps = 1;

            // trying all intervals in the range to find one with
            // smaller difference in signal waves
            for (int i = 0; i < steps; i++) {
                int interval = intervalMin + (intervalMax - intervalMin) * i / steps;

                double sum = 0;
                for (int j = 0; j < length; j++) {
                    double diff = x[index + j] - x[index + j + interval];
                    sum += diff * diff;
                }
                if (optimalValue > sum) {
                    optimalValue = sum;
                    optimalInterval = interval;
                }
            }
        }
    }
}
