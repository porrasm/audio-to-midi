using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitchToMidi.GUI {
    public class SettingFormGenerator {

        #region fields
        public SettingsForm Form { get; private set; }
        private Dictionary<Settings.Category, List<SettingType>> settings = new Dictionary<Settings.Category, List<SettingType>>();
        #endregion

        #region static methods
        public static SettingFormGenerator CreateSettingsForm() {

            SettingFormGenerator generator = new SettingFormGenerator();

            Type settingsType = typeof(Settings);

            PropertyInfo[] properties = settingsType.GetProperties();
            foreach (PropertyInfo property in properties) {
                SettingType s = GetSetting(property);
                if (s.Type == AllowedType.None) {
                    continue;
                }
                generator.AddCategory(s.Category);
                generator.settings[s.Category].Add(s);
            }

            return null;
        }

        private static SettingType GetSetting(PropertyInfo property) {
            if (property.PropertyType == typeof(Setting<int>)) {

                Setting<int> intSetting = (Setting<int>)property.GetValue(Settings.Current);

                SettingType s = new SettingType();
                s.Type = AllowedType.Integer;
                s.Setting = intSetting;
                s.Category = intSetting.Category;

                return s;
            } else if (property.PropertyType == typeof(Setting<double>)) {

                Setting<double> doubleSetting = (Setting<double>)property.GetValue(Settings.Current);

                SettingType s = new SettingType();
                s.Type = AllowedType.Double;
                s.Setting = doubleSetting;
                s.Category = doubleSetting.Category;

                return s;
            }

            return new SettingType();
        }
        #endregion

        private void AddCategory(Settings.Category categoryToAdd) {
            if (!settings.ContainsKey(categoryToAdd)) {
                settings.Add(categoryToAdd, new List<SettingType>());
            }
        }

        private void CreateForm() {

            Form = new SettingsForm();

        }


        private enum AllowedType {
            None = 0,
            Integer,
            Double
        }
        private struct SettingType {
            public AllowedType Type;
            public Settings.Category Category;
            public object Setting;
        }
    }
}
