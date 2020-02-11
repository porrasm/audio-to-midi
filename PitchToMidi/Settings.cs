using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    [Serializable]
    public class Settings : ICloneable {

        public delegate void OnSettingsChange();

        public static Settings Current { get; private set; }

        public static OnSettingsChange OnAnalysisSettingsChange { get; set; }
        public static OnSettingsChange OnModifySettingsChange { get; set; }

        static Settings() {
            Current = new Settings();
        }

        #region analysis
        private int sampleStep = 1024;
        public int SampleStep { get => sampleStep; }
        public bool SetSampleStep(int value) {
            if ((value & (value - 1)) != 0 || value < 2 || value > 16384) {
                return false;
            }
            if (value == sampleStep) {
                return false;
            }
            sampleStep = value;
            AnalysisCallback();
            return false;
        }

        private double minAmplitude = 0.05;
        public double MinAmplitude {
            get {
                return minAmplitude;
            }
        }
        public bool SetMinAmplitude(double value) {
            if (value < 0) {
                return false;
            }
            if (minAmplitude == value) {
                return false;
            }
            this.minAmplitude = value;
            AnalysisCallback();
            return true;
        }

        private void AnalysisCallback() {
            OnAnalysisSettingsChange?.Invoke();
        }
        #endregion

        #region modifying
        #endregion

        public static void SetSettings(Settings s) {
            Current = s;
            OnAnalysisSettingsChange?.Invoke();
            OnModifySettingsChange?.Invoke();
        }

        public object Clone() {
            Settings clone = new Settings();
            clone.sampleStep = sampleStep;
            clone.minAmplitude = minAmplitude;
            return clone;
        }
    }
}
