using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiedye.Hardware
{
    /// <summary>
    /// General class for 8x series calculators
    /// </summary>
    public abstract class Calculator
    {
        public Z80Core.Z80Cpu Cpu;
        public Flash Flash;
        public Ram Ram;
        public MemoryMapper MemoryMapper;
        public Keypad Keypad;
        public DBus DBus;
        public Scheduler Scheduler;

        public Calculator()
        {
            Cpu = new Z80Core.Z80Cpu();
            Cpu.Clock.Frequency = 6000000;
            Scheduler = new Scheduler(Cpu.Clock);
            Keypad = new Keypad();
        }

        public virtual void Reset()
        {
            Cpu.ForceReset = true;
            Flash.Reset();
            Ram.Reset();
            MemoryMapper.Reset();
            Keypad.Reset();
            DBus.Reset();
        }

        public bool Interrupt = false;
        
        /// <summary>
        /// Checks all interrupt sources for interrupt state, then steps the CPU.
        /// </summary>
        public void Step()
        {
            doStep();
            if (ExecutionFinished != null)
                ExecutionFinished(this, null);
        }
        public void Step(long count)
        {
            while (count-- > 0) // "while count goes toward zero"
            {
                doStep();
                if (Cpu.Break)
                    break;
            }
            if (ExecutionFinished != null)
                ExecutionFinished(this, null);
        }


        protected virtual void doStep()
        {
            Interrupt = false;
            Scheduler.ProcessEvents();
            Interrupt = DBus.HasInterrupt || Keypad.HasInterrupt;
        }

        /// <summary>
        /// This event is fired after Step() or ExecuteFor(), allowing monitors
        /// to update themselves.
        /// </summary>
        public event EventHandler ExecutionFinished;

        public virtual void ExecuteFor(double seconds)
        {
            double endTime = Cpu.Clock.WallTime + seconds;
            int abortTimer = 1000000;
            while (Cpu.Clock.WallTime < endTime)
            {
                doStep();
                if (abortTimer-- < 0)
                    break;
                if (Cpu.Break)
                    break;
            }
            if (ExecutionFinished != null)
                ExecutionFinished(this, null);
        }

        public abstract void WritePort(object sender, ushort address, byte value);

        public abstract byte ReadPort(object sender, ushort address);
    }
}
