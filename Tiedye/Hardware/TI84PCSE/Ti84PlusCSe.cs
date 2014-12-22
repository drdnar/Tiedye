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
            CrystalTimer1 = new CrystalTimer(Crystal, Scheduler, 1);
            CrystalTimer2 = new CrystalTimer(Crystal, Scheduler, 2);
            CrystalTimer3 = new CrystalTimer(Crystal, Scheduler, 3);
            Apd = new ApdTimerSe(Scheduler, InterruptId.ApdTimer1, this, 1.0 / 512);
            PApd = new ApdTimerSe(Scheduler, InterruptId.ApdTimer2, this, 1.0 / 1024);
        }

        public override void Reset()
        {
            base.Reset();

        }

        protected override void doStep()
        {
            base.doStep();
            // Check for additional interrupt sources.
            //Interrupt = Interrupt || CrystalTimer1.HasInterrupt || CrystalTimer2.HasInterrupt || CrystalTimer3.HasInterrupt;// || ... ;
            Cpu.Step();
        }

        public int CpuSpeed
        {
            get;
            set;
        }

        public enum PortNames
        {
            Link = 0,
            Key = 1,
            StatusIntAck = 2,
            IntMask = 3,
            IntIdAsicCfg = 4,
            MemoryPageC = 5,
            MemoryPageA = 6,
            MemoryPageB = 7,
            LinkAssistEnable = 8,
            LinkAssistStatusSpeed6Mhz = 9,
            LinkAssistInBufferSpeed15Mhz = 0x0A,
            LinkAssistSpeed20Mhz = 0x0B,
            LinkAssistSpeed25Mhz = 0x0C,
            LinkAssistOutBuffer = 0x0D,
            MemoryPageAHigh = 0x0E,
            MemoryPageBHigh = 0x0F,
            LcdCommand = 0x10,
            LcdData = 0x11,
            ProtectionControl = 0x14,
            AsicId = 0x15,
            Md5RegA = 0x18,
            Md5RegB = 0x19,
            Md5RegC = 0x1A,
            Md5RegD = 0x1B,
            Md5RegXByte0 = 0x1C,
            Md5RegACByte1 = 0x1D,
            Md5RegSByte2 = 0x1E,
            Md5FuncByte3 = 0x1F,
            CpuSpeed = 0x20,
            FlashRamType = 0x21,
            FlashExecLowerLimit = 0x22,
            FlashExecUpperLimit = 0x23,
            FlashExecHighBitsLimit = 0x24,
            RamExecLowerLimit = 0x25,
            RamExecUpperLimit = 0x26,
            BlockMemoryRemapPageC = 0x27,
            BlockMemoryRemapPageB = 0x28,
            LcdDelay6MHz = 0x29,
            LcdDelay15MHz = 0x2A,
            LcdDelay20MHz = 0x2B,
            LcdDelay25MHz = 0x2C,
            CrystalKeepAlive = 0x2D,
            MemoryDelay = 0x2E,
            LcdDelay = 0x2F,
            Timer1Frequency = 0x30,
            Timer1Config = 0x31,
            Timer1Count = 0x32,
            Timer2Frequency = 0x33,
            Timer2Config = 0x34,
            Timer2Count = 0x35,
            Timer3Frequency = 0x36,
            Timer3Config = 0x37,
            Timer3Count = 0x38,
            GpioDirection = 0x39,
            GpioData = 0x3A,
            ExtCs0 = 0x3C,
            ExtCs1 = 0x3D,
            ExtCs2 = 0x3E,
            ExtCs3 = 0x3F,
            RtcControl = 0x40,
            RtcSet0 = 0x41,
            RtcSet1 = 0x42,
            RtcSet2 = 0x43,
            RtcSet3 = 0x44,
            RtcRead0 = 0x45,
            RtcRead1 = 0x46,
            RtcRead2 = 0x47,
            RtcRead3 = 0x48,
            UsbXcvr = 0x49,
            PuPdCtrl = 0x4A,
            VBusCtrl = 0x4B,
            UsbSystem = 0x4C,
            UsbActivity = 0x4D,
            ZsVBusControl = 0x4F,
            ZsVBusSetValue = 0x50,
            Usb48MHzGateTime = 0x51,
            ChargePumpEnableTime = 0x52,
            PdEnableTime = 0x53,
            UsbSuspendControl = 0x54,
            UsbInterruptStatus = 0x55,
            UsbLineInterruptStatus = 0x56,
            UsbLineInterruptEnable = 0x57,
            UsbHardwareEnableActivity = 0x58,
            UsbHardwareEnableActivityEnable = 0x59,
            ViewScreenDma = 0x5A,
            UsbCoreInterruptEnable = 0x5B,
            UsbFunctionAddress = 0x80,
            UsbPower = 0x81,
            UsbTxInterrupt = 0x82,
            UsbTxInterruptCont = 0x83,
            UsbRxInterrupt = 0x84,
            UsbRxInterruptCont = 0x85,
            UsbOtherInterruptId = 0x86,
            UsbTxInterruptMask = 0x87,
            UsbTxInterruptMaskCont = 0x88,
            UsbRxInterruptMask = 0x89,
            UsbRxInterruptMaskCont = 0x8A,
            UsbOtherInterruptMask = 0x8B,
            UsbFrame = 0x8C,
            UsbFrameCont = 0x8D,
            UsbIndex = 0x8E,
            UsbDeviceControl = 0x8F,
            UsbTxMaxPacketSize = 0x90,
            UsbTxCsrControlPipeCsr = 0x91,
            UsbTxCsrControlPipeCsrCont = 0x92,
            UsbRxMaxPacketSize = 0x93,
            UsbRxCsr = 0x94,
            UsbRxCsrCont = 0x95,
            UsbRxCountControlPipeCount = 0x96,
            UsbRxCountControlPipeCountCont = 0x97,
            UsbTxType = 0x98,
            UsbTxIntervalUsbNakLimit0 = 0x99,
            UsbRxType = 0x9A,
            UsbRxInterval = 0x9B,
            UsbPipe0 = 0xA0,
            UsbPipe1 = 0xA1,
            UsbPipe2 = 0xA2,
            UsbPipe3 = 0xA3,
            UsbPipe4 = 0xA4,
            UsbPipe5 = 0xA5,
            UsbPipe6 = 0xA6,
            UsbPipe7 = 0xA7,
            UsbPipe8 = 0xA8,
            UsbPipe9 = 0xA9,
            UsbPipeA = 0xAA,
            UsbPipeB = 0xAB,
            UsbPipeC = 0xAC,
            UsbPipeD = 0xAD,
            UsbPipeE = 0xAE,
            UsbPipeF = 0xAF,
        }

        public byte GpioDirection = 0;
        public byte GpioData = 0;

        public override void WritePort(object sender, ushort address, byte value)
        {
            address = (ushort)(address & 0xFF);
            if (TraceIo && sender is Z80Core.Z80Cpu && (address & 0xFE) != 0x10)
            {
                IoLogData[IoLogPtr, 0] = (byte)(address & 0xFF);
                IoLogData[IoLogPtr, 1] = value;
                IoLogData[IoLogPtr, 2] = 1;
                IoLogPtr = (IoLogPtr + 1) & IoLogMask;
            }
            switch (address)
            {
                case 0x00: // D-Bus
                    DBus.WriteLines(value);
                    break;
                case 0x01: // Keypad
                    Keypad.CurrentGroup = value;
                    break;
                case 0x02: // Interrupt ACK
                    ulong old = (ulong)(Interrupts & (InterruptId.OnKey | InterruptId.ApdTimer1 | InterruptId.ApdTimer2 | InterruptId.DBus)) & value;
                    Interrupts = (Interrupts & ~(InterruptId.OnKey | InterruptId.ApdTimer1 | InterruptId.ApdTimer2 | InterruptId.DBus)) | (InterruptId)old;
                    break;
                case 0x03: // TODO: Interrupt mask
                    Keypad.OnInterruptEnable = (value & 1) != 0;
                    Apd.GenerateInterrupt = (value & 2) != 0;
                    PApd.GenerateInterrupt = (value & 4) != 0;
                    OffEnableMode = (value & 8) != 0;
                    // TODO: DBus interrupt
                    break;
                case 0x04: // Memory mapping, timer frequencies, battery cutoff voltage
                    switch (value & 6)
                    {
                        case 0:
                            Apd.Period = 1.0 / (32768 / 64);
                            PApd.Period = 1.0 / (32768 / 32);
                            break;
                        case 2:
                            Apd.Period = 1.0 / (32768 / 144);
                            PApd.Period = 1.0 / (32768 / 72);
                            break;
                        case 4:
                            Apd.Period = 1.0 / (32768 / 224);
                            PApd.Period = 1.0 / (32768 / 112);
                            break;
                        case 6:
                            Apd.Period = 1.0 / (32768 / 304);
                            PApd.Period = 1.0 / (32768 / 152);
                            break;
                    }
                    TimerSetting = (value >> 1) & 3;
                    // TODO: Battery cutoff voltage
                    Mapper.MemoryMappingMode = value & 1;
                    break;
                case 0x05: // Memory page C
                    Mapper.PageC = value;
                    break;
                case 0x06: // Memory page A lower bits
                    if ((value & 0x80) == 0)
                    {
                        Mapper.PageA = (Mapper.PageA & 0x180) | (value & 0x7F);
                        Mapper.PageAIsRam = false;
                    }
                    else
                    {
                        Mapper.PageA = (Mapper.PageA & 0x180) | (value & 0x0F);
                        Mapper.PageAIsRam = true;
                    }
                    break;
                case 0x07: // Memory page B lower bits
                    if ((value & 0x80) == 0)
                    {
                        Mapper.PageB = (Mapper.PageB & 0x180) | (value & 0x7F);
                        Mapper.PageBIsRam = false;
                    }
                    else
                    {
                        Mapper.PageB = (Mapper.PageB & 0x180) | (value & 0x0F);
                        Mapper.PageBIsRam = true;
                    }
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
                    Mapper.PageA = (Mapper.PageA & 0x7F) | ((value & 3) << 7);
                    break;
                case 0x0F: // Memory Page B upper bits
                    Mapper.PageB = (Mapper.PageB & 0x7F) | ((value & 3) << 7);
                    break;
                case 0x10: // LCD command port
                case 0x12:
                    Lcd.SetCurrentRegister(value);
                    if (Lcd.PanicMode)
                        Cpu.Break = true;
                    break;
                case 0x11: // LCD data port
                case 0x13:
                    Lcd.WriteData(value);
                    if (Lcd.PanicMode)
                        Cpu.Break = true;
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
                    CpuSpeed = value & 3;
                    switch (CpuSpeed)
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
                case 0x39: // GPIO direction
                    GpioDirection = value;
                    break;
                case 0x3A: // GPIO data
                    GpioData = value;
                    break;
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        byte ReadLogPort(object sender, ushort address)
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
                    // TODO: DBus interrupt enable
                    return (byte)((Keypad.OnInterruptEnable ? 1 : 0) | (Apd.GenerateInterrupt ? 2 : 0) | (PApd.GenerateInterrupt ? 4 : 0) | (OffEnableMode ? 8 : 0));// | (DBus.I));
                case 0x04: // Interrupt ID
                    return (byte)((Keypad.OnKey ? 0 : 8) | ((byte)Interrupts & 0xFF));
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
                    return (byte)CpuSpeed;
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
                case 0x39: // GPIO direction
                    return GpioDirection;
                case 0x3A: // GPIO data
                    return GpioData;

                // Dummy USB implementation
                case 0x4A:
                    return 0x24;
                case 0x4C:
                    return 0x22;
                case 0x4D:
                    return 0xA5;
                case 0x55:
                    return 0x1F;
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

        public override byte ReadPort(object sender, ushort address)
        {
            byte val = ReadLogPort(sender, address);
            if (TraceIo && sender is Z80Core.Z80Cpu && (address & 0xFE) != 0x10)
            {
                IoLogData[IoLogPtr, 0] = (byte)(address & 0xFF);
                IoLogData[IoLogPtr, 1] = val;
                IoLogData[IoLogPtr, 2] = 0;
                IoLogPtr = (IoLogPtr + 1) & IoLogMask;
            }
            return val;
        }
    }
}
