using NAudio.Midi;
using PitchToMidi.SoundAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi.MIDI {
    public class MIDIPlayer {
        #region fields
        private MidiOut midi;

        private AudioFrequencyData audio;

        private static string defaultMidi = "LoopBe Internal MIDI";

        private int pitchBendRange = 12;

        private Note lastNote;

        private int channel = 1;
        #endregion

        public MIDIPlayer(int device = -1) {
            midi = MIDI.GetMidiDeviceByName(defaultMidi);
        }

        public void SetFrequencyData(AudioFrequencyData data) {
            this.audio = data;
        }

        public async void PlayAudioAsMidi() {

            long time = Timer.Time;

            Queue<NoteEvent> events = audio.AsQueue();

            while (events.Count > 0) {

                double passed = 0.001 * Timer.Passed(time);

                while (events.Count > 0 && passed >= audio.NoteToTime(events.Peek())) {
                    SendMIDI(events.Dequeue());
                }

                await Task.Delay(1);
            }

            Console.WriteLine("Done");
        }
        private void SendMIDI(NoteEvent e) {

            if (e.Type == NoteEventType.End && lastNote.MidiNote > 0) {
                Console.WriteLine("Ending note: " + e.Note);
                midi.Send(MidiMessage.StopNote(lastNote.MidiNote, 0, channel).RawData);
                SendPitchBend();
                lastNote = new Note(-1, 0, 0);
                return;
            }

            if (lastNote.MidiNote > 0) {
                SendPitchBend(PitchBendValue(lastNote, e.Note));
            } else {
                SendPitchBend(PitchBendValue(e.Note, e.Note));
                midi.Send(MidiMessage.StartNote(e.Note.MidiNote, 80, channel).RawData);

                lastNote = e.Note;
                Console.WriteLine("Sending note: " + e.Note);
            }
        }

        public void SendPitchBend(int value = 8192) {
            int message = PitchEventMessage(value);
            Console.WriteLine(message);
            midi.Send(message);
        }

        private int PitchEventMessage(int value) {
            byte lowValue = (byte)(value & 0x7F);
            byte highValue = (byte)(value >> 7);
            return new MidiMessage((int)MidiCommandCode.PitchWheelChange, lowValue, highValue).RawData;
        }

        private int PitchBendValue(Note fromNote, Note target) {

            Console.WriteLine("From Note: " + fromNote + ", target: " + target);
            Console.WriteLine("Target_ " + target.AccurateMidiNote);

            double noteDiff = target.AccurateMidiNote - fromNote.AccurateMidiNote;

            double pitchStep = 16384.0 / (2 * pitchBendRange);

            short middle = 8192;

            int val = (int)(middle + noteDiff * pitchStep);
            if (val < 0) {
                val = 0;
            } else if (val >= 16384) {
                val = 16384 - 1;
            }

            Console.WriteLine("Pitch val: " + val);
            return val;
        }
    }
}
