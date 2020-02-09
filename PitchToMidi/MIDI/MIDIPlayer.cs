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

        private int lastNote;

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

            if (e.Type == NoteEventType.End && lastNote > 0) {
                Console.WriteLine("Ending note: " + e.Note);
                midi.Send(MidiMessage.StopNote(lastNote, 0, channel).RawData);
                lastNote = -1;
                return;
            }

            if (lastNote > 0) {
                SendPitchBend(PitchBendValue(lastNote, e.Note.AccurateMidiNote));
            } else {
                lastNote = e.Note.MidiNote;
                Console.WriteLine("Sending note: " + e.Note);
                SendPitchBend(PitchBendValue(lastNote, e.Note.AccurateMidiNote));
                midi.Send(MidiMessage.StartNote(lastNote, 80, channel).RawData);
            }
        }

        private void SendPitchBend(short value) {

            byte status = (byte)MidiCommandCode.PitchWheelChange;
            byte[] datas = BitConverter.GetBytes(value);

            Console.WriteLine("Sending pitch value: " + value);

            MidiMessage m = new MidiMessage(status, datas[0], datas[1]);
            midi.Send(m.RawData);
        }

        private short PitchBendValue(int note, double target) {

            double pitchStep = 1.0 * pitchBendRange / 16384;

            short value = 8192;

            return (short)(value + ((target - note) / pitchStep));
        }
    }
}
