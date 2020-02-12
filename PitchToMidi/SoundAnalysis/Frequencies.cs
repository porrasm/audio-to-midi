using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi.SoundAnalysis {
    public static class Frequencies {

        #region fields
        private const int A4NOTE = 69;
        public const int A4FREQ = 440;
        private const double alpha = 1.059463094359;

        public static double[] NoteFrequencies { get; private set; }
        public static Note[] Notes { get; private set; }
        #endregion

        static Frequencies() {
            CalculateFrequencies();
        }

        public static void Initliaze() {
            Settings.SubscribeToSettingChange(Settings.Category.AudioAnalysis, CalculateFrequencies);
        }

        public static void CalculateFrequencies() {

            NoteFrequencies = new double[128];
            Notes = new Note[128];

            double a = Math.Pow(2, (1.0 / 12.0));

            for (int midiNote = 0; midiNote < 128; midiNote++) {
                int n = midiNote - A4NOTE;

                double freq = NoteToFrequency(n);

                Note note = new Note(midiNote, freq, midiNote);
                NoteFrequencies[midiNote] = (float)freq;
                Notes[midiNote] = note;
                //Debug.Log("Note: " + note);
            }
        }

        public static Note FrequencyToNote(double frequency) {

            int index = 0;
            double dist = double.MaxValue;

            for (int i = 0; i < 128; i++) {
                double newDist = Math.Abs(NoteFrequencies[i] - frequency);
                if (newDist < dist) {
                    dist = newDist;
                    index = i;
                } else {
                    break;
                }
            }

            dist = NoteFrequencies[index] - frequency;

            Note note = Notes[index];
            note.AccurateMidiNote = NoteAccurateValue(note, index, frequency);

            return note;
        }

        private static double NoteAccurateValue(Note note, int index, double frequency) {

            if (index == 0 || index == 127) {
                return 0;
            }

            double distance = note.NoteFrequency - frequency;

            if (distance > 0) {
                Note lower = Notes[index - 1];

                double offset = frequency - lower.NoteFrequency;
                double factor = offset / (note.NoteFrequency - lower.NoteFrequency);
                return lower.MidiNote + factor;
            } else if (distance < 0) {
                Note higher = Notes[index + 1];

                double offset = frequency - note.NoteFrequency;
                double factor = offset / (higher.NoteFrequency - note.NoteFrequency);
                return note.MidiNote + factor;
            }
            return 0;
        }

        public static Note FrequncyToNoteGood(float frequency) {

            int i = 64;
            int indexChange = 32;

            double dist = Math.Abs(NoteFrequencies[i] - frequency);

            while (true) {

                if (i == 0 || i == 127) {
                    return Notes[i];
                }

                double leftDist = Math.Abs(NoteFrequencies[i - 1] - frequency);
                double rightDist = Math.Abs(NoteFrequencies[i + 1] - frequency);

                if (leftDist < dist) {
                    if (indexChange == 1) {
                        return Notes[i - 1];
                    }
                    i -= indexChange;
                    indexChange /= 2;
                } else if (rightDist < dist) {
                    if (indexChange == 1) {
                        return Notes[i + 1];
                    }
                    i += indexChange;
                    indexChange /= 2;
                } else {
                    return Notes[i];
                }
            }
        }

        #region helpers
        public static double NoteToFrequency(int n) {
            return (A4FREQ + Settings.Current.TuneOffset.Value) * Math.Pow(alpha, n);
        }
        #endregion
    }
}
