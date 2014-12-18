using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class ApdTimerSe : ApdTimer
    {
        //internal Quartz32768HzCrystal MasterTimer;

        public ApdTimerSe(Scheduler sched, Calculator.InterruptId interruptId, Calculator master)
            : base(sched, interruptId, master)
        {
            /*if (master is Ti84PlusCSe)
                MasterTimer = ((Ti84PlusCSe)master).Crystal;*/
            //else if (master is TiSE84)
        }
        /*
        bool ChangedFrequency = false;
        double NewPeriod = 0;

        public override double Period
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
        */

    }
}
