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
    public partial class IoLogDebug : Form
    {
        public Ti8xCalculator Master;
        protected Calculator Calculator;

        public IoLogDebug(Ti8xCalculator master)
        {
            InitializeComponent();
            Master = master;
            Calculator = master.Calculator;
            Master.UpdateData += Calculator_ExecutionFinished;
            traceCountUpDown.Minimum = 1;
            traceCountUpDown.Maximum = Calculator.IoLogSize;
            RefreshData();
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            if (Master.Executing)
                return;
            RefreshData();
        }

        StringBuilder str = new StringBuilder();

        public void RefreshData()
        {
            traceEnableCheckBox.Checked = Calculator.TraceIo;
            str.Clear();
            int pos = (Calculator.IoLogPtr - 1) & Calculator.IoLogMask;
            int asdf = (int)traceCountUpDown.Value;
            for (int i = 0; i < asdf; i++)
            {
                if ((Calculator.IoLogData[pos, 2] & 1) != 0)
                    str.Append("OUT: ");
                else
                    str.Append("IN:  ");
                str.Append(Calculator.IoLogData[pos, 1].ToString("X2"));
                if ((Calculator.IoLogData[pos, 2] & 1) != 0)
                    str.Append(" => ");
                else
                    str.Append(" <= ");
                str.Append(Calculator.IoLogData[pos, 0].ToString("X2"));
                str.Append(" (");
                str.Append(((Ti84PlusCSe.PortNames)Calculator.IoLogData[pos, 0]).ToString());
                str.Append(")");
                str.AppendLine();
                pos = (pos - 1) & Calculator.IoLogMask;
            }
            logTextBox.Text = str.ToString();
        }

        private void traceEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Calculator.TraceIo = traceEnableCheckBox.Checked;
        }

        private void traceCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
