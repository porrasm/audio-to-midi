using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi.SoundAnalysis {
    [Serializable]
    public struct Note {
        public int MidiNote;
        public double AccurateMidiNote;
        public double NoteFrequency;

        public Note(int midiNote, double freq, double accurateMidiNote) {
            this.MidiNote = midiNote;
            this.NoteFrequency = freq;
            this.AccurateMidiNote = accurateMidiNote;
        }

        public string PrettyNote() {
            int note = MidiNote % 12;
            int octave = MidiNote / 12 - 1;
            return NoteString(note) + octave;
        }

        public override string ToString() {
            return MidiNote + ": " + PrettyNote() + ": " + NoteFrequency;
        }

        public static string NoteToString(int note) {
            int n = note % 12;
            int octave = note / 12 - 1;
            return NoteString(n) + octave;
        }
        private static string NoteString(int note) {
            switch (note) {
                case -1:
                    return "-";
                case 0:
                    return "C";
                case 1:
                    return "C#";
                case 2:
                    return "D";
                case 3:
                    return "D#";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "F#";
                case 7:
                    return "G";
                case 8:
                    return "G#";
                case 9:
                    return "A";
                case 10:
                    return "A#";
                case 11:
                    return "B";
            }
            Console.WriteLine("NOTE WAS: " + note);
            throw new System.Exception("Invalid note");
        }
    }
}
