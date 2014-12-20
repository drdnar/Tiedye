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
        public ApdTimer Apd;
        public ApdTimer PApd;


        protected bool OffEnableMode = false;
        public int TimerSetting = 0;

        public Calculator()
        {
            Cpu = new Z80Core.Z80Cpu();
            Cpu.Clock.Frequency = 6000000;
            Scheduler = new Scheduler(Cpu.Clock);
            Keypad = new Keypad(this);
        }

        public virtual void Reset()
        {
            Cpu.ForceReset = true;
            OffEnableMode = false;
            Flash.Reset();
            Ram.Reset();
            MemoryMapper.Reset();
            Keypad.Reset();
            DBus.Reset();
        }

        public bool Interrupt
        {
            get
            {
                return Interrupts != 0;
            }
        }

        /// <summary>
        /// Bitfield showing what devices are asserting an interrupt.
        /// </summary>
        public InterruptId Interrupts = InterruptId.None;

        [Flags]
        public enum InterruptId : ulong
        {
            None = 0,
            // Byte 0: Basic interrupts
            OnKey = 1,
            ApdTimer1 = 2,
            ApdTimer2 = 4,
            DBus = 8,
            CrystalTimer1 = 0x10,
            CrystalTimer2 = 0x20,
            CrystalTimer3 = 0x40,
            // Byte 1: Link assist
            LinkAssistRx = 0x100,
            LinkAssistTx = 0x200,
            LinkAssistError = 0x400,
            /*// Byte 2: USB misc
            UsbSuspend = 0x10000,
            UsbVBusTimeout = 0x20000,
            UsbLines = 0x40000,
            UsbVScreen = 0x80000,
            UsbProtocol = 0x100000,
            // Byte 3: USB lines
            UsbLineEventDipFall = 0x1000000,
            UsbLineEventDipRise = 0x2000000,
            UsbLineEventDimFall = 0x4000000,
            UsbLineEventDimRise = 0x8000000,
            UsbLineEventCidFall = 0x10000000,
            UsbLineEventCidRise = 0x20000000,
            UsbLineEventVBusRise = 0x40000000,
            UsbLineEventVBusFall = 0x80000000,
            // Byte 4: USB HW enable
            UsbHwEnableDipFall = 0x100000000,
            UsbHwEnableDipRise = 0x200000000,
            UsbHwEnableDimFall = 0x400000000,
            UsbHwEnableDimRise = 0x800000000,
            UsbHwEnableCidFall = 0x1000000000,
            UsbHwEnableCidRise = 0x2000000000,
            UsbHwEnableVBusRise = 0x4000000000,
            UsbHwEnableVBusFall = 0x8000000000,
            // Byte 5: USB protocol interrupts
            UsbProtocolSuspend = 0x10000000000,
            UsbProtocolEp0 = 0x20000000000,
            UsbProtocolResume = 0x20000000000,
            UsbProtocolBabble = 0x40000000000,
            UsbProtocolReset = 0x40000000000,
            UsbProtocolSof = 0x80000000000,
            UsbProtocolConnect = 0x100000000000,
            UsbProtocolDisconnect = 0x200000000000,
            UsbProtocolSessReq = 0x400000000000,
            UsbProtocolVBusError = 0x800000000000,
            // Byte 6: USB TX interrupts
            UsbTxPipe0 = 0x1000000000000,
            UsbTxPipe1 = 0x2000000000000,
            UsbTxPipe2 = 0x4000000000000,
            UsbTxPipe3 = 0x8000000000000,
            UsbTxPipe4 = 0x10000000000000,
            UsbTxPipe5 = 0x20000000000000,
            UsbTxPipe6 = 0x40000000000000,
            UsbTxPipe7 = 0x80000000000000,
            // Byte 7: USB RX interrupts
            UsbRxPipe0 = 0x100000000000000,
            UsbRxPipe1 = 0x200000000000000,
            UsbRxPipe2 = 0x400000000000000,
            UsbRxPipe3 = 0x800000000000000,
            UsbRxPipe4 = 0x1000000000000000,
            UsbRxPipe5 = 0x2000000000000000,
            UsbRxPipe6 = 0x4000000000000000,
            UsbRxPipe7 = 0x8000000000000000,*/
        }

        /// <summary>
        /// Latches interrupt in enabled state
        /// </summary>
        /// <param name="intid"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetInterrupt(InterruptId intid)
        {
            Interrupts |= intid;
        }

        /// <summary>
        /// Sets interrupt latch
        /// </summary>
        /// <param name="intid"></param>
        /// <param name="value"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetInterrupt(InterruptId intid, bool value)
        {
            if (value)
                Interrupts |= intid;
            else
                Interrupts &= ~intid;
        }

        /// <summary>
        /// Clear interrupt latch
        /// </summary>
        /// <param name="intid"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void ClearInterrupt(InterruptId intid)
        {
            Interrupts &= ~intid;
        }

        /// <summary>
        /// Returns whether or not a given interrupt is being asserted
        /// </summary>
        /// <param name="intid"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool GetInterrupt(InterruptId intid)
        {
            return (Interrupts & intid) != 0;
        }


        /// <summary>
        /// Checks all interrupt sources for interrupt state, then steps the CPU.
        /// </summary>
        public void Step()
        {
            if (Cpu.Halt)
                DoHalt(); 
            else
                doStep();
            if (ExecutionFinished != null)
                ExecutionFinished(this, null);
        }
        public void Step(long count)
        {
            while (count --> 0) // "while count goes toward zero"
            {
                doStep();
                if (Cpu.Break)
                    break;
                if (Cpu.Halt)
                    DoHalt();
            }
            if (ExecutionFinished != null)
                ExecutionFinished(this, null);
        }


        protected virtual void doStep()
        {
            //Interrupt = false;
            Cpu.Interrupt = Interrupts != InterruptId.None;
            Scheduler.ProcessEvents();
            //Interrupt = DBus.HasInterrupt || Keypad.HasInterrupt;
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
                if (Cpu.Halt)
                    DoHalt();
            }
            if (ExecutionFinished != null)
                ExecutionFinished(this, null);
        }

        public void DoHalt()
        {
            //Keypad.HasInterrupt = true;
            double t = Scheduler.GetNextEventTime() - Cpu.Clock.WallTime;
            if (t > 0)
                Cpu.Clock.IncWallTime(t);
             
        }

        public abstract void WritePort(object sender, ushort address, byte value);

        public abstract byte ReadPort(object sender, ushort address);
    }
}
