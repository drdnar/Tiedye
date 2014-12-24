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
    public partial class TimersDebug : Form
    {
        public Ti8xCalculator Master;

        protected Calculator Calculator
        {
            get
            {
                return Master.Calculator;
            }
        }

        public TimersDebug(Ti8xCalculator master)
        {
            InitializeComponent();
            Master = master;
            //Master.Calculator.ExecutionFinished += Calculator_ExecutionFinished;
            Master.UpdateData += Calculator_ExecutionFinished;
            if (Calculator is Ti84PlusCSe)
            {

            }
            else
            {
                cTimer1GroupBox.Visible = false;
                cTimer2GroupBox.Visible = false;
                cTimer3GroupBox.Visible = false;
            }
            RefreshData();
        }

        public void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        bool RefreshingData = false;

        public void RefreshData()
        {
            RefreshingData = true;
            timer1Speed.Text = Calculator.TimerSetting.ToString();
            timer2Speed.Text = Calculator.TimerSetting.ToString();
            timer1Period.Text = Math.Round((Calculator.Apd.Period * 1000), 3).ToString() + " ms";
            timer2Period.Text = Math.Round((Calculator.PApd.Period * 1000), 3).ToString() + " ms";
            timer1NextTick.Text = Math.Round((Calculator.Apd.NextTick - Calculator.Cpu.Clock.WallTime) * 1000, 3).ToString() + " ms";
            timer2NextTick.Text = Math.Round((Calculator.PApd.NextTick  - Calculator.Cpu.Clock.WallTime) * 1000, 3).ToString() + " ms";
            timer1InterruptEnableCheckBox.Checked = Calculator.Apd.GenerateInterrupt;
            timer2InterruptEnableCheckBox.Checked = Calculator.PApd.GenerateInterrupt;
            timer1InterruptCheckBox.Checked = Calculator.Apd.HasInterrupt;
            timer2InterruptCheckBox.Checked = Calculator.PApd.HasInterrupt;
            if (Calculator is Ti84PlusCSe)
            {
                CrystalTimer ctimer = ((Ti84PlusCSe)Calculator).CrystalTimer1;
                cTimer1ModeLabel.Text = ctimer.FrequencyMode.ToString();
                cTimer1CountLabel.Text = ctimer.Count.ToString("X2");
                cTimer1FreqLabel.Text = ctimer.CounterMode.ToString("X2");
                if (ctimer.FrequencyMode == CrystalTimer.Mode.Off || !ctimer.IsActive)
                    cTimer1NextLabel.Text = "(off)";
                else if (ctimer.FrequencyMode == CrystalTimer.Mode.FixedFrequency)
                    cTimer1NextLabel.Text = Math.Round((ctimer.NextWtTime - Calculator.Cpu.Clock.WallTime) * 1000, 3).ToString();
                else if (ctimer.FrequencyMode == CrystalTimer.Mode.CpuClockCounter)
                    cTimer1NextLabel.Text = (ctimer.NextScTime - Calculator.Cpu.Clock.ClockTime).ToString();
                else
                    cTimer1NextLabel.Text = "?";
                cTimer1LoopCheckBox.Checked = ctimer.Loop;
                cTimer1MissedLoopCheckBox.Checked = ctimer.MissedLoop;
                cTimer1ExpiredCheckBox.Checked = ctimer.HasExpired;
                cTimer1InterruptEnableCheckBox.Checked = ctimer.GenerateInterrupt;
                cTimer1InterruptCheckBox.Checked = ctimer.HasInterrupt;
                ctimer = ((Ti84PlusCSe)Calculator).CrystalTimer2;
                cTimer2ModeLabel.Text = ctimer.FrequencyMode.ToString();
                cTimer2CountLabel.Text = ctimer.Count.ToString("X2");
                cTimer2FreqLabel.Text = ctimer.CounterMode.ToString("X2");
                if (ctimer.FrequencyMode == CrystalTimer.Mode.Off || !ctimer.IsActive)
                    cTimer2NextLabel.Text = "(off)";
                else if (ctimer.FrequencyMode == CrystalTimer.Mode.FixedFrequency)
                    cTimer2NextLabel.Text = Math.Round((ctimer.NextWtTime - Calculator.Cpu.Clock.WallTime) * 1000, 3).ToString();
                else if (ctimer.FrequencyMode == CrystalTimer.Mode.CpuClockCounter)
                    cTimer2NextLabel.Text = (ctimer.NextScTime - Calculator.Cpu.Clock.ClockTime).ToString();
                else
                    cTimer2NextLabel.Text = "?";
                cTimer2LoopCheckBox.Checked = ctimer.Loop;
                cTimer2MissedLoopCheckBox.Checked = ctimer.MissedLoop;
                cTimer2ExpiredCheckBox.Checked = ctimer.HasExpired; 
                cTimer2InterruptEnableCheckBox.Checked = ctimer.GenerateInterrupt;
                cTimer2InterruptCheckBox.Checked = ctimer.HasInterrupt;
                ctimer = ((Ti84PlusCSe)Calculator).CrystalTimer3;
                cTimer3ModeLabel.Text = ctimer.FrequencyMode.ToString();
                cTimer3CountLabel.Text = ctimer.Count.ToString("X2");
                cTimer3FreqLabel.Text = ctimer.CounterMode.ToString("X2");
                if (ctimer.FrequencyMode == CrystalTimer.Mode.Off || !ctimer.IsActive)
                    cTimer3NextLabel.Text = "(off)";
                else if (ctimer.FrequencyMode == CrystalTimer.Mode.FixedFrequency)
                    cTimer3NextLabel.Text = Math.Round((ctimer.NextWtTime - Calculator.Cpu.Clock.WallTime) * 1000, 3).ToString();
                else if (ctimer.FrequencyMode == CrystalTimer.Mode.CpuClockCounter)
                    cTimer3NextLabel.Text = (ctimer.NextScTime - Calculator.Cpu.Clock.ClockTime).ToString();
                else
                    cTimer3NextLabel.Text = "?";
                cTimer3LoopCheckBox.Checked = ctimer.Loop;
                cTimer3MissedLoopCheckBox.Checked = ctimer.MissedLoop;
                cTimer3ExpiredCheckBox.Checked = ctimer.HasExpired;
                cTimer3InterruptEnableCheckBox.Checked = ctimer.GenerateInterrupt;
                cTimer3InterruptCheckBox.Checked = ctimer.HasInterrupt;

            }
            RefreshingData = false;
        }

        private void timer1InterruptEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Calculator.Apd.GenerateInterrupt = timer1InterruptEnableCheckBox.Checked;
        }

        private void timer1InterruptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Calculator.Apd.HasInterrupt = timer1InterruptCheckBox.Checked;
        }

        private void timer2InterruptEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Calculator.PApd.GenerateInterrupt = timer2InterruptEnableCheckBox.Checked;
        }

        private void timer2InterruptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Calculator.PApd.HasInterrupt = timer2InterruptCheckBox.Checked;
        }

        private void cTimer1LoopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (sender == cTimer1LoopCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer1.Loop = cTimer1LoopCheckBox.Checked;
            else if (sender == cTimer2LoopCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer2.Loop = cTimer2LoopCheckBox.Checked;
            else if (sender == cTimer3LoopCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer3.Loop = cTimer3LoopCheckBox.Checked;
            else
                cTimer1GroupBox.Text = "???";
        }

        private void cTimer1MissedLoopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (sender == cTimer1MissedLoopCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer1.MissedLoop = cTimer1MissedLoopCheckBox.Checked;
            else if (sender == cTimer2MissedLoopCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer2.MissedLoop = cTimer2MissedLoopCheckBox.Checked;
            else if (sender == cTimer3MissedLoopCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer3.MissedLoop = cTimer3MissedLoopCheckBox.Checked;
            else
                cTimer1GroupBox.Text = "???";
        }

        private void cTimer1InterruptEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (sender == cTimer1InterruptEnableCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer1.GenerateInterrupt = cTimer1InterruptEnableCheckBox.Checked;
            else if (sender == cTimer2InterruptEnableCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer2.GenerateInterrupt = cTimer2InterruptEnableCheckBox.Checked;
            else if (sender == cTimer3InterruptEnableCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer3.GenerateInterrupt = cTimer3InterruptEnableCheckBox.Checked;
            else
                cTimer1GroupBox.Text = "???";
        }

        private void cTimer1InterruptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (sender == cTimer1InterruptCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer1.HasInterrupt = cTimer1InterruptCheckBox.Checked;
            else if (sender == cTimer2InterruptCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer2.HasInterrupt = cTimer2InterruptCheckBox.Checked;
            else if (sender == cTimer3InterruptCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer3.HasInterrupt = cTimer3InterruptCheckBox.Checked;
            else
                cTimer1GroupBox.Text = "???";
        }

        private void cTimer1ExpiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (sender == cTimer1ExpiredCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer1.HasExpired = cTimer1ExpiredCheckBox.Checked;
            else if (sender == cTimer2ExpiredCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer2.HasExpired = cTimer2ExpiredCheckBox.Checked;
            else if (sender == cTimer3ExpiredCheckBox)
                ((Ti84PlusCSe)Calculator).CrystalTimer3.HasExpired = cTimer3ExpiredCheckBox.Checked;
            else
                cTimer1GroupBox.Text = "???";
        }



    }
}
