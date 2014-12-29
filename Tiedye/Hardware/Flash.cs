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

        public override byte ReadByte(object sender, int address)
        {
            // TODO: Optimize this
            return Data[address % Data.Length];
        }

        public virtual void WriteByteForced(object sender, int address, byte value)
        {
            Data[address] = value;
        }
    }
}
