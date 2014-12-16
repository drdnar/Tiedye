using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public abstract class Lcd
    {
        public abstract int Width
        {
            get;
        }

        public abstract int Height
        {
            get;
        }

        public abstract int this[int x, int y]
        {
            get;
            set;
        }
    }
}
