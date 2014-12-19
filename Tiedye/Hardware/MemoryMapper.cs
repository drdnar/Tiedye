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
                if (value > 0x1FF)
                    bank4000 = value << 14;
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
            }
        }

        public bool BankC000IsRam = true;

        public abstract byte BusRead(object sender, ushort address);

        public abstract void BusWrite(object sender, ushort address, byte value);
        

    }
}
