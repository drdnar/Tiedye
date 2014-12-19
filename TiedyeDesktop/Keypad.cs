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
            public KeyLocation(int Group = 0, int Key = 0, bool On = false)
            {
                this.Group = Group;
                this.Key = Key;
                this.On = On;
            }
        }

        private Timer lastKeyTimer = new Timer();
        private KeyLocation LastKey = default(KeyLocation);
        private bool lastKeyMouseReleased = false;

        public string KeyLog = "";

        protected void Log(string text)
        {
            KeyLog = text + "\r\n" + KeyLog;
        }

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
                Log("Click " + Convert.ToString(group, 2) + " " + Convert.ToString(key, 2) + on);
                if (lastKeyTimer.Enabled)
                {
                    // What a fast clicker!
                    // Release last key.
                    lastKeyTimer.Stop();
                    if (KeyReleased != null)
                    {
                        Log(". . . released.");
                        KeyReleased(this, LastKey);
                    }
                }
                LastKey = data;
                lastKeyMouseReleased = false;
                lastKeyTimer.Stop();
                lastKeyTimer.Start();
                if (KeyPressed != null)
                {
                    Log(". . . pressed.");
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
            lastKeyTimer.Stop();
            if (lastKeyMouseReleased)
                return;
            if (KeyReleased != null)
            {
                Log(". . . released.");
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
            if (KeyReleased != null)
                KeyReleased(this, LastKey);
        }

    }
}
