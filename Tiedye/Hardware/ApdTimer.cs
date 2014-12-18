using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class ApdTimer
    {
        protected double period = 0.002;
        public virtual double Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
            }
        }

        protected Scheduler Scheduler;

        protected Scheduler.WallTimeEvent NextIncrement;

        public readonly Calculator.InterruptId InterruptId;

        protected Calculator Master;
        public ApdTimer(Scheduler sched, Calculator.InterruptId interruptId, Calculator master) : this(sched, interruptId, master, 0.002)
        {
            
        }
        
        public ApdTimer(Scheduler sched, Calculator.InterruptId interruptId, Calculator master, double period)
        {
            NextIncrement = new Scheduler.WallTimeEvent();
            NextIncrement.Tag = "HW Timer tick " + interruptId.ToString();
            NextIncrement.Handler = new EventHandler<Scheduler.WallTimeEvent>(DoTick);
            Scheduler = sched;
            InterruptId = interruptId;
            Master = master;
            this.period = period;
            Scheduler.EnqueueRelativeEvent(NextIncrement, Period);
        }

        public virtual void DoTick(object sender, Scheduler.WallTimeEvent e)
        {
            Scheduler.EnqueueRelativeEvent(e, Period);
            if (GenerateInterrupt)
                Master.SetInterrupt(InterruptId);
        }
        
        public void Reset()
        {
            HasInterrupt = false;
        }

        public double NextTick
        {
            get
            {
                return NextIncrement.Time;
            }
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
