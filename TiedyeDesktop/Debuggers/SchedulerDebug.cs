using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiedyeDesktop
{
    public partial class SchedulerDebug : Form
    {
        Tiedye.Hardware.Calculator Master;
        public SchedulerDebug(Tiedye.Hardware.Calculator master)
        {
            InitializeComponent();
            Master = master;
            Master.ExecutionFinished += Calculator_ExecutionFinished;
            Calculator_ExecutionFinished(null, null);
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            dataTextBox.Text = "Frequency: " + Master.Cpu.Clock.Frequency + "\r\n" + Master.Scheduler.GetDebugInformation();
        }

        private void SchedulerDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            Master.ExecutionFinished -= Calculator_ExecutionFinished;
        }

    }
}
