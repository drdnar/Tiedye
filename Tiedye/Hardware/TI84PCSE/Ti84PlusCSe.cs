using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class Ti84PlusCSe : Calculator
    {
        public Quartz32768HzCrystal Crystal;
        public CrystalTimer CrystalTimer1;
        public CrystalTimer CrystalTimer2;
        public CrystalTimer CrystalTimer3;


        protected MemoryMapperSE Mapper
        {
            get
            {
                return (MemoryMapperSE)MemoryMapper;
            }
        }

        public ColorLcd Lcd = new ColorLcd();

        public Ti84PlusCSe()
        {
            Flash = new Flash4096K();
            Ram = new Ram128K();
            MemoryMapper = new MemoryMapperSE();
            MemoryMapper.Cpu = Cpu;
            MemoryMapper.Flash = Flash;
            MemoryMapper.Ram = Ram;
            (Mapper).Master = this;
            Cpu.MemoryRead = new Z80Core.Z80Cpu.BusRead(MemoryMapper.BusRead);
            Cpu.MemoryWrite = new Z80Core.Z80Cpu.BusWrite(MemoryMapper.BusWrite);
            Cpu.IoPortRead = new Z80Core.Z80Cpu.BusRead(ReadPort);
            Cpu.IoPortWrite = new Z80Core.Z80Cpu.BusWrite(WritePort);
            Crystal = new Quartz32768HzCrystal(this);
            DBus = new DBusDummy();
            CrystalTimer1 = new CrystalTimer(Crystal, Scheduler);
            CrystalTimer2 = new CrystalTimer(Crystal, Scheduler);
            CrystalTimer3 = new CrystalTimer(Crystal, Scheduler);
        }

        public override void Reset()
        {
            base.Reset();

        }

        protected override void doStep()
        {
            base.doStep();
            // Check for additional interrupt sources.
            Interrupt = Interrupt || CrystalTimer1.HasInterrupt || CrystalTimer2.HasInterrupt || CrystalTimer3.HasInterrupt;// || ... ;
            Cpu.Step();
        }

        public override void WritePort(object sender, ushort address, byte value)
        {
            address = (ushort)(address & 0xFF);
            switch (address)
            {
                case 0x00: // D-Bus
                    DBus.WriteLines(value);
                    break;
                case 0x01: // Keypad
                    Keypad.CurrentGroup = value;
                    break;
                case 0x02: // TODO: Interrupt ACK
                    break;
                case 0x03: // TODO: Interrupt mask
                    break;
                case 0x04: // Memory mapping, timer frequencies, battery cutoff voltage
                    // TODO: Timers
                    // TODO: Battery cutoff voltage
                    Mapper.MemoryMappingMode = value & 1;
                    break;
                case 0x05: // Memory page C
                    Mapper.PageC = value;
                    break;
                case 0x06: // Memory page A lower bits
                    Mapper.PageA = (Mapper.PageA & 0xF80) | (value & 0x7F);
                    Mapper.PageAIsRam = (value & 0x80) != 0;
                    break;
                case 0x07: // Memory page B lower bits
                    Mapper.PageB = (Mapper.PageB & 0xF80) | (value & 0x7F);
                    Mapper.PageBIsRam = (value & 0x80) != 0;
                    break;
                case 0x08: // TODO: UART enable
                    break;
                case 0x09: // TODO: UART status & signaling rate 0
                    break;
                case 0x0A: // TODO: UART input & signaling rate 1
                    break;
                case 0x0B: // TODO
                case 0x0C: // TODO
                case 0x0D: // TODO
                    break;
                case 0x0E: // Memory page A upper bits
                    Mapper.PageA = (Mapper.PageA & 0x7F) | (value << 7);
                    break;
                case 0x0F: // Memory Page B upper bits
                    Mapper.PageB = (Mapper.PageB & 0x7F) | (value << 7);
                    break;
                case 0x10: // LCD command port
                case 0x12:
                    Lcd.SetCurrentRegister(value);
                    break;
                case 0x11: // LCD data port
                case 0x13:
                    Lcd.WriteData(value);
                    break;
                case 0x14: // Flash write enable
                    // TODO: Implement protection
                    Mapper.FlashWriteEnable = (value & 1) != 0; // I dunno.
                    break;
                case 0x15: // Gate array ID
                    // Do nothing.
                    break;
                case 0x16: // Does nothing; this port's function has been moved.
                    break;
                case 0x17: // "Reserved for debug," whatever that means.
                    break;
                case 0x18: // TODO: MD5 ports
                case 0x19: // See http://www.faqs.org/rfcs/rfc1321.html
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x1E:
                case 0x1F:
                    break;
                case 0x20: // CPU speed
                    // TODO: Have something that keeps track of what these speeds should be.
                    switch (value)
                    {
                        case 0:
                            Cpu.Clock.Frequency = 6000000;
                            break;
                        case 1:
                            Cpu.Clock.Frequency = 15000000;
                            break;
                        case 2:
                            Cpu.Clock.Frequency = 15010000;
                            break;
                        case 3:
                            Cpu.Clock.Frequency = 15020000;
                            break;
                    }
                    break;
                case 0x21: // Flash & RAM size
                    if (!Mapper.FlashWriteEnable)
                        break;
                    Mapper.FlashType = (value & 3);
                    Mapper.RamType = (value & 3) >> 4;
                    break;
                case 0x22: // Flash execution lower limit
                    Mapper.FlashLowerLimit = Mapper.FlashLowerLimit & 0x100 | value;
                    break;
                case 0x23: // Flash execution upper limit
                    Mapper.FlashUpperLimit = Mapper.FlashUpperLimit & 0x100 | value;
                    break;
                case 0x24: // Flash execution limit hight bits
                    // TODO: Bits 2-7 are toggleable; retain their values for later readout.
                    Mapper.FlashLowerLimit = Mapper.FlashLowerLimit & 0xFF | (value & 0x100);
                    Mapper.FlashUpperLimit = Mapper.FlashUpperLimit & 0xFF | ((value & 0x200) >> 1);
                    break;
                case 0x25: // RAM execution lower limit
                    Mapper.RamLowerLimit = value;
                    break;
                case 0x26: // RAM execution upper limit
                    Mapper.RamUpperLimit = value;
                    break;
                case 0x27: // Stack space always present
                    Mapper.UpperPageAlwaysPresentAmount = value;
                    break;
                case 0x28: // System variables always present
                    Mapper.LowerPageAlwaysPresentAmount = value;
                    break;
                case 0x29: // CPU mode 0 delays
                case 0x2A:
                case 0x2B:
                case 0x2C:
                    // TODO: Actually implement delay cycles
                    break;
                case 0x2D: // 32768 Hz crystal control
                    // TODO: Actually implement this never-used feature.
                    break;
                case 0x2E: // Memory access delay
                    // TODO: Impement delay cycles
                    break;
                case 0x2F: // LCD I/O delay
                    // TODO: Implement delay cycles
                    break;
            }
        }

        public override byte ReadPort(object sender, ushort address)
        {
            switch (address & 0xFF)
            {
                case 0x00: // D-Bus
                    return DBus.ReadLines();
                case 0x01: // Keypad
                    return Keypad.Read();
                case 0x02: // TODO: System status
                    // Bit 1 is the LCD ready timer.
                    return (byte)(1 | 2 | (Mapper.FlashWriteEnable ? 4 : 0) | 0xE0);
                case 0x03: // Interrupt mask
                    return 0;
                case 0x04: // Interrupt ID
                    return (byte)(Keypad.OnKey ? 8 : 0);
                case 0x05: // Memory page C
                    return (byte)Mapper.PageC;
                case 0x06: // Memory page A lower bits
                    return (byte)((Mapper.PageA & 0x7F) | (Mapper.PageAIsRam ? 0x80 : 0));
                case 0x07: // Memory page B lower bits
                    return (byte)((Mapper.PageB & 0x7F) | (Mapper.PageBIsRam ? 0x80 : 0));
                case 0x08: // TODO: UART enable
                    return 0x80;
                case 0x09: // TODO: UART status & signaling rate 0
                    return 0;
                case 0x0A: // TODO: UART read buffer
                    return 0;
                case 0x0B: // TODO
                case 0x0C: // TODO
                case 0x0D: // UART write buffer
                    return 0;
                case 0x0E: // Memory page A upper bits
                    return (byte)(Mapper.PageA >> 7);
                case 0x0F: // Memory page B upper bits
                    return (byte)(Mapper.PageB >> 7);
                case 0x10: // LCD command port
                case 0x12:
                    return Lcd.GetCurrentRegister();
                case 0x11: // LCD data port
                case 0x13:
                    return Lcd.ReadData();
                case 0x14: // Flash write enable
                    return 0;
                case 0x15: // Gate array ID
                    return 0x45;
                case 0x16: // Does nothing; this port's function has been moved.
                    return 0;
                case 0x17: // "Reserved for debug," whatever that means.
                    return 0;
                case 0x18: // TODO: MD5 ports
                case 0x19: // See http://www.faqs.org/rfcs/rfc1321.html
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x1E:
                case 0x1F:
                    return 0;
                case 0x20: // CPU speed
                    if (Cpu.Clock.Frequency > 8000000)
                        return 1;
                    return 0;
                case 0x21: // Flash & RAM size
                    return (byte)((Mapper.FlashType) | (Mapper.RamType << 4));
                case 0x22: // Flash execution lower limit
                    return (byte)(Mapper.FlashLowerLimit);
                case 0x23: // Flash execution upper limit
                    return (byte)Mapper.FlashUpperLimit;
                case 0x24: // Flash execution limit hight bits
                    return (byte)((Mapper.FlashLowerLimit >> 8) | ((Mapper.FlashUpperLimit >> 7) & 2));
                case 0x25: // RAM execution lower limit
                    return (byte)(Mapper.RamLowerLimit);
                case 0x26: // RAM execution upper limit
                    return (byte)(Mapper.RamUpperLimit);
                case 0x27: // Stack space always present
                    return (byte)(Mapper.UpperPageAlwaysPresentAmount);
                case 0x28: // System variables always present
                    return (byte)(Mapper.LowerPageAlwaysPresentAmount);
                case 0x29: // CPU mode 0 delays
                case 0x2A:
                case 0x2B:
                case 0x2C:
                    return 0; // Whatever.
                case 0x2D: // 32768 Hz crystal control
                    return 3;
                case 0x2E: // Memory access delay
                    return 0;
                case 0x2F:
                    return 0x4B;

                // Dummy USB implementation
                case 0x4A:
                    return 0x24;
                case 0x4C:
                    return 0x22;
                case 0x4D:
                    return 0xA5;
                case 0x56:
                    return 0x50;
                case 0x57:
                    return 0x50;
                case 0x5B:
                    return 0;
                case 0x80:
                    return 0;
                case 0x49:
                case 0x4B:
                case 0x4E:
                case 0x4F:
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x58:
                case 0x59:
                case 0x5A:
                case 0x5C:
                case 0x5D:
                case 0x5E:
                case 0x5F:
                case 0x81:
                case 0x82:
                case 0x83:
                case 0x84:
                case 0x85:
                case 0x86:
                case 0x87:
                case 0x88:
                case 0x89:
                case 0x8A:
                case 0x8B:
                case 0x8C:
                case 0x8D:
                case 0x8E:
                case 0x8F:
                case 0xA0:
                case 0xA1:
                case 0xA2:
                case 0xA3:
                case 0xA4:
                case 0xA5:
                case 0xA6:
                case 0xA7:
                case 0xA8:
                case 0xA9:
                case 0xAA:
                case 0xAB:
                case 0xAC:
                case 0xAD:
                case 0xAE:
                case 0xAF:
                    return 255;
            }
            return 0;
        }
    }
}
