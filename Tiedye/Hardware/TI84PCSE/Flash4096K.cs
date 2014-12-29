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

        public override void WriteByte(object sender, int address, byte value)
        {
            Data[address] = value;
        }

        
    }
}
