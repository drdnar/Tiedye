using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiedye.Z80Core;

namespace Tiedye.Hardware
{
    /// <summary>
    /// For calculators containing a 32768 Hz quartz crystal, this is the
    /// master crystal timing source.
    /// </summary>
    public class Quartz32768HzCrystal
    {
        protected Z80Cpu Cpu;
        public readonly Calculator Master;
        protected Scheduler Scheduler;
        
        protected Scheduler.WallTimeEvent NextIncrement;

        /// <summary>
        /// Devices that need notification that the 32768 crystal has ticked should hook into this event.
        /// </summary>
        public EventHandler Tick;

        private void DoTick(object sender, Scheduler.WallTimeEvent e)
        {
            Scheduler.EnqueueRelativeEvent(e, 1.0 / 32768);
            if (Tick != null)
                Tick(this, null);
        }

        public Quartz32768HzCrystal(Calculator master)
        {
            Master = master;
            Cpu = master.Cpu;
            Scheduler = Master.Scheduler;

            NextIncrement = new Scheduler.WallTimeEvent();
            NextIncrement.Tag = "32768 Hz Crystal Tick";
            NextIncrement.Handler = new EventHandler<Scheduler.WallTimeEvent>(DoTick);
            Scheduler.EnqueueRelativeEvent(NextIncrement, 1.0 / 32768);
        }

    }
}
