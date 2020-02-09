using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PitchToMidi.GUI {
    public partial class MainForm : Form {

        #region fields
        private SoundFile loadedSoundFile;
        private AudioFrequencyAnalyzer analyzer;
        #endregion

        public MainForm() {
            InitializeComponent();

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "wav files (*.wav)|*.wav|All files (*.*)|*.*";

            analyzer = new AudioFrequencyAnalyzer();

            loadFileButton.Click += new EventHandler((sender, e) => LoadFile());
            analyzeAudioButton.Click += new EventHandler((sender, e) => AnalyzeFrequencyData());
        }

        #region audio tab
        

        public void LoadFile() {

            if (openFileDialog.ShowDialog() == DialogResult.OK) {

                string file = openFileDialog.FileName;
                loadedSoundFile = new SoundFile(file);

                if (loadedSoundFile.LoadFile()) {
                    fileNameLabel.Text = "Loaded file: " + file;
                } else {
                    fileNameLabel.Text = "Loaded file: Error loading file";
                    return;
                }

                waveViewer1.WaveStream = loadedSoundFile.GetStream();
            }
        }

        public void AnalyzeFrequencyData() {
            if (loadedSoundFile == null || loadedSoundFile.SampleCount == 0) {
                return;
            }

            if (loadedSoundFile.SelectedChannel == -1) {
                loadedSoundFile.SetChannel(0);
            }

            analyzer.SetAudio(loadedSoundFile);
            analyzer.AnalyzeAudio();

            frequencyChart.InitializeChart();
            frequencyChart.AddFrequencyData(analyzer.FrequencyData, "Frequency data");

            mainTabs.SelectedTab = analyzeTab;
        }
        #endregion

        #region analyze tab
        #endregion
    }
}
