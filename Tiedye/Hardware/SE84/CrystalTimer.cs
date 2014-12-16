using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class CrystalTimer
    {
        internal Quartz32768HzCrystal MasterTimer;

        internal Scheduler Scheduler;

        protected EventHandler Oscillation;

        public CrystalTimer(Quartz32768HzCrystal master, Scheduler scheduler)
        {
            MasterTimer = master;
            Oscillation += new EventHandler(DoTick);
            Scheduler = scheduler;
        }

        public void DoTick(object sender, EventArgs e)
        {
            TickCounter--;
            if (TickCounter < 1)
            {
                count--;
                if (count == 0)
                {
                    if (GenerateInterrupt)
                        HasInterrupt = true;
                    if (Loop)
                    {
                        if (MissedLoop)
                            count = 255;
                        else
                            count = InitialCount;
                        MissedLoop = true;
                    }
                    
                }
                TickCounter = InitialTickCounter;
            }
        }

        

        protected int TickCounter = 1;
        protected int InitialTickCounter = 1;


        public void Reset()
        {

        }

        public bool HasInterrupt;

        protected void Deactivate()
        {

        }

        protected void Activate()
        {
            if (active)
                return;
            
        }

        private bool active = false;
        public bool Active
        {
            get
            {
                return active;
            }
            /*set
            {
                // ??
            }*/
        }

        

        public bool GenerateInterrupt
        {
            get;
            set;
        }

        public bool Loop
        {
            get;
            set;
        }

        public bool MissedLoop
        {
            get;
            set;
        }

        #region CPU interface
        protected byte frequencySelect;
        public byte FrequencySelect
        {
            get
            {
                return frequencySelect;
            }
            set
            {
                switch (value & 0xC0)
                {
                    case 0:
                        active = false;
                        break;
                    case 0x40:
                        switch (value & 7)
                        {
                            case 0:
                                InitialTickCounter = 3;
                                break;
                            case 1:
                                InitialTickCounter = 33;
                                break;
                            case 2:
                                InitialTickCounter = 328;
                                break;
                            case 3:
                                InitialTickCounter = 3277;
                                break;
                            case 4:
                                InitialTickCounter = 1;
                                break;
                            case 5:
                                InitialTickCounter = 16;
                                break;
                            case 6:
                                InitialTickCounter = 256;
                                break;
                            case 7:
                                InitialTickCounter = 4096;
                                break;
                        }
                        break;
                    case 0x80:
                        if ((value & 0x20) != 0)
                        {
                            InitialTickCounter = 64;
                        }
                        else if ((value & 0x10) != 0)
                        {
                            InitialTickCounter = 32;
                        }
                        else if ((value & 0x08) != 0)
                        {
                            InitialTickCounter = 16;
                        }
                        else if ((value & 0x04) != 0)
                        {
                            InitialTickCounter = 8;
                        }
                        else if ((value & 0x02) != 0)
                        {
                            InitialTickCounter = 4;
                        }
                        else if ((value & 0x01) != 0)
                        {
                            InitialTickCounter = 2;
                        }
                        else
                        {
                            InitialTickCounter = 1;
                        }
                        break;
                }
            }
        }

        public byte Mode
        {
            get;
            set;
        }

        public int InitialCount;

        protected int count;
        public byte Count
        {
            get
            {
                unchecked
                {
                    return (byte)count;
                }
            }
            set
            {
                count = value;
                InitialCount = value;
                MissedLoop = false;
            }
        }
        #endregion
    }
}
