using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tiedye.Hardware;

namespace TiedyeDesktop
{
    // crashes some time after 0AC8

    /// <summary>
    /// Primary GUI for calculator.
    /// </summary>
    public partial class Ti8xCalculator : Form
    {
        /// <summary>
        /// Master window. Needed for . . . things.
        /// </summary>
        public MainWindow Master;
        /// <summary>
        /// Master reference to simulated calculator.
        /// </summary>
        public Calculator Calculator;

        public Bitmap ScreenImage = new Bitmap(320, 240);

        private string calculatorName = "Generic Calculator";
        /// <summary>
        /// Name of calculator.
        /// </summary>
        public string CalculatorName
        {
            get
            {
                return calculatorName;
            }
            set
            {
                calculatorName = value;
                this.Text = value;
            }
        }

        public Ti8xCalculator(MainWindow master, Calculator calculator)
        {
            InitializeComponent();
            Master = master;
            Calculator = calculator;
            Calculator.Flash.Data = System.IO.File.ReadAllBytes("84PCSE.rom");
            Calculator.ExecutionFinished += Calculator_ExecutionFinished;
            screen.Image = ScreenImage;
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            if (Calculator.Cpu.Break)
                Pause();
            UpdateScreen();
        }

        private void Ti8xCalculator_Enter(object sender, EventArgs e)
        {
            Master.SelectedCalculator = this;
        }

        private void keypad_KeyPressed(object sender, Keypad.KeyLocation e)
        {
            if (e.On)
                Calculator.Keypad.OnKey = true;
            else
                Calculator.Keypad.SetKey(e.Group, e.Key);
        }

        private void keypad_KeyReleased(object sender, Keypad.KeyLocation e)
        {
            if (e.On)
                Calculator.Keypad.OnKey = false;
            else
                Calculator.Keypad.ResetKey(e.Group, e.Key);
        }

        public bool Paused
        {
            get
            {
                return !cpuTimer.Enabled;
            }
            set
            {
                cpuTimer.Enabled = value;
            }
        }

        public void Pause()
        {
            cpuTimer.Stop();
            Master.playToolStripMenuItem.Text = "&Play";
            UpdateScreen();
        }

        public void Play()
        {
            cpuTimer.Start();
            Master.playToolStripMenuItem.Text = "&Pause";
        }

        private void cpuTimer_Tick(object sender, EventArgs e)
        {
            Calculator.ExecuteFor(0.025);
            if (Calculator.Cpu.Break)
                Pause();
        }

        public void UpdateScreen()
        {
            if (Calculator is Ti84PlusCSe)
            {
                ColorLcd lcd = (Calculator as Ti84PlusCSe).Lcd;
                for (int y = 0; y < lcd.Height; y++)
                    for (int x = 0; x < lcd.Width; x++)
                        unchecked
                        {
                            ScreenImage.SetPixel(x, y, Color.FromArgb((int)((uint)0xFF000000 | (uint)lcd[x, y])));
                        }
            }
            screen.Image = ScreenImage;
        }

        private void Ti8xCalculator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Calculator.ExecutionFinished -= Calculator_ExecutionFinished;
        }

        private void screenTimer_Tick(object sender, EventArgs e)
        {
            UpdateScreen();
        }

    }
}
