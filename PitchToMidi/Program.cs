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
            return;

            Console.SetBufferSize(Console.BufferWidth, 25000);

            SoundFile audio = new SoundFile(SoundFile.DebugFilePath("got"));
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
            Console.WriteLine("First: " + analyzer.FrequencyData.Events[0].Note);
            foreach (NoteEvent e in analyzer.FrequencyData.Events) {
                //string newS = "Found note start: " + e.SampleStart + ", note: " + e.Note + ", freq " + e.Frequency;
                //Console.WriteLine(newS);
                if (e.Type == NoteEventType.Start) {
                    startCount++;
                }
            }
            Console.WriteLine("Total notes: " + startCount);

            FrequencyChartForm form = new FrequencyChartForm();
            form.SetAudioFrequencyData(analyzer.FrequencyData);
            Application.Run(form);
        }
    }
}
