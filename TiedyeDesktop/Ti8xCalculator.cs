﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
            Calculator.Cpu.ResetEvent += Calculator_Reset;
            screen.Image = ScreenImage;
        }

        void Calculator_Reset(object sender, EventArgs e)
        {
            Calculator.Cpu.Break = true;
            Pause();
            Calculator.Cpu.ForceReset = false;
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            if (Calculator.Cpu.Break)
                Pause();
            //UpdateScreen();
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
            //cpuTimer.Stop();
            ContinueExecution = false;
            Master.playToolStripMenuItem.Text = "&Play";
        }

        public void Play()
        {
            cpuTimer.Start();
            if (executing)
                return;
            ContinueExecution = true;
            executionThread = new Thread(new ThreadStart(DoEmulation));
            executionThread.Start();
            Master.playToolStripMenuItem.Text = "&Pause";
        }

        protected bool executing;
        public bool ContinueExecution = false;

        public bool Throttle = true;

        protected Thread executionThread;

        public double ExecutionQuantum = 0.05;

        public double AverageDeltaT = 0.01;

        protected void DoEmulation()
        {
            executing = true;
            System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();
            System.TimeSpan lastTime;
            System.TimeSpan delta;
            System.TimeSpan execQuantumSpan = new System.TimeSpan(0, 0, 0, (int)ExecutionQuantum, (int)(ExecutionQuantum * 1000));
            double deltaT;
            double alpha = 0.1;
            while (ContinueExecution)
            {
                lastTime = timer.Elapsed;
                Calculator.ExecuteFor(ExecutionQuantum);//0.005);//
                if (Calculator.Cpu.Break)
                    Pause();
                delta = (timer.Elapsed - lastTime);
                deltaT = delta.TotalSeconds;
                AverageDeltaT = alpha * (execQuantumSpan - delta).TotalSeconds + (1.0 - alpha) * AverageDeltaT;
                if (deltaT < ExecutionQuantum)
                    Thread.Sleep(execQuantumSpan - delta);
            }
            executing = false;
        }

        private void cpuTimer_Tick(object sender, EventArgs e)
        {
            /*Calculator.ExecuteFor((double)cpuTimer.Interval / 10000);
            if (Calculator.Cpu.Break)
                Pause();*/
            UpdateEverything();
            if (!ContinueExecution)
                cpuTimer.Stop();
        }

        public void UpdateEverything()
        {
            if (UpdateData != null)
                UpdateData(this, null);
            UpdateScreen();
        }

        public void UpdateScreen()
        {
            keyLogTextBox.Text = keypad.KeyLog;
            if (Calculator is Ti84PlusCSe)
            {
                ColorLcd lcd = (Calculator as Ti84PlusCSe).Lcd;
                for (int y = 0; y < lcd.Height; y++)
                    for (int x = 0; x < lcd.Width; x++)
                        unchecked
                        {
                            ScreenImage.SetPixel(x, y, Color.FromArgb((int)((uint)0xFF000000 | (uint)lcd[x, y])));
                            /*if (((int)(Calculator.Cpu.Clock.WallTime * 10) & 1) != 0)
                                ScreenImage.SetPixel(x, y, (lcd.Data[x, y] != 0 ? Color.Blue : Color.Red));
                            else
                                ScreenImage.SetPixel(x, y, (lcd.Data[x, y] != 0 ? Color.Purple : Color.Pink));*/
                        }
            }
            screen.Image = ScreenImage;
            screen.Refresh();
        }

        private void Ti8xCalculator_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (executing && executionThread != null)
                executionThread.Abort();
            Calculator.ExecutionFinished -= Calculator_ExecutionFinished;
        }

        private void screenTimer_Tick(object sender, EventArgs e)
        {
            //UpdateScreen();
        }


        /// <summary>
        /// This event is fired regularly, so that things can do updating.
        /// </summary>
        public event EventHandler UpdateData;

        #region Associated windows
        
        KeypadDebug keypadDebug;
        public KeypadDebug KeypadDebug
        {
            get
            {
                return keypadDebug;
            }
            set
            {
                keypadDebug = value;
                if (value != null)
                    keypadDebug.FormClosed += keypadDebug_FormClosed;
            }
        }

        void keypadDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            keypadDebug.FormClosed -= keypadDebug_FormClosed;
            keypadDebug = null;
        }

        ColorLcdDebug colorLcdDebug;
        public ColorLcdDebug ColorLcdDebug
        {
            get
            {
                return colorLcdDebug;
            }
            set
            {
                colorLcdDebug = value;
                if (value != null)
                    colorLcdDebug.FormClosed += colorLcdDebug_FormClosed;
            }
        }

        void colorLcdDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            colorLcdDebug.FormClosed -= colorLcdDebug_FormClosed;
            colorLcdDebug = null;
        }

        SchedulerDebug schedulerDebug;
        public SchedulerDebug SchedulerDebug
        {
            get
            {
                return schedulerDebug;
            }
            set
            {
                schedulerDebug = value;
                if (value != null)
                    schedulerDebug.FormClosed += schedulerDebug_FormClosed;
            }
        }

        void schedulerDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            schedulerDebug.FormClosed -= schedulerDebug_FormClosed;
            schedulerDebug = null;
        }

        Breakpoints breakpoints;
        public Breakpoints Breakpoints
        {
            get
            {
                return breakpoints;
            }
            set
            {
                breakpoints = value;
                if (value != null)
                    breakpoints.FormClosed += breakpoints_FormClosed;
            }
        }

        void breakpoints_FormClosed(object sender, FormClosedEventArgs e)
        {
            breakpoints.FormClosed -= breakpoints_FormClosed;
            breakpoints = null;
        }

        MemoryDebugSe memoryDebugSe;
        public MemoryDebugSe MemoryDebugSe
        {
            get
            {
                return memoryDebugSe;
            }
            set
            {
                memoryDebugSe = value;
                if (value != null)
                    memoryDebugSe.FormClosed += memoryDebugSe_FormClosed;
            }
        }

        void memoryDebugSe_FormClosed(object sender, FormClosedEventArgs e)
        {
            memoryDebugSe.FormClosed -= memoryDebugSe_FormClosed;
            memoryDebugSe = null;
        }

        CpuDebug cpuDebug;
        public CpuDebug CpuDebug
        {
            get
            {
                return cpuDebug;
            }
            set
            {
                cpuDebug = value;
                if (value != null)
                    cpuDebug.FormClosed += cpuDebug_FormClosed;
            }
        }

        void cpuDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            cpuDebug.FormClosed -= cpuDebug_FormClosed;
            cpuDebug = null;
        }

        ExecutionHistory executionHistory;
        public ExecutionHistory ExecutionHistory
        {
            get
            {
                return executionHistory;
            }
            set
            {
                executionHistory = value;
                if (value != null)
                    executionHistory.FormClosed += executionHistory_FormClosed;
            }
        }

        void executionHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            executionHistory.FormClosed -= executionHistory_FormClosed;
            executionHistory = null;
        }

        TimersDebug timersDebug;
        public TimersDebug TimersDebug
        {
            get
            {
                return timersDebug;
            }
            set
            {
                timersDebug = value;
                if (value != null)
                    timersDebug.FormClosed += timersDebug_FormClosed;
            }
        }

        void timersDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            timersDebug.FormClosed -= timersDebug_FormClosed;
            timersDebug = null;
        }
        #endregion

    }
}
