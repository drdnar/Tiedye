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
using Tiedye.Z80Core;

namespace TiedyeDesktop
{
    public partial class SystemStatusDebug : Form
    {
        public Ti8xCalculator Master;
        Calculator Calculator
        {
            get
            {
                return Master.Calculator;
            }
        }

        public SystemStatusDebug(Ti8xCalculator master)
        {
            InitializeComponent();
            Master = master;
            Master.UpdateData += Calculator_ExecutionFinished;
            speedNumericUpDown.Value = (decimal)Master.ExecutionSpeed;
            RefreshData();
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Master.UpdateData -= Calculator_ExecutionFinished;
            base.OnFormClosed(e);
        }

        double OldFreq;
        Calculator.InterruptId OldInts;

        public void RefreshData()
        {
            double oldFreq = Calculator.Cpu.Clock.Frequency;
            if (oldFreq != OldFreq)
            {
                OldFreq = oldFreq;
                frequencyLabel.Text = OldFreq.ToString();
            }
            execTimeLabel.Text = Math.Round(Master.AverageDeltaT * 1000, 3) + " ms (relative to " + Math.Round(Master.ExecutionQuantum * 1000, 0) + " ms nominal)" + ((int)(Master.ExecutionSpeed * 100) != 100 ? " (scaled with speed)" : "");
            Calculator.InterruptId oldInts = Calculator.Interrupts;
            if (oldInts != OldInts)
            {
                OldInts = oldInts;
                intsLabel.Text = OldInts.ToString();
            }
            
        }

        private void speedNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Master.ExecutionSpeed = (double)speedNumericUpDown.Value;
        }
    }
}
