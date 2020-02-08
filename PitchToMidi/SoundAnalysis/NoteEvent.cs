using PitchToMidi.SoundAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    public enum NoteEventType {
        None,
        Start,
        Hold,
        End
    }

    [Serializable]
    public struct NoteEvent {
        public NoteEventType Type;
        public int SampleStart;
        public double Frequency;
        public double Amplitude;
        public Note Note;

        public static NoteEvent EndEvent(int time = 0) {
            NoteEvent e = new NoteEvent();
            e.SampleStart = time;
            e.Type = NoteEventType.End;
            return e;
        }
    }
}
