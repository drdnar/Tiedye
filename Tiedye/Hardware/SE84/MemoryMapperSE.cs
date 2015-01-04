using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiedye.Z80Core;

namespace Tiedye.Hardware
{
    public class MemoryMapperSE : MemoryMapper
    {
        public Calculator Master;

        /// <summary>
        /// If true, flash is unlocked.
        /// </summary>
        public bool FlashWriteEnable = true;


        #region Flash & RAM Type
        private int ramType = 3;
        private int ramTypeMask = 0x3FFFF;
        /// <summary>
        /// Informs the memory mapper what type of RAM chip is installed.
        /// This does NOT control the actual size of the RAM chip.
        /// It ONLY controls how the memory mapper interacts with the RAM.
        /// </summary>
        public int RamType
        {
            get
            {
                return ramType;
            }
            set
            {
                if (value > 3 || value < 0)
                    throw new ArgumentOutOfRangeException();
                ramType = value;
                ramTypeMask = (1 << (15 + value)) - 1;
            }
        }

        private int flashType = 3;
        private int flashTypeMask = 0x7FFFFF;
        /// <summary>
        /// Informs the memory mapper what type of flash chip is installed.
        /// This does NOT control the actual size of the flash chip.
        /// It ONLY controls how the memory mapper interacts with flash.
        /// </summary>
        public int FlashType
        {
            get
            {
                return flashType;
            }
            set
            {
                if (value > 3 || value < 0)
                    throw new ArgumentOutOfRangeException();
                flashType = value;
                flashTypeMask = (1 << (20 + value)) - 1;
            }
        }
        #endregion


        #region Execution Limits
        private int ramUpperLimit = 1024;
        /// <summary>
        /// Highested RAM address, divided by 1024, that can execute code.
        /// </summary>
        public int RamUpperLimit
        {
            get
            {
                unchecked
                {
                    return (byte)((ramUpperLimit - 1024) >> 10);
                }
            }
            set
            {
                ramUpperLimit = (value << 10) + 1024;
            }
        }

        private int ramLowerLimit = 0;
        /// <summary>
        /// Lowest RAM address, divided by 1024, that can execute code.
        /// </summary>
        public int RamLowerLimit
        {
            get
            {
                unchecked
                {
                    return (byte)(ramLowerLimit >> 10);
                }
            }
            set
            {
                ramLowerLimit = value << 10;
            }
        }

        private int flashUpperLimit = 0;
        /// <summary>
        /// Flash addresses above this can execute code.
        /// This value is in pages.
        /// </summary>
        public int FlashUpperLimit
        {
            get
            {
                return flashUpperLimit >> 14;
            }
            set
            {
                flashUpperLimit = value << 14;
            }
        }

        private int flashLowerLimit = 0;
        /// <summary>
        /// Flash addresses below this can execute code.
        /// This value is in pages.
        /// </summary>
        public int FlashLowerLimit
        {
            get
            {
                return flashLowerLimit >> 14;
            }
            set
            {
                flashLowerLimit = value << 14;
            }
        }
        #endregion


        #region Paging
        private int memoryMapMode = 0;
        public int MemoryMappingMode
        {
            get
            {
                return memoryMapMode;
            }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException();
                if (memoryMapMode == value)
                    return; // No change, so do nothing
                memoryMapMode = value;
                PageA = pageA;
                PageAIsRam = pageAIsRam;
                PageB = pageB;
                PageBIsRam = pageBIsRam;
                PageC = pageC;
            }
        }

        private int pageA = 0;
        /// <summary>
        /// Controls memory page A.
        /// </summary>
        public int PageA
        {
            get
            {
                return pageA;
            }
            set
            {
                pageA = value;
                if (memoryMapMode == 0)
                {
                    Bank4000 = value;
                }
                else
                {
                    Bank4000 = value & 0x1FE;
                    Bank8000 = value | 1;
                }
            }
        }
        private bool pageAIsRam = false;
        /// <summary>
        /// Set to true to make page A map to RAM.
        /// </summary>
        public bool PageAIsRam
        {
            get
            {
                return pageAIsRam;
            }
            set
            {
                if (memoryMapMode == 0)
                {
                    Bank4000IsRam = pageAIsRam = value;
                    Bank4000 = pageA;
                }
                else
                {
                    Bank4000IsRam = Bank8000IsRam = pageAIsRam = value;
                    Bank4000 = pageA & 0x1FE;
                    Bank8000 = pageA | 1;
                }
            }
        }

