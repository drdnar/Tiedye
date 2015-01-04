using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public abstract class Flash : Memory
    {        
        public byte[] Data;

        public int BootSector;

        protected bool writeEnable;
        public bool WriteEnable
        {
            get
            {
                return writeEnable;
            }
            set
            {
                writeEnable = value;
                if (!value)
                    CurrentCommand = Command.None;
            }
        }

        protected enum Command
        {
            None,
            Unlock1,
            Unlock2,
            Write,
            EraseSectorStart,
            EraseSectorConfirmStart,
            EraseSector,
            Writing,
            Erasing,
            Error,
        }
        protected Command CurrentCommand = Command.None;

        public override byte ReadByte(object sender, int address)
        {
            if (sender is Z80Core.Z80Cpu)
                if (CurrentCommand != Command.None)
                {
                    if (CurrentCommand == Command.Writing)
                    {
                        return (byte)(~(written ^= 0x40));
                    }
                    //else
                    CurrentCommand = Command.None;
                }
            // TODO: Optimize this
            return Data[address & SizeMask];
        }

        public abstract int GetSectorStart(int address);
        public abstract int GetSectorLength(int address);

        byte written;

        public override void WriteByte(object sender, int address, byte value)
        {
            if (!(sender is Z80Core.Z80Cpu))
            {
                Data[address & SizeMask] = value;
                return;
            }
            if (!writeEnable)
                return;
            if (CurrentCommand == Command.Write)
            {
                WriteByteForced(sender, address, value);
                written = value;
                CurrentCommand = Command.None;//Command.Writing;
            }
            else if (CurrentCommand == Command.EraseSector && value == 0x30)
            {
                int end = GetSectorStart(address) + GetSectorLength(address);
                for (int i = GetSectorStart(address); i < end; i++)
                    Data[i] = 0xFF;
                CurrentCommand = Command.None;
            }
            else if ((address & 0xFFF) == 0xAAA)
            {
                if (CurrentCommand == Command.None && value == 0xAA)
                    CurrentCommand = Command.Unlock1;
                else if (CurrentCommand == Command.Unlock2 && value == 0xA0)
                    CurrentCommand = Command.Write;
                else if (CurrentCommand == Command.Unlock2 && value == 0x80)
                    CurrentCommand = Command.EraseSectorStart;
                else if (CurrentCommand == Command.EraseSectorConfirmStart)
                    CurrentCommand = Command.EraseSector;
                else
                    CurrentCommand = Command.None;
            }
            else if ((address & 0xFFF) == 0x555)
            {
                if (CurrentCommand == Command.Unlock1 && value == 0x55)
                    CurrentCommand = Command.Unlock2;
                else if (CurrentCommand == Command.EraseSectorStart)
                    CurrentCommand = Command.EraseSectorConfirmStart;
            }
        }

        public virtual void WriteByteForced(object sender, int address, byte value)
        {
            Data[address] = (byte)(Data[address] & value);
        }
    }
}
