using PitchToMidi.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitchToMidi {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());

            Console.SetBufferSize(Console.BufferWidth, 25000);

            SoundFile audio = new SoundFile(SoundFile.DebugFilePath("audioTest"));
            if (!audio.LoadFile()) {
                Console.WriteLine("Audio not loaded");
                Console.ReadLine();
                return;
            }

            audio.SetChannel(0);

            AudioFrequencyAnalyzer analyzer = new AudioFrequencyAnalyzer();
            analyzer.SetAudio(audio);
            analyzer.AnalyzeAudio();

            int startCount = 0;
            foreach (NoteEvent e in analyzer.FrequencyData.Events) {
                string newS = "Found note start: " + e.SampleStart + ", note: " + e.Note + ", freq " + e.Frequency;
                Console.WriteLine(newS);
                if (e.Type == NoteEventType.Start) {
                    startCount++;
                }
            }
            Console.WriteLine("Total notes: " + startCount);
            Console.ReadLine();
        }
    }
}
