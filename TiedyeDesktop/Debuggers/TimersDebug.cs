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
            RefreshData();
        }

        public void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
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

        }

        private void timer1InterruptEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Calculator.Apd.GenerateInterrupt = timer1InterruptEnableCheckBox.Checked;
        }

        private void timer1InterruptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Calculator.Apd.HasInterrupt = timer1InterruptCheckBox.Checked;
        }

        private void timer2InterruptEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Calculator.PApd.GenerateInterrupt = timer2InterruptEnableCheckBox.Checked;
        }

        private void timer2InterruptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Calculator.PApd.HasInterrupt = timer2InterruptCheckBox.Checked;
        }

    }
}
