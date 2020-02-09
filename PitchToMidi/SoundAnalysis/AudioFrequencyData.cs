using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi.SoundAnalysis {
    [Serializable]
    public class AudioFrequencyData {

        #region fields
        public List<NoteEvent> Events { get; set; }

        public int SampleCount { get; private set; }
        public int SampleStep { get; private set; }
        public int SampleRate { get; private set; }
        #endregion

        public AudioFrequencyData(int samples, int sampleStep, int sampleRate) {
            this.SampleCount = samples;
            this.SampleStep = sampleStep;
            this.SampleRate = sampleRate;
            Events = new List<NoteEvent>();
        }

        public int EventCount {
            get {
                return Events.Count;
            }
        }

        public void AddNote(NoteEvent noteEvent) {
            Events.Add(noteEvent);
        }
        public void EndNote(int time = 0) {
            if (Events.Count > 0) {
                if (Events[Events.Count - 1].Type != NoteEventType.End || time == SampleCount - 1) {
                    Events.Add(NoteEvent.EndEvent(time));
                }
            }
        }

        public double SampleIndexToTime(int sampleTime, int samplesOffset = 0) {
            return 1.0 * (sampleTime + samplesOffset) / SampleRate;
        }
        public double NoteToTime(int index, int samplesOffset = 0) {
            return 1.0 * (Events[index].SampleStart + samplesOffset) / SampleRate;
        }
        public double NoteToTime(NoteEvent note, int samplesOffset = 0) {
            return 1.0 * (note.SampleStart + samplesOffset) / SampleRate;
        }
    }

}
