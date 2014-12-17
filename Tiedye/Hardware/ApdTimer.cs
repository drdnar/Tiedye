using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class ApdTimer
    {
        public double Period = 0.002;

        protected Scheduler Scheduler;

        protected Scheduler.WallTimeEvent NextIncrement;

        public readonly Calculator.InterruptId InterruptId;

        protected Calculator Master;

        public ApdTimer(Scheduler sched, Calculator.InterruptId interruptId, Calculator master)
        {
            NextIncrement = new Scheduler.WallTimeEvent();
            NextIncrement.Tag = "HW Timer tick";
            NextIncrement.Handler = new EventHandler<Scheduler.WallTimeEvent>(DoTick);
            Scheduler.EnqueueRelativeEvent(NextIncrement, Period);
            InterruptId = interruptId;
            Master = master;
        }

        public void DoTick(object sender, Scheduler.WallTimeEvent e)
        {
            Scheduler.EnqueueRelativeEvent(e, Period);
            if (GenerateInterrupt)
                Master.SetInterrupt(InterruptId);

        }
        
        public void Reset()
        {
            HasInterrupt = false;
        }

        public bool HasInterrupt
        {
            get
            {
                return Master.GetInterrupt(InterruptId);
            }
            set
            {
                Master.SetInterrupt(InterruptId, value);
            }
        }

        protected bool generateInterrupt = false;
        public bool GenerateInterrupt
        {
            get
            {
                return generateInterrupt;
            }
            set
            {
                generateInterrupt = value;
                if (!value)
                {
                    Master.ClearInterrupt(InterruptId);
                }
            }
        }

    }
}
