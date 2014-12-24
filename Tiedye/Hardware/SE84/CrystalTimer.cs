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

        //protected EventHandler Oscillation;

        protected Scheduler.WallTimeEvent NextWtIncrement;
        protected Scheduler.SystemClockEvent NextScIncrement;

        readonly public int TimerNumber;

        public readonly Calculator.InterruptId InterruptId;

        public double NextWtTime
        {
            get
            {
                return NextWtIncrement.Time;
            }
        }

        public long NextScTime
        {
            get
            {
                return NextScIncrement.Time;
            }
        }

        public CrystalTimer(Quartz32768HzCrystal master, Scheduler scheduler, int timerNumber)
        {
            switch (timerNumber)
            {
                case 1:
                    InterruptId = Calculator.InterruptId.CrystalTimer1;
                    break;
                case 2:
                    InterruptId = Calculator.InterruptId.CrystalTimer2;
                    break;
                case 3:
                    InterruptId = Calculator.InterruptId.CrystalTimer3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("timerNumber must be between 1 and 3");
            }
            TimerNumber = timerNumber;
            MasterTimer = master;
            //Oscillation = new EventHandler(DoTick);
            Scheduler = scheduler;
            
            NextWtIncrement = new Scheduler.WallTimeEvent();
            NextWtIncrement.Tag = "Crystal timer " + timerNumber.ToString() + " wall-time tick";
            NextWtIncrement.Handler = new EventHandler<Scheduler.WallTimeEvent>(DoTickWt);
            NextScIncrement = new Scheduler.SystemClockEvent();
            NextScIncrement.Tag = "Crystal timer " + timerNumber.ToString() + " clock-time tick";
            NextScIncrement.Handler = new EventHandler<Scheduler.SystemClockEvent>(DoTickSc);
            //Scheduler.EnqueueRelativeEvent(NextWtIncrement, 1.0 / 32768);
        }

        public bool HasInterrupt
        {
            get
            {
                return MasterTimer.Master.GetInterrupt(InterruptId);
            }
            set
            {
                MasterTimer.Master.SetInterrupt(InterruptId, value);
            }
        }
        
        private void DoTickWt(object sender, Scheduler.WallTimeEvent e)
        {
            unchecked
            {
                if (--count == 0)
                {
                    if (MissedLoop)
                        count = 255;
                    else
                        count = InitialCount;
                    if (Loop)
                    {
                        MissedLoop = true;
                        Scheduler.EnqueueRelativeEvent(e, WtPeriod);
                    }
                    else
                        IsActive = false;
                    if (GenerateInterrupt)
                        HasInterrupt = true;
                    HasExpired = true;
                }
                else
                    Scheduler.EnqueueRelativeEvent(e, WtPeriod);
            }
        }

        private void DoTickSc(object sender, Scheduler.SystemClockEvent e)
        {
            // TODO: This won't count correctly when the rate is faster than each CPU instruction
            unchecked
            {
                if (--count == 0)
                {
                    if (MissedLoop)
                        count = 255;
                    else
                        count = InitialCount;
                    if (Loop)
                    {
                        MissedLoop = true;
                        Scheduler.EnqueueRelativeEvent(e, ScPeriod);
                    }
                    else
                        IsActive = false;
                    if (GenerateInterrupt)
                        HasInterrupt = true;
                    HasExpired = true;
                }
                else
                    Scheduler.EnqueueRelativeEvent(e, ScPeriod);
            }
        }

        public void Reset()
        {
            HasInterrupt = false;
            HasExpired = false;
        }

        protected double WtPeriod;
        protected long ScPeriod;

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

        bool hasExpired;
        public bool HasExpired
        {
            get
            {
                return hasExpired;
            }
            set
            {
                hasExpired = value;
            }
        }

        bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                if (!value)
                {
                    HasInterrupt = false;
                    HasExpired = false;
                }
                isActive = value;
            }
        }

        public byte CounterMode;

        public enum Mode
        {
            Off = 0,
            FixedFrequency = 1,
            CpuClockCounter = 2,
            AdjustedClock = 3
        }

        public Mode FrequencyMode
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

        public void SetMode(byte value)
        {
            if (CounterMode == value)
                return;
            CounterMode = value;
            IsActive = false;
            Scheduler.RemoveEvent(NextWtIncrement);
            Scheduler.RemoveEvent(NextScIncrement);
            switch (CounterMode & 0xC0)
            {
                case 0:
                    FrequencyMode = CrystalTimer.Mode.Off;
                    break;
                case 0x40:
                    //Scheduler.EnqueueEvent(NextWtIncrement, MasterTimer.NextTickTime);
                    FrequencyMode = CrystalTimer.Mode.FixedFrequency;
                    switch (value & 7)
                    {
                        case 0:
                            WtPeriod = 3.0 / 32768.0;
                            break;
                        case 1:
                            WtPeriod = 33.0 / 32768.0;
                            break;
                        case 2:
                            WtPeriod = 328.0 / 32768;
                            break;
                        case 3:
                            WtPeriod = 3277.0 / 32768.0;
                            break;
                        case 4:
                            WtPeriod = 1.0 / 32768.0;
                            break;
                        case 5:
                            WtPeriod = 1.0 / 2048.0;
                            break;
                        case 6:
                            WtPeriod = 1.0 / 128.0;
                            break;
                        case 7:
                            WtPeriod = 1.0 / 8.0;
                            break;
                    }
                    break;
                case 0x80:
                    ScPeriod = 1 << (value & 7);
                    FrequencyMode = CrystalTimer.Mode.CpuClockCounter;
                    break;
                case 0xC0:
                    // TODO: This.
                    break;
            }
        }

        public void SetLoopControl(byte value)
        {
            MissedLoop = false;
            Loop = (value & 1) != 0;
            GenerateInterrupt = (value & 2) != 0;
            HasInterrupt = false;
            HasExpired = false;
        }

        public void SetCount(byte value)
        {
            //IsActive = true;
            Count = value;
            switch (FrequencyMode)
            {
                case Mode.Off:
                    return;
                case Mode.FixedFrequency:
                    Scheduler.EnqueueEvent(NextWtIncrement, MasterTimer.NextTickTime + WtPeriod);
                    break;
                case Mode.CpuClockCounter:
                    Scheduler.EnqueueRelativeEvent(NextScIncrement, ScPeriod);
                    break;
                case Mode.AdjustedClock:
                    return;
            }
            IsActive = true;
        }

        public byte GetMode()
        {
            return CounterMode;
        }

        public byte GetLoopStatus()
        {
            return (byte)((MissedLoop ? 4 : 0) | (GenerateInterrupt ? 2 : 0) | (Loop ? 1 : 0));
        }

        public byte GetCount()
        {
            return Count;
        }


        /*
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

        */
        /*
        protected int TickCounter = 1;
        protected int InitialTickCounter = 1;


        public void Reset()
        {
            HasInterrupt = false;
        }

        public bool HasInterrupt
        {
            get
            {
                return MasterTimer.Master.GetInterrupt(InterruptId);
            }
            set
            {
                MasterTimer.Master.SetInterrupt(InterruptId, value);
            }
        }

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
            set
            {
                active = value;
            }
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
         * */
    }
}
