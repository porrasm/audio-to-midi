using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitchToMidi.GUI {
    public partial class SettingsForm : Form {

        private Settings backup;

        public SettingsForm() {
            InitializeComponent();

            sampleStepLabel.Text = "Sample step";
            minAmplitudeLabel.Text = "Min amplitude";

            SetCallbacks(sampleStepText, new EventHandler(SetSampleStep));
            SetCallbacks(minAmplitudeText, new EventHandler(SetMinAmplitude));

            okButton.Click += new EventHandler(OK);
            cancelButton.Click += new EventHandler(Cancel);

            backup = (Settings)Settings.Current.Clone();

            SetValues();
        }

        public void SetValues() {
            sampleStepText.Text = "" + Settings.Current.SampleStep;
            minAmplitudeText.Text = "" + Settings.Current.MinAmplitude;
        }

        public void SetSampleStep(object sender, EventArgs e) {
            int val = -1;
            int.TryParse(sampleStepText.Text, out val);
            if (!Settings.Current.SetSampleStep(val)) {
                sampleStepText.Text = "" + Settings.Current.SampleStep;
            }
        }

        public void SetMinAmplitude(object sender, EventArgs e) {
            double val = -1;
            double.TryParse(minAmplitudeText.Text, out val);
            if (!Settings.Current.SetMinAmplitude(val)) {
                minAmplitudeText.Text = "" + Settings.Current.MinAmplitude;
            }
        }

        public void SetCallbacks(TextBox text, EventHandler e) {
            text.LostFocus += e;
            text.KeyDown += new KeyEventHandler((sender, ke) => {
                if (ke.KeyCode == Keys.Return) {
                    e?.Invoke(sender, null);
                }
            });
        }

        public void OK(object sender, EventArgs e) {
            Close();
            Dispose();
        }
        public void Cancel(object sender, EventArgs e) {
            Settings.SetSettings(backup);
            Close();
            Dispose();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Settings.SetSettings(backup);
            Dispose();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e) {

        }
    }
}
