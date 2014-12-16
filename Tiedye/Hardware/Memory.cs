using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    /// <summary>
    /// Represents a generic memory interface.
    /// </summary>
    public abstract class Memory
    {
        public abstract void Reset();

        /// <summary>
        /// Reads a byte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public abstract byte ReadByte(object sender, int address);

        /// <summary>
        /// Attempts a write operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        public abstract void WriteByte(object sender, int address, byte value);

    }
}
