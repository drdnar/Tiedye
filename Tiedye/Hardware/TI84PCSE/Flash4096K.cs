using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class Flash4096K : Flash
    {
        public Flash4096K()
        {
            SizeMask = 0x3FFFFF;
            Data = new byte[4194304];
            BootSector = 0x3FC000;
        }

        public override void Reset()
        {
            // Do nothing
        }

        public override byte ReadByte(object sender, int address)
        {
            return base.ReadByte(sender, address);
        }

        public override int GetSectorStart(int address)
        {
            if (address < 0x3F0000)
                return address & 0xFF0000;
            else if (address < 0x3F8000)
                return 0x3F0000;
            else if (address < 0x3FA000)
                return 0x3F8000;
            else if (address < 0x3FC000)
                return 0x3FA000;
            else
                return 0x3FC000;
        }

        public override int GetSectorLength(int address)
        {
            if (address < 0x3F0000)
                return 0x10000;
            else if (address < 0x3F8000)
                return 0x8000;
            else if (address < 0x3FC000)
                return 0x2000;
            else
                return 0x4000;
        }
    }
}
