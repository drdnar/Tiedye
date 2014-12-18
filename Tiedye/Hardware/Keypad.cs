using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    /// <summary>
    /// Calculator keypad, for TI-83 Plus, TI-83 Plus SE, TI-84 Plus, TI-84 Plus SE, and TI-84 Plus C SE.
    /// TODO: Implement key ghosting for multiple key presses.
    /// </summary>
    public class Keypad
    {
        public Calculator Master;

        /// <summary>
        /// Probably what the name implies it is.
        /// </summary>
        public int KeysDownCount;

        /// <summary>
        /// Currently selected read mask.
        /// </summary>
        public byte CurrentGroup;

        /// <summary>
        /// Table of keys held down.
        /// </summary>
        public byte[] KeyStateTable;
        
        /// <summary>
        /// True if the ON key is being held down.
        /// </summary>
        protected bool onKey;

        /// <summary>
        /// Controls the state of the ON key.
        /// This fires an interrupt on the transistion between false and true.
        /// </summary>
        public bool OnKey
        {
            get
            {
                return onKey;
            }
            set
            {
                if (onKey && value)
                    return;
                onKey = value;
                if (onKey)
                    HasInterrupt = true;
            }
        }

        protected bool onInterruptEnable = false;
        public bool OnInterruptEnable
        {
            get
            {
                return onInterruptEnable;
            }
            set
            {
                onInterruptEnable = value;
                if (!value)
                {
                    HasInterrupt = false;
                }
            }
        }

        /// <summary>
        /// True if pressing the ON key has caused an interrupt which has not
        /// been ACKed.
        /// </summary>
        public bool HasInterrupt
        {
            get
            {
                return Master.GetInterrupt(Calculator.InterruptId.OnKey);
            }
            set
            {
                Master.SetInterrupt(Calculator.InterruptId.OnKey, value);
            }
        }

        public Keypad(Calculator master)
        {
            Master = master;
            Reset();
        }

        public void Reset()
        {
            // Release all keys
            KeyStateTable = new byte[8];
            KeysDownCount = 0;
            CurrentGroup = 0xFF;
            HasInterrupt = false;
            onKey = false;
        }

        /// <summary>
        /// Performs a read.
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public byte ReadGroup(byte mask)
        {
            byte returnValue = 0;
            if ((mask & 1) == 0)
                returnValue = (byte)(KeyStateTable[0]);
            if ((mask & 2) == 0)
                returnValue = (byte)(KeyStateTable[1] | returnValue);
            if ((mask & 4) == 0)
                returnValue = (byte)(KeyStateTable[2] | returnValue);
            if ((mask & 8) == 0)
                returnValue = (byte)(KeyStateTable[3] | returnValue);
            if ((mask & 16) == 0)
                returnValue = (byte)(KeyStateTable[4] | returnValue);
            if ((mask & 32) == 0)
                returnValue = (byte)(KeyStateTable[5] | returnValue);
            if ((mask & 64) == 0)
                returnValue = (byte)(KeyStateTable[6] | returnValue);
            if ((mask & 128) == 0)
                returnValue = (byte)(KeyStateTable[7] | returnValue);
            return (byte)(~returnValue);
        }

        /// <summary>
        /// Returns the value the CPU will see if it reads port 1.
        /// </summary>
        /// <returns>Return value uses the group specified by CurrentGroup
        /// </returns>
        public byte Read()
        {
            return ReadGroup(CurrentGroup);
        }

        /// <summary>
        /// Signals that the given key in the given group is being held down.
        /// A number of 0 indicates that bit 0 in the byte should be reset.
        /// For example, group=0 will mean you're talking about group 0xFE.
        /// </summary>
        /// <param name="group">A number 0-7</param>
        /// <param name="bit">A number 0-7</param>
        public void SetKey(int group, int bit)
        {
            KeyStateTable[group] = (byte)(KeyStateTable[group] | (1 << bit));
            KeysDownCount++;
        }

        /// <summary>
        /// Signals that the given key in the given group is not being held down.
        /// A number of 0 indicates that bit 0 in the byte should be reset.
        /// For example, group=0 will mean you're talking about group 0xFE.
        /// </summary>
        /// <param name="group">A number 0-7</param>
        /// <param name="bit">A number 0-7</param>
        public void ResetKey(int group, int bit)
        {
            KeyStateTable[group] = (byte)(KeyStateTable[group] & (~(1 << bit)));
            KeysDownCount--;
        }

        /// <summary>
        /// Signals that the given key is being held down.
        /// </summary>
        /// <param name="key"></param>
        public void SetKey(KeyScanCode key)
        {
            if (key == KeyScanCode.None)
                return;
            if (key == KeyScanCode.On)
            {
                onKey = true;
                return;
            }
            int n = (int)key - 1;
            SetKey(n >> 3, n & 0x7);
        }

        /// <summary>
        /// Signals that the given key is not being held down.
        /// </summary>
        /// <param name="key"></param>
        public void ResetKey(KeyScanCode key)
        {
            if (key == KeyScanCode.None)
                return;
            if (key == KeyScanCode.On)
            {
                onKey = false;
                return;
            }
            int n = (int)key - 1;
            ResetKey(n >> 3, n & 0x7);
        }

        /// <summary>
        /// Returns true if the specified key is being held down.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TestKey(KeyScanCode key)
        {
            if (key == KeyScanCode.None)
                return false;
            if (key == KeyScanCode.On)
                return onKey;
            int n = (int)key - 1;
            int group = n >> 3;
            int bit = n & 0x7;
            return (KeyStateTable[group] & (1 << bit)) != 0;
        }

        /// <summary>
        /// This provides a simple way to control which keys are being pressed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool this[KeyScanCode key]
        {
            get
            {
                return TestKey(key);
            }
            set
            {
                if (value)
                    SetKey(key);
                else
                    ResetKey(key);
            }
        }

        /// <summary>
        /// The values in this enum directly translate to the values from
        /// _GetCSC, except for the On key.  You can easily test that by
        /// looking at bit 6.
        /// </summary>
        public enum KeyScanCode
        {
            /// <summary>
            /// The OS _GetCSC returns 0 if no key is pressed.
            /// 
            /// </summary>
            None = 0,
            Down = 0x01,
            Left = 0x02,
            Right = 0x03,
            Up = 0x04,
            Enter = 0x09,
            /// <summary>
            /// + key
            /// </summary>
            Add = 0x0A,
            /// <summary>
            /// - key
            /// </summary>
            Sub = 0x0B,
            /// <summary>
            /// × key, shows up as *
            /// </summary>
            Mul = 0x0C,
            /// <summary>
            /// ÷ key, shows up as /
            /// </summary>
            Div = 0x0D,
            /// <summary>
            /// ^ key
            /// </summary>
            Power = 0x0E,
            Clear = 0x0F,
            /// <summary>
            /// (-) key, the one by .
            /// </summary>
            ChangeSign = 0x11,
            N3 = 0x12,
            N6 = 0x13,
            N9 = 0x14,
            /// <summary>
            /// ) key
            /// </summary>
            RParen = 0x15,
            Tan = 0x16,
            Vars = 0x17,
            /// <summary>
            /// . (period or decimal point)
            /// </summary>
            DecPnt = 0x19,
            N2 = 0x1A,
            N5 = 0x1B,
            N8 = 0x1C,
            /// <summary>
            /// ( key
            /// </summary>
            LParen = 0x1D,
            Cos = 0x1E,
            Prgm = 0x1F,
            Stat = 0x20,
            N0 = 0x21,
            N1 = 0x22,
            N4 = 0x23,
            N7 = 0x24,
            /// <summary>
            /// , key
            /// </summary>
            Comma = 0x25,
            Sin = 0x26,
            /// <summary>
            /// xTθn key
            /// </summary>
            GraphVar = 0x28,
            /// <summary>
            /// TI's code likes to refer to the Apps key as Matrix because
            /// that's what it was on the TI-83 non-plus.  So the Apps enum
            /// value has the same code.
            /// </summary>
            Matrix = 0x27,
            /// <summary>
            /// Same as Matrix due to the legacy of the TI-83 non-plus.
            /// </summary>
            Apps = 0x27,
            /// <summary>
            /// -> symbol
            /// </summary>
            Store = 0x2A,
            Ln = 0x2B,
            Log = 0x2C,
            /// <summary>
            /// x^2 key
            /// </summary>
            Square = 0x2D,
            /// <summary>
            /// x^-1 key
            /// </summary>
            Recip = 0x2E,
            Math = 0x2F,
            Alpha = 0x30,
            Graph = 0x31,
            Trace = 0x32,
            Zoom = 0x33,
            Window = 0x34,
            YEqu = 0x35,
            /// <summary>
            /// 2nd key, named Second because symbol names cannot start with a number
            /// </summary>
            Second = 0x36,
            /// <summary>
            /// 2nd key, same as the enum value Second
            /// </summary>
            _2nd = 0x36,
            Mode = 0x37,
            Del = 0x38,
            On = 0x40,
        }

    }
}
