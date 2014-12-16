﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public abstract class Ram : Memory
    {
        public byte[] Data;

        public override byte ReadByte(object sender, int address)
        {
            // TODO: Optimize this
            return Data[address % Data.Length];
        }

        public override void WriteByte(object sender, int address, byte value)
        {
            Data[address % Data.Length] = value;
        }


    }
}
