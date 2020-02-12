using PitchToMidi.SoundAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    [Serializable]
    public class Settings : ICloneable {

        #region fields
        public enum Category {
            Any,
            AudioAnalysis,
            AudioModify,
            Audio
        }

        public delegate void OnCategorySettingChanged();

        public static Settings Current { get; private set; }

        private static Dictionary<Category, OnCategorySettingChanged> callbacks;
        #endregion

        static Settings() {
            Current = new Settings();
            callbacks = new Dictionary<Category, OnCategorySettingChanged>();
        }

        #region settings functionality
        public static void SubscribeToSettingChange(Category c, OnCategorySettingChanged cb) {
            if (!callbacks.ContainsKey(c)) {
                callbacks.Add(c, cb);
            } else {
                callbacks[c] += cb;
            }
        }
        public static void UnsubscribeFromSettingChange(Category c, OnCategorySettingChanged cb) {
            if (callbacks.ContainsKey(c)) {
                callbacks[c] -= cb;
            }
        }

        public static void SettingsChanged(Category c) {
            if (c != Category.Any && callbacks.ContainsKey(Category.Any)) {
                callbacks[Category.Any]?.Invoke();
            }
            if (callbacks.ContainsKey(c)) {
                callbacks[c]?.Invoke();
            }
        }

        public static void SetSettings(Settings s) {
            Current = s;
            foreach (OnCategorySettingChanged cbDelegate in callbacks.Values) {
                cbDelegate?.Invoke();
            }
        }

        public object Clone() {
            Settings clone = new Settings();
            clone.SampleStep = (Setting<int>)this.SampleStep.Clone();
            clone.MinAmplitude = (Setting<double>)this.MinAmplitude.Clone();
            return clone;
        }
        #endregion


        #region analysis
        public Setting<double> TuneOffset { get; private set; } = new Setting<double>(
            0,
            Category.AudioAnalysis,
            "Tuning offset in Hz",
            (value) => {
                if (value <= -Frequencies.A4FREQ) {
                    return false;
                }
                return true;
            }
);

        public Setting<int> SampleStep { get; private set; } = new Setting<int>(
            1024,
            Category.AudioAnalysis,
            "Number of sampels are analyzed per step. Greater number means more accurate frequency but less accurate time. Has to be a power of 2",
            (value) => {
                if ((value & (value - 1)) != 0 || value < 2 || value > 16384) {
                    return false;
                }
                if (Current != null && value == Current.SampleStep.Value) {
                    return false;
                }
                return true;
            });

        public Setting<double> MinAmplitude { get; private set; } = new Setting<double>(
            0.05,
            Category.AudioAnalysis,
            "Minimum accepted amplitude. Lower value analyzes quieter sounds.",
            (value) => {
                if (value < 0) {
                    return false;
                }
                if (Current != null && value == Current.MinAmplitude.Value) {
                    return false;
                }
                return true;
            });
        #endregion

        #region modifying
        #endregion


    }

    [Serializable]
    public class Setting<T> : ICloneable where T : struct {

        public delegate bool SettingValidator(T newValue);

        #region fields
        public T Value { get; private set; }
        public Settings.Category Category { get; private set; }
        public string Description { get; private set; }
        public SettingValidator Validator { get; private set; }
        #endregion

        public Setting(T value, Settings.Category category, string description, SettingValidator validator = null) {
            if (!typeof(T).IsSerializable) {
                throw new Exception("A serializeable type is required");
            }

            this.Value = value;
            this.Category = category;
            this.Description = description;
            this.Validator = validator;
        }

        public bool SetValue(T newValue) {
            if (Validator != null && !Validator(newValue)) {
                return false;
            }
            this.Value = newValue;
            Settings.SettingsChanged(Category);
            return true;
        }

        public object Clone() {
            Setting<T> clone = new Setting<T>(Value, Category, Description, Validator);
            return clone;
        }
    }
}
