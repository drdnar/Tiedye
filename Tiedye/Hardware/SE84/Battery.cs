using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    /// <summary>
    /// TODO: Actually make the emualted calculator reference the battery voltage.
    /// </summary>
    public class Battery
    {
        public double Voltage0 = 5.0;
        public double Voltage1 = 5.0;
        public double Voltage2 = 5.0;
        public double Voltage3 = 5.5;
        public double VoltageNormal = 5.5;
        public double CurrentVoltage = 6.0;

        public bool IsBatteryGood
        {
            get
            {
                return CurrentVoltage <= VoltageNormal;
            }
        }

        public bool IsBatteryAbsent
        {
            get
            {
                return CurrentVoltage < 1.0;
            }
        }

        public bool IsBatteryVoltageGreaterThan(int x)
        {
            switch (x)
            {
                case 0:
                    if (CurrentVoltage >= Voltage0)
                        return true;
                    return false;
                case 1:
                    if (CurrentVoltage >= Voltage1)
                        return true;
                    return false;
                case 2:
                    if (CurrentVoltage >= Voltage2)
                        return true;
                    return false;
                case 3:
                    if (CurrentVoltage >= Voltage3)
                        return true;
                    return false;
            }
            return false;
        }
    }
}
