﻿using PitchToMidi.SoundAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PitchToMidi.GUI {
    public partial class MainForm : Form {

        #region fields
        AudioFrequencyData audioData;
        #endregion

        public MainForm() {
            InitializeComponent();
        }

        public void SetAudioFrequencyData(AudioFrequencyData data) {
            this.audioData = data;
        }
    }
}
