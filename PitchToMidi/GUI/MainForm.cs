using NAudio.Wave;
using PitchToMidi.MIDI;
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
        private MIDIPlayer midiPlayer;

        private WaveOut waveOut;

        private SettingsForm settings;
        #endregion

        public MainForm() {
            InitializeComponent();

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "wav files (*.wav)|*.wav|All files (*.*)|*.*";

            analyzer = new AudioFrequencyAnalyzer();
            midiPlayer = new MIDIPlayer();

            loadFileButton.Click += new EventHandler((sender, e) => LoadFile());
            analyzeAudioButton.Click += new EventHandler((sender, e) => AnalyzeFrequencyData());
            playSoundButton.Click += new EventHandler((sender, e) => PlayAudio());
            generateAudioButton.Click += new EventHandler((sender, e) => GenerateAudio());
            playMidiButton.Click += new EventHandler((sender, e) => PlayAsMidi());

            settingsButton.Click += new EventHandler(OpenSettings);

            Settings.OnAnalysisSettingsChange = AnalyzeFrequencyData;

            //Testing();
        }

        private void Testing() {
            loadedSoundFile = new SoundFile(SoundFile.DebugFilePath("audioTest"));
            loadedSoundFile.LoadFile();
            AnalyzeFrequencyData();
        }

        public void OpenSettings(object sender, EventArgs e) {
            if (settings != null) {
                settings.Close();
                settings.Dispose();
            }
            settings = new SettingsForm();
            settings.Show();
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

        public void PlayAudio() {
            if (loadedSoundFile == null || loadedSoundFile.SampleCount == 0) {
                return;
            }

            if (waveOut != null) {
                if (waveOut.PlaybackState == PlaybackState.Playing) {
                    waveOut.Stop();
                    return;
                }
            }

            waveOut = null;
            waveOut = new WaveOut();
            waveOut.Init(loadedSoundFile.GetStream());

            waveOut.Play();
        }
        #endregion

        #region analyze tab
        public void GenerateAudio() {
            if (waveOut != null) {
                if (waveOut.PlaybackState == PlaybackState.Playing) {
                    waveOut.Stop();
                    return;
                }
            }

            if (analyzer.FrequencyData == null || analyzer.FrequencyData.EventCount == 0) {
                return;
            }

            SoundFile generated = SoundFile.GenerateFromFrequencyData(analyzer.FrequencyData);

            waveOut = null;
            waveOut = new WaveOut();
            waveOut.Init(generated.GetStream());

            waveOut.Play();
        }

        public void PlayAsMidi() {

            if (analyzer.FrequencyData == null || analyzer.FrequencyData.EventCount == 0) {
                return;
            }

            midiPlayer.SetFrequencyData(analyzer.FrequencyData);
            midiPlayer.PlayAudioAsMidi();


        }
        #endregion
    }
}
