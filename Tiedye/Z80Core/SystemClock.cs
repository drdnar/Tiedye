using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Z80Core
{
    public class SystemClock
    {
        private double frequency = 2000000;
        private double period = 1.0 / 2000000;

        /// <summary>
        /// Clock speed in hertz.
        /// </summary>
        public double Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
                period = 1 / value;
            }
        }

        public double Period
        {
            get
            {
                return period;
            }
        }
        
        private double wallTime = 0;
        /// <summary>
        /// Time in seconds. This is used for CPU throttling, and may be used
        /// by peripherals for timing.
        /// </summary>
        public double WallTime
        {
            get
            {
                return wallTime;
            }
        }

        /// <summary>
        /// Increments the wall time. This does not update the clock cycles
        /// count; this assumes the CPU was halted.
        /// </summary>
        /// <param name="seconds"></param>
        public void IncWallTime(double seconds)
        {
            wallTime += seconds;
        }
        
        private long clockTime = 0;
        /// <summary>
        /// Time in clock cycles.
        /// </summary>
        public long ClockTime
        {
            get
            {
                return clockTime;
            }
        }

        /// <summary>
        /// Increments the clock cycles count. This also updates the wall time.
        /// </summary>
        /// <param name="clockCycles"></param>
        public void IncTime(long clockCycles)
        {
            clockTime += clockCycles;
            wallTime += clockCycles * period;
        }
    }
}
