using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tiedye.Hardware;
using ScanCode = Tiedye.Hardware.Keypad.KeyScanCode;

namespace TiedyeDesktop
{
    public partial class Keypad : UserControl
    {
        public Keypad()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, true);
            lastKeyTimer.Tick += new EventHandler(KeyTimerTick);
            lastKeyTimer.Interval = 50;
        }

        

        public event EventHandler<KeyLocation> KeyPressed;
        public event EventHandler<KeyLocation> KeyReleased;
        
        public struct KeyLocation
        {
            public int Group;
            public int Key;
            public bool On;
            public Tiedye.Hardware.Keypad.KeyScanCode KeyCode;
            public KeyLocation(int Group = 0, int Key = 0, bool On = false)
            {
                this.Group = Group;
                this.Key = Key;
                this.On = On;
                KeyCode = ScanCode.None;
            }
            public KeyLocation(Tiedye.Hardware.Keypad.KeyScanCode key)
            {
                this.Group = 0;
                this.Key = 0;
                this.On = false;
                this.KeyCode = key;
            }

        }

        private Timer lastKeyTimer = new Timer();
        private KeyLocation LastKey = default(KeyLocation);
        private bool lastKeyMouseReleased = false;

        public string KeyLog = "";

        /*protected void Log(string text)
        {
            KeyLog = text + " | " + Focused.ToString() + "\r\n" + KeyLog;
        }*/

        private void Keypad_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventArgs f = e as MouseEventArgs;
            if (f == null)
                return;
            Bitmap b = keyIdsPicture.Image as Bitmap;
            // Above should never fail. If it does, this is situation worth crashing for anyway.
            /*if (b == null)
                return;*/
            //MessageBox.Show("" + f.X + ", " + f.Y + ": " + b.GetPixel(f.X, f.Y));
            Color c = b.GetPixel(f.X, f.Y);
            if (c.R != 16 && c.R != 32)
                return;
            int group = c.G >> 5;
            int key = c.B >> 5;
            bool on = c.R == 32;
            KeyLocation data = new KeyLocation(Group: group, Key: key, On: on);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //Log("Click " + Convert.ToString(group, 2) + " " + Convert.ToString(key, 2) + on);
                if (lastKeyTimer.Enabled)
                {
                    // What a fast clicker!
                    // Release last key.
                    lastKeyTimer.Stop();
                    if (KeyReleased != null)
                    {
                        //Log(". . . released. (fast click)");
                        KeyReleased(this, LastKey);
                    }
                }
                LastKey = data;
                lastKeyMouseReleased = false;
                lastKeyTimer.Stop();
                lastKeyTimer.Start();
                if (KeyPressed != null)
                {
                    //Log(". . . pressed.");
                    KeyPressed(this, data);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // TODO: Support key locking
            }
        }

        private void KeyTimerTick(object sender, EventArgs e)
        {
            Focus();
            lastKeyTimer.Stop();
            if (!lastKeyMouseReleased)
                return;
            if (KeyReleased != null)
            {
                //Log(". . . released. (timer)");
                KeyReleased(this, LastKey);
            }
        }


        private void Keypad_MouseUp(object sender, MouseEventArgs e)
        {
            if (lastKeyTimer.Enabled)
            {
                lastKeyMouseReleased = true;
                return;
            }
            lastKeyMouseReleased = true;
            if (KeyReleased != null)
            {
                //Log(". . . released. (mouse)");
                KeyReleased(this, LastKey);
            }
        }


        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData) //& ~Keys.Shift & ~Keys.Control & ~Keys.Alt)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Enter:
                case Keys.Escape:
                case Keys.Insert:
                case Keys.Decimal:
                case Keys.Home:
                case Keys.End:
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F10:
                case Keys.F12:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.Shift:
                case Keys.ShiftKey:
                case Keys.Control:
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private ScanCode TranslateKeyCode(KeyEventArgs e)
        {
            
            return TranslateKeyCode(e.KeyCode);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            if ((m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP) && ((int)m.WParam == VK_CONTROL || (int)m.WParam == VK_SHIFT))
            {
                KeyEventArgs e = new KeyEventArgs(Keys.None);
                switch ((OemScanCode)(((int)m.LParam >> 16) & 0x1FF))
                {
                    case OemScanCode.LControl:
                        e = new KeyEventArgs(Keys.LControlKey);
                        break;
                    case OemScanCode.RControl:
                        e = new KeyEventArgs(Keys.RControlKey);
                        break;
                    case OemScanCode.LShift:
                        e = new KeyEventArgs(Keys.LShiftKey);
                        break;
                    case OemScanCode.RShift:
                        e = new KeyEventArgs(Keys.RShiftKey);
                        break;
                    default:
                        if ((int)m.WParam == VK_SHIFT)
                            e = new KeyEventArgs(Keys.ShiftKey);
                        else if ((int)m.WParam == VK_CONTROL)
                            e = new KeyEventArgs(Keys.ControlKey);
                        break;
                }
                if (e.KeyData != Keys.None)
                {
                    if (m.Msg == WM_KEYDOWN)
                        OnKeyDown(e);
                    else
                        OnKeyUp(e);
                    return true;
                }
            }
            return base.ProcessKeyMessage(ref m);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //Log("RAW press: " + e.KeyCode.ToString());
            ScanCode x = TranslateKeyCode(e);
            if (x == ScanCode.None)
            {
                base.OnKeyDown(e);
                return;
            }
            //Log("Press " + x.ToString());
            KeyPressed(this, new KeyLocation(x));
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            //Log("RAW release: " + e.KeyCode.ToString());
            ScanCode x = TranslateKeyCode(e);
            if (x == ScanCode.None)
            {
                base.OnKeyUp(e);
                return;
            }
            //Log("Release " + x.ToString());
            KeyReleased(this, new KeyLocation(x));
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Focus();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Focus();
        }

        protected ScanCode TranslateKeyCode(Keys k)
        {
            switch (k)
            {
                case Keys.F1:
                    return ScanCode.YEqu;
                case Keys.F2:
                    return ScanCode.Window;
                case Keys.F3:
                    return ScanCode.Zoom;
                case Keys.F4:
                    return ScanCode.Trace;
                case Keys.F5:
                    return ScanCode.Graph;
                case Keys.LShiftKey:
                case Keys.ShiftKey:
                    return ScanCode.Second;
                case Keys.Escape:
                    return ScanCode.Mode;
                case Keys.Delete:
                    return ScanCode.Del;
                case Keys.Left:
                    return ScanCode.Left;
                case Keys.Right:
                    return ScanCode.Right;
                case Keys.Up:
                    return ScanCode.Up;
                case Keys.Down:
                    return ScanCode.Down;
                case Keys.RControlKey:
                case Keys.LControlKey:
                case Keys.ControlKey:
                    return ScanCode.Alpha;
                case Keys.Oemplus:
                    return ScanCode.GraphVar;
                case Keys.End:
                    return ScanCode.Stat;
                case Keys.A:
                    return ScanCode.Math;
                case Keys.B:
                    return ScanCode.Apps;
                case Keys.C:
                    return ScanCode.Prgm;
                case Keys.Insert:
                    return ScanCode.Vars;
                case Keys.RShiftKey:
                    return ScanCode.Clear;
                case Keys.D:
                    return ScanCode.Recip;
                case Keys.E:
                    return ScanCode.Sin;
                case Keys.F:
                    return ScanCode.Cos;
                case Keys.G:
                    return ScanCode.Tan;
                case Keys.H:
                    return ScanCode.Power;
                case Keys.I:
                    return ScanCode.Square;
                case Keys.J:
                    return ScanCode.Comma;
                case Keys.K:
                    return ScanCode.LParen;
                case Keys.L:
                    return ScanCode.RParen;
                case Keys.M:
                    return ScanCode.Div;
                case Keys.N:
                    return ScanCode.Log;
                case Keys.O:
                    return ScanCode.N7;
                case Keys.P:
                    return ScanCode.N8;
                case Keys.Q:
                    return ScanCode.N9;
                case Keys.R:
                    return ScanCode.Mul;
                case Keys.S:
                    return ScanCode.Ln;
                case Keys.T:
                    return ScanCode.N4;
                case Keys.U:
                    return ScanCode.N5;
                case Keys.V:
                    return ScanCode.N6;
                case Keys.W:
                    return ScanCode.Sub;
                case Keys.X:
                    return ScanCode.Store;
                case Keys.Y:
                    return ScanCode.N1;
                case Keys.Z:
                    return ScanCode.N2;
                case Keys.OemSemicolon:
                    return ScanCode.N3;
                case Keys.OemQuotes:
                    return ScanCode.Add;
                case Keys.Space:
                    return ScanCode.N0;
                case Keys.OemPeriod:
                    return ScanCode.DecPnt;
                case Keys.OemMinus:
                    return ScanCode.ChangeSign;
                case Keys.Enter:
                    return ScanCode.Enter;
                case Keys.F12:
                    return ScanCode.On;
                case Keys.Oemcomma:
                    return ScanCode.Comma;
                case Keys.NumPad0:
                    return ScanCode.N0;
                case Keys.NumPad1:
                    return ScanCode.N1;
                case Keys.NumPad2:
                    return ScanCode.N2;
                case Keys.NumPad3:
                    return ScanCode.N3;
                case Keys.NumPad4:
                    return ScanCode.N4;
                case Keys.NumPad5:
                    return ScanCode.N5;
                case Keys.NumPad6:
                    return ScanCode.N6;
                case Keys.NumPad7:
                    return ScanCode.N7;
                case Keys.NumPad8:
                    return ScanCode.N8;
                case Keys.NumPad9:
                    return ScanCode.N9;
                case Keys.Add:
                    return ScanCode.Add;
                case Keys.Subtract:
                    return ScanCode.Sub;
                case Keys.Multiply:
                    return ScanCode.Mul;
                case Keys.Divide:
                    return ScanCode.Div;
                case Keys.Decimal:
                    return ScanCode.DecPnt;
                case Keys.D1:
                    return ScanCode.N1;
                case Keys.D2:
                    return ScanCode.N2;
                case Keys.D3:
                    return ScanCode.N3;
                case Keys.D4:
                    return ScanCode.N4;
                case Keys.D5:
                    return ScanCode.N5;
                case Keys.D6:
                    return ScanCode.N6;
                case Keys.D7:
                    return ScanCode.N7;
                case Keys.D8:
                    return ScanCode.N8;
                case Keys.D9:
                    return ScanCode.N9;
                case Keys.D0:
                    return ScanCode.N0;
            }
            return ScanCode.None;
        }

        #region Scan code & window message stuff
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        const int VK_SHIFT = 0x10;
        const int VK_CONTROL = 0x11;

        /// <summary>
        /// List of scan codes for standard 104-key keyboard US English keyboard
        /// </summary>
        enum OemScanCode
        {
            /// <summary>
            /// ` ~
            /// </summary>
            BacktickTilde = 0x29,
            /// <summary>
            /// 1 !
            /// </summary>
            N1 = 2,
            /// <summary>
            /// 2 @
            /// </summary>
            N2 = 3,
            /// <summary>
            /// 3 #
            /// </summary>
            N3 = 4,
            /// <summary>
            /// 4 $
            /// </summary>
            N4 = 5,
            /// <summary>
            /// 5 %
            /// </summary>
            N5 = 6,
            /// <summary>
            /// 6 ^
            /// </summary>
            N6 = 7,
            /// <summary>
            /// 7 &
            /// </summary>
            N7 = 8,
            /// <summary>
            /// 8 *
            /// </summary>
            N8 = 9,
            /// <summary>
            /// 9 (
            /// </summary>
            N9 = 0x0A,
            /// <summary>
            /// 0 )
            /// </summary>
            N0 = 0x0B,
            /// <summary>
            /// - _
            /// </summary>
            MinusDash = 0x0C,
            /// <summary>
            /// = +
            /// </summary>
            Equals = 0x0D,
            Backspace = 0x0E,
            Tab = 0x0F,
            Q = 0x10,
            W = 0x11,
            E = 0x12,
            R = 0x13,
            T = 0x14,
            Y = 0x15,
            U = 0x16,
            I = 0x17,
            O = 0x18,
            P = 0x19,
            /// <summary>
            /// [ {
            /// </summary>
            LBracket = 0x1A,
            /// <summary>
            /// ] }
            /// </summary>
            RBracket = 0x1B,
            /// <summary>
            /// | \ (same as pipe)
            /// </summary>
            VerticalBar = 0x2B,
            /// <summary>
            /// | \ (same as vertical bar)
            /// </summary>
            Pipe = 0x2B,
            CapsLock = 0x3A,
            A = 0x1E,
            S = 0x1F,
            D = 0x20,
            F = 0x21,
            G = 0x22,
            H = 0x23,
            J = 0x24,
            K = 0x25,
            L = 0x26,
            /// <summary>
            /// ; :
            /// </summary>
            SemiColon = 0x27,
            /// <summary>
            /// ' "
            /// </summary>
            Quotes = 0x28,
            // Unused
            Enter = 0x1C,
            LShift = 0x2A,
            Z = 0x2C,
            X = 0x2D,
            C = 0x2E,
            V = 0x2F,
            B = 0x30,
            N = 0x31,
            M = 0x32,
            /// <summary>
            /// , <
            /// </summary>
            Comma = 0x33,
            /// <summary>
            /// . >
            /// </summary>
            Period = 0x34,
            /// <summary>
            /// / ?
            /// </summary>
            Slash = 0x35,
            RShift = 0x36,
            LControl = 0x1D,
            LAlternate = 0x38,
            SpaceBar = 0x39,
            RAlternate = 0x138,
            RControl = 0x11D,
            /// <summary>
            /// The menu key thingy
            /// </summary>
            Application = 0x15D,
            Insert = 0x152,
            Delete = 0x153,
            Home = 0x147,
            End = 0x14F,
            PageUp = 0x149,
            PageDown = 0x151,
            UpArrow = 0x148,
            DownArrow = 0x150,
            LeftArrow = 0x14B,
            RightArrow = 0x14D,
            NumLock = 0x145,
            NumPad0 = 0x52,
            NumPad1 = 0x4F,
            NumPad2 = 0x50,
            NumPad3 = 0x51,
            NumPad4 = 0x4B,
            NumPad5 = 0x4C,
            NumPad6 = 0x4D,
            NumPad7 = 0x47,
            NumPad8 = 0x48,
            NumPad9 = 0x49,
            NumPadDecimal = 0x53,
            NumPadEnter = 0x11C,
            NumPadPlus = 0x4E,
            NumPadMinus = 0x4A,
            NumPadAsterisk = 0x37,
            NumPadSlash = 0x135,
            Escape = 1,
            PrintScreen = 0x137,
            ScrollLock = 0x46,
            PauseBreak = 0x45,
            LeftWindows = 0x15B,
            RightWindows = 0x15C,
            F1 = 0x3B,
            F2 = 0x3C,
            F3 = 0x3D,
            F4 = 0x3E,
            F5 = 0x3F,
            F6 = 0x40,
            F7 = 0x41,
            F8 = 0x42,
            F9 = 0x43,
            F10 = 0x44,
            F11 = 0x57,
            F12 = 0x58,
        }
        #endregion
    }
}
