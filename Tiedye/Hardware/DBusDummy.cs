using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    /// <summary>
    /// This is a dummy class that implements no actual linking.
    /// </summary>
    public class DBusDummy : DBus
    {
        protected int LastWrite = 0;
        protected int LinesState = 0x3;
        public override void Reset()
        {
            LastWrite = 0;
            LinesState = 0x3;
        }

        public override void WriteLines(byte value)
        {
            LastWrite = value & 0x3;
            LinesState = LastWrite;
        }

        public override byte ReadLines()
        {
            return (byte)(LinesState | (LastWrite << 4));
        }


    }
}