        private int pageB = 0;
        /// <summary>
        /// Controls memory page B.
        /// </summary>
        public int PageB
        {
            get
            {
                return pageB;
            }
            set
            {
                pageB = value;
                if (memoryMapMode == 0)
                    Bank8000 = value;
                else
                    BankC000 = value;
            }
        }
        private bool pageBIsRam = false;
        /// <summary>
        /// Set to true to make page B map to RAM.
        /// </summary>
        public bool PageBIsRam
        {
            get
            {
                return pageBIsRam;
            }
            set
            {
                if (memoryMapMode == 0)
                {
                    Bank8000IsRam = pageBIsRam = value;
                    Bank8000 = pageB;
                }
                else
                {
                    BankC000IsRam = pageBIsRam = value;
                    BankC000 = pageB;
                }
            }
        }

        private int pageC = 0;
        /// <summary>
        /// Controls memory page C.
        /// </summary>
        public int PageC
        {
            get
            {
                return pageC;
            }
            set
            {
                pageC = value;
                if (memoryMapMode == 0)
                {
                    BankC000IsRam = true;
                    BankC000 = value;
                }
                // Otherwise, this value has no effect, other than to be stored.
            }
        }
        #endregion


        #region Partial Page Mapping

        private int upperPageAlawayPresentLimit = 0x10000;
        /// <summary>
        /// Basically, port 27.
        /// </summary>
        public int UpperPageAlwaysPresentAmount
        {
            get
            {
                return (byte)(0x10000 - (upperPageAlawayPresentLimit >> 6));
            }
            set
            {
                upperPageAlawayPresentLimit = 0x10000 - (value << 6);
            }
        }

        private int lowerPageAlwaysPresentLimit = 0x8000;
        /// <summary>
        /// Basically, port 28.
        /// </summary>
        public int LowerPageAlwaysPresentAmount
        {
            get
            {
                return (byte)((lowerPageAlwaysPresentLimit - 0x8000) >> 6);
            }
            set
            {
                lowerPageAlwaysPresentLimit = (value << 6) + 0x8000;
            }
        }

        #endregion


        public override void Reset()
        {
            base.Reset();
            FlashWriteEnable = true;
            RamType = 3;
            FlashType = 3;
            ramUpperLimit = 0;
            ramLowerLimit = 0;
            flashUpperLimit = 0;
            flashLowerLimit = 0;
            memoryMapMode = 0;
            pageA = 0;
            pageAIsRam = false;
            pageB = 0;
            pageBIsRam = false;
            pageC = 0;
            upperPageAlawayPresentLimit = 0x10000;
            lowerPageAlwaysPresentLimit = 0x8000;
        }
        /*
         * 20.02

*/


        // Value    Boot    Cert.   Privileged      Immutable
        // 0        3F      3E      2C-2F, 3C-3F    2C-2F, 3F
        // 1        7F      7E      6C-6F, 7C-7F    6C-6F, 7F
        // 2        FF      FE      EC-EF, FC-FF    EC-EF, FF

