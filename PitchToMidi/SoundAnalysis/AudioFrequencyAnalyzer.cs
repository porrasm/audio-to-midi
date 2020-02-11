using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using PitchToMidi.SoundAnalysis;

namespace PitchToMidi {
    public class AudioFrequencyAnalyzer {

        #region fields
        public SoundFile AudioFile { get; private set; }

        public AudioFrequencyData FrequencyData { get; private set; }
        #endregion

        public void SetAudio(SoundFile audioFile) {
            this.AudioFile = audioFile;
        }

        public void AnalyzeAudio() {

            if (this.AudioFile == null || this.AudioFile.ChannelSampleCount == 0) {
                return;
            }

            int SampleStep = Settings.Current.SampleStep;

            Console.WriteLine("To analyze length: " + AudioFile.ChannelSampleCount);
            Console.WriteLine("To analyze time: " + AudioFile.Length);

            SpectrumAnalyzer analyzer = new SpectrumAnalyzer(GetResolution());
            FrequencyData = new AudioFrequencyData(AudioFile.ChannelSampleCount, SampleStep, AudioFile.Format.SampleRate);


            for (int sampleOffset = 0; sampleOffset + SampleStep < AudioFile.ChannelSampleCount; sampleOffset += SampleStep) {

                double[] buffer = AudioFile.ReadSamples(sampleOffset, SampleStep, 0);

                //Console.WriteLine("Buffer:" + buffer[0] + "Offset: " + sampleOffset + ", step: " + SampleStep + ", sampleCOunt: " + AudioFile.ChannelSampleCount);

                //for (int i = 0; i < buffer.Length; i++) {
                //    Console.WriteLine("Buffer " + i + ": " + buffer[i]);
                //}

                double[] spectrum = FFT(buffer);

                analyzer.SetSpectrum(spectrum);
                analyzer.CalculateFrequency();

                //for (int i = 0; i < spectrum.Length; i++) {
                //    Console.WriteLine("Spectrum " + i + ": " + spectrum[i]);
                //}

                if (!analyzer.FrequencyWasFound()) {
                    FrequencyData.EndNote(sampleOffset);
                    //Console.WriteLine("Not found: first buf: " + buffer[0]);
                    continue;
                }

                NoteEventType type = FrequencyData.EventCount == 0 ? NoteEventType.None : FrequencyData.Events[FrequencyData.EventCount - 1].Type;

                if (type == NoteEventType.Start || type == NoteEventType.Hold) {
                    type = NoteEventType.Hold;
                } else {
                    type = NoteEventType.Start;
                }

                NoteEvent noteEvent = new NoteEvent();

                noteEvent.Type = type;
                noteEvent.SampleStart = sampleOffset;
                noteEvent.Frequency = analyzer.Frequency;
                noteEvent.Amplitude = analyzer.Amplitude;
                noteEvent.Note = Frequencies.FrequencyToNote(analyzer.Frequency);

                FrequencyData.AddNote(noteEvent);
            }

            FrequencyData.EndNote(AudioFile.ChannelSampleCount - 1);

            Console.WriteLine("Processing complete, event count: " + FrequencyData.EventCount);

        }

        private double[] FFT(double[] data) {
            double[] spectrum = new double[data.Length];
            Complex[] dataComplex = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++) {
                dataComplex[i] = new Complex(data[i], 0.0);
            }
            Accord.Math.FourierTransform.FFT(dataComplex, Accord.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < data.Length; i++) {
                spectrum[i] = dataComplex[i].Magnitude;
            }
            return spectrum;
        }

        public double GetResolution() {
            return 1.0 * AudioFile.Format.SampleRate / Settings.Current.SampleStep;
        }
    }
}
