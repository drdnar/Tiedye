using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public abstract class DBus
    {
        public virtual void Reset()
        {
            HasInterrupt = false;
        }

        public bool HasInterrupt = false;

        public abstract void WriteLines(byte value);

        public abstract byte ReadLines();
    }
}
