using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class Ram128K : Ram
    {
        public Ram128K()
        {
            Data = new byte[131072];
        }
        public override void Reset()
        {
            // Do nothing
        }

    }
}
