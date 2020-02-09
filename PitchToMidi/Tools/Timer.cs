using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchToMidi {
    public static class Timer {
        public static long Time {
            get {
                return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
        }
        public static long Passed(long time) {
            return Time - time;
        }
    }
}
