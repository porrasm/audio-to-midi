using PitchToMidi.SoundAnalysis;
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
    public partial class FrequencyChartForm : Form {

        #region fields
        AudioFrequencyData audioData;
        #endregion

        public FrequencyChartForm() {
            InitializeComponent();
        }

        public void SetAudioFrequencyData(AudioFrequencyData data) {
            this.audioData = data;
            frequencyChart.InitializeChart();
            frequencyChart.Clear();
            frequencyChart.AddFrequencyData(data, "Frequency data");
        }
    }
}
