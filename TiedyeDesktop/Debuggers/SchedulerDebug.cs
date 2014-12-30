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
    public partial class SchedulerDebug : Form
    {
        Ti8xCalculator Master;
        Tiedye.Hardware.Calculator Calc;
        public SchedulerDebug(Ti8xCalculator master)
        {//Tiedye.Hardware.Calculator 
            InitializeComponent();
            Master = master;
            Calc = Master.Calculator;
            //Master.ExecutionFinished += Calculator_ExecutionFinished;
            Master.UpdateData += Calculator_ExecutionFinished;
            Calculator_ExecutionFinished(null, null);
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            dataTextBox.Text = Calc.Scheduler.GetDebugInformation();
        }

        private void SchedulerDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Master.ExecutionFinished -= Calculator_ExecutionFinished;
            Master.UpdateData -= Calculator_ExecutionFinished;
        }

    }
}
