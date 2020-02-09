using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitchToMidi.GUI {
    public class KeyState {

        private HashSet<Keys> keyStates;

        public KeyState() {
            keyStates = new HashSet<Keys>();
        }

        public void KeyEvent(Keys key, bool state) {
           if (state) {
                keyStates.Add(key);
            } else {
                keyStates.Remove(key);
            }
        }
        public bool Pressing(Keys key) {
            return keyStates.Contains(key);
        }
    }
}
