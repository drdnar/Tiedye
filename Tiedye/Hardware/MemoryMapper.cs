using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public abstract class MemoryMapper
    {
        public Z80Core.Z80Cpu Cpu;
        public Flash Flash;
        public Ram Ram;

        public bool BootMode = true;

        public virtual void Reset()
        {
            BootMode = true;
            bank0000 = 0x7FC000;
            Bank0000IsRam = false;
            bank4000 = 0;
            Bank4000IsRam = false;
            bank8000 = 0;
            Bank8000IsRam = false;
            bankC000 = 0;
            BankC000IsRam = true;
            
        }

        protected int bank0000;

        /// <summary>
        /// Controls what data is in the space 0000-3FFF.
        /// Placing RAM here would be weird.
        /// </summary>
        public int Bank0000
        {
            get
            {
                return bank0000 >> 14;
            }
            set
            {
                bank0000 = value << 14;
            }
        }

        public bool Bank0000IsRam;


        protected int bank4000;

        public int Bank4000
        {
            get
            {
                return bank4000 >> 14;
            }
            set
            {
                bank4000 = value << 14;
                if (Bank4000IsRam)
                    bank4000 = bank4000 & Ram.SizeMask;
                else
                    bank4000 = bank4000 & Flash.SizeMask;
                if (value > 0x1FF)
                    bank4000 = value << 14;
            }
        }

        public bool Bank4000IsRam;


        protected int bank8000;

        public int Bank8000
        {
            get
            {
                return bank8000 >> 14;
            }
            set
            {
                bank8000 = value << 14;
                if (Bank8000IsRam)
                    bank8000 = bank8000 & Ram.SizeMask;
                else
                    bank8000 = bank8000 & Flash.SizeMask;
            }
        }

        public bool Bank8000IsRam;

        protected int bankC000;

        public int BankC000
        {
            get
            {
                return bankC000 >> 14;
            }
            set
            {
                bankC000 = value << 14;
                if (Bank8000IsRam)
                    bankC000 = bankC000 & Ram.SizeMask;
                else
                    bankC000 = bankC000 & Flash.SizeMask;
            }
        }

        public bool BankC000IsRam = true;

        public abstract byte BusRead(object sender, ushort address);

        public abstract void BusWrite(object sender, ushort address, byte value);
        
        public enum MemoryBreakpointType
        {
            Execution,
            Read,
            Write,
        }

        public struct Breakpoint
        {
            public int Page;
            public ushort Address;
            public bool IsRam;
            public MemoryBreakpointType Type;
            public override string ToString()
            {
                return (Type == MemoryBreakpointType.Execution ? "EXEC: " : (Type == MemoryBreakpointType.Read ? "READ: " : "WRITE: "))
                    + (IsRam ? "R" : "F")
                    + Page.ToString(IsRam ? "X1" : "X2")
                    + ":"
                    + Address.ToString("X4");
            }
        }

        public void AddBreakpoint(Breakpoint bp, bool isActive)
        {
            bp.Address = (ushort)(bp.Address & 0x3FFF);
            switch (bp.Type)
            {
                case MemoryBreakpointType.Execution:
                    haveExecBps = true;
                    if (!ExecBps.ContainsKey(bp))
                        ExecBps.Add(bp, isActive);
                    break;
                case MemoryBreakpointType.Read:
                    haveReadBps = true;
                    if (!ReadBps.ContainsKey(bp))
                        ReadBps.Add(bp, isActive);
                    break;
                case MemoryBreakpointType.Write:
                    haveWriteBps = true;
                    if (!WriteBps.ContainsKey(bp))
                        WriteBps.Add(bp, isActive);
                    break;
            }
        }

        public void DeleteBreakpoint(Breakpoint bp)
        {
            switch (bp.Type)
            {
                case MemoryBreakpointType.Execution:
                    //if (ExecBps.ContainsKey(bp))
                    ExecBps.Remove(bp);
                    if (ExecBps.Count == 0)
                        haveExecBps = false;
                    break;
                case MemoryBreakpointType.Read:
                    //if (ReadBps.ContainsKey(bp))
                    ReadBps.Remove(bp);
                    if (ReadBps.Count == 0)
                        haveReadBps = false;
                    break;
                case MemoryBreakpointType.Write:
                    //if (WriteBps.ContainsKey(bp))
                    WriteBps.Remove(bp);
                    if (WriteBps.Count == 0)
                        haveWriteBps = false;
                    break;
            }
        }

        public void ActivateBreakpoint(Breakpoint bp)
        {
            switch (bp.Type)
            {
                case MemoryBreakpointType.Execution:
                    if (ExecBps.ContainsKey(bp))
                        ExecBps[bp] = true;
                    break;
                case MemoryBreakpointType.Read:
                    if (ReadBps.ContainsKey(bp))
                        ReadBps[bp] = true;
                    break;
                case MemoryBreakpointType.Write:
                    if (WriteBps.ContainsKey(bp))
                        WriteBps[bp] = true;
                    break;
            }
        }

        public void DeactivateBreakpoint(Breakpoint bp)
        {
            switch (bp.Type)
            {
                case MemoryBreakpointType.Execution:
                    if (ExecBps.ContainsKey(bp))
                        ExecBps[bp] = false;
                    break;
                case MemoryBreakpointType.Read:
                    if (ReadBps.ContainsKey(bp))
                        ReadBps[bp] = false;
                    break;
                case MemoryBreakpointType.Write:
                    if (WriteBps.ContainsKey(bp))
                        WriteBps[bp] = false;
                    break;
            }
        }

        public bool IsBreakpoint(Breakpoint bp)
        {
            switch (bp.Type)
            {
                case MemoryBreakpointType.Execution:
                    return IsExecutionBreakpoint(bp);
                case MemoryBreakpointType.Read:
                    return IsReadBreakpoint(bp);
                case MemoryBreakpointType.Write:
                    return IsWriteBreakpoint(bp);
            }
            return false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsExecutionBreakpoint(Breakpoint bp)
        {
            bool isActive = false;
            return ExecBps.TryGetValue(bp, out isActive) ? isActive : false;
        }
        
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsReadBreakpoint(Breakpoint bp)
        {
            bool isActive = false;
            return ReadBps.TryGetValue(bp, out isActive) ? isActive : false;
        }
        
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsWriteBreakpoint(Breakpoint bp)
        {
            bool isActive = false;
            return WriteBps.TryGetValue(bp, out isActive) ? isActive : false;
        }

        public List<KeyValuePair<Breakpoint, bool>> GetBreakpoints()
        {
            List<KeyValuePair<Breakpoint, bool>> items = new List<KeyValuePair<Breakpoint, bool>>(ExecBps);
            items.AddRange(ReadBps);
            items.AddRange(WriteBps);
            return items;
        }

        protected bool haveExecBps = false;
        Dictionary<Breakpoint, bool> ExecBps = new Dictionary<Breakpoint, bool>();

        protected bool haveReadBps = false;
        Dictionary<Breakpoint, bool> ReadBps = new Dictionary<Breakpoint, bool>();

        protected bool haveWriteBps = false;
        Dictionary<Breakpoint, bool> WriteBps = new Dictionary<Breakpoint, bool>();

    }
}