        public override byte BusRead(object sender, ushort address)
        {
            int linearAddress = 0;
            bool isRam = false;
            // Compute linear address in RAM or flash
            switch (address & 0xC000)
            {
                case 0x0000:
                    /*
                    if (BootMode)
                        return Flash.ReadByte(sender, address | 0xFFC000);
                    else
                        return Flash.ReadByte(sender, address);*/
                    isRam = false;
                    if (BootMode)
                        linearAddress = address | Flash.BootSector;
                    else
                        linearAddress = address;
                    break;
                case 0x4000:
                    BootMode = false;
                    linearAddress = (address & 0x3FFF) | bank4000;
                    isRam = Bank4000IsRam;
                    break;
                case 0x8000:
                    if (address < lowerPageAlwaysPresentLimit && memoryMapMode == 0)
                    {
                        isRam = true;
                        linearAddress = (address & 0x3FFF) | 0x4000;
                        break;
                    }
                    if (memoryMapMode == 1)
                        BootMode = false;
                    linearAddress = (address & 0x3FFF) | bank8000;
                    isRam = Bank8000IsRam;
                    break;
                case 0xC000:
                    if (address >= upperPageAlawayPresentLimit && memoryMapMode == 0)
                    {
                        isRam = true;
                        linearAddress = (address & 0x3FFF);
                        break;
                    }
                    linearAddress = (address & 0x3FFF) | bankC000;
                    isRam = BankC000IsRam;
                    break;
            }
            Breakpoint bp = new Breakpoint()
                { Address = (ushort)(linearAddress & 0x3FFF),
                    IsRam = isRam,
                    Page = linearAddress >> 14,
                    Type = Cpu.M1 ? MemoryBreakpointType.Execution : MemoryBreakpointType.Read
                };
            if (Cpu.M1 && IsExecutionBreakpoint(bp))
                Cpu.BreakExecution();
            else if (haveReadBps && IsReadBreakpoint(bp))
                Cpu.BreakExecution();
            if (isRam)
            {
                // Check execution permissions: address must be within allowed limits, masked by RAM chip size
                if (Cpu.M1 && sender is Z80Cpu)
                    if ((linearAddress & ramTypeMask) < ramLowerLimit || (linearAddress & ramTypeMask) >= ramUpperLimit)
                        Master.Reset();
                return Ram.ReadByte(sender, linearAddress & 0x3FFFF);
            }
            else
            {
                // Check execution permissions: address must be within allowed limits
                
                if (Cpu.M1 && sender is Z80Cpu)
                    if ((linearAddress & flashTypeMask) >= flashLowerLimit && (linearAddress & flashTypeMask) < flashUpperLimit)
                        // . . . unless it's on a privileged page, in which case, execution is always allowed
                        if (!((((linearAddress >> 14) & 0xF) >= 0xC) && (((linearAddress >> 18) & 0x2) >= 2) && ((linearAddress >> 20) == (1 << flashType) - 1)))
                            Master.Reset();
                // Check for censorship
                int highBits = ((1 << flashType) - 1) << 20;
                if ((linearAddress & 0xF00000) == highBits && !FlashWriteEnable)
                {
                    highBits = linearAddress & 0xFFFFF;
                    if (highBits >= 0xF8000 && highBits < 0xFC000)
                        return 0xFF;
                }
                return Flash.ReadByte(sender, linearAddress);
            }
        }


        public override void BusWrite(object sender, ushort address, byte value)
        {
            int linearAddress = 0;
            bool isRam = false;
            switch (address & 0xC000)
            {
                case 0x0000:
                    if (BootMode)
                        return;
                    linearAddress = address & 0x3FFF;
                    isRam = false;
                    break;
                case 0x4000:
                    linearAddress = (address & 0x3FFF) | bank4000;
                    isRam = Bank4000IsRam;
                    break;
                case 0x8000:
                    if (address < lowerPageAlwaysPresentLimit && memoryMapMode == 0)
                    {
                        isRam = true;
                        linearAddress = (address & 0x3FFF) | 0x4000;
                        break;
                    }
                    linearAddress = (address & 0x3FFF) | bank8000;
                    isRam = Bank8000IsRam;
                    break;
                case 0xC000:
                    if (address >= upperPageAlawayPresentLimit && memoryMapMode == 0)
                    {
                        isRam = true;
                        linearAddress = (address & 0x3FFF);
                        break;
                    }
                    linearAddress = (address & 0x3FFF) | bankC000;
                    isRam = BankC000IsRam;
                    break;
            }
            Breakpoint bp = new Breakpoint()
            {
                Address = (ushort)(linearAddress & 0x3FFF),
                IsRam = isRam,
                Page = linearAddress >> 14,
                Type = MemoryBreakpointType.Write
            };
            if (haveWriteBps && IsWriteBreakpoint(bp))
                Cpu.BreakExecution();
            if (isRam)
                Ram.WriteByte(sender, linearAddress & 0x3FFFF, value);
            else
            {
                if (!FlashWriteEnable)
                    return;
                int highBits = ((1 << flashType) - 1) << 20;
                if ((linearAddress & 0xF00000) == highBits)
                {
                    highBits = linearAddress & 0xFFFFF;
                    if (highBits >= 0xB0000 && highBits < 0xC000 || highBits >= 0xFC000)
                        return;
                }
                Flash.WriteByte(sender, linearAddress, value);
            }
        }

    }
}

