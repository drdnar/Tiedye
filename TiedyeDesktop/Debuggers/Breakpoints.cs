using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tiedye.Z80Core;

namespace TiedyeDesktop
{
    public partial class Breakpoints : Form
    {
        public Z80Cpu Cpu;

        public Breakpoints(Z80Cpu cpu)
        {
            InitializeComponent();
            Cpu = cpu;
            execBpUpDown.Value = Cpu.BpExecution;
            readBpUpDown.Value = Cpu.BpMemoryRead;
            writeBpUpDown.Value = Cpu.BpMemoryWrite;
            inBpUpDown.Value = Cpu.BpIoRead;
            outBpUpDown.Value = Cpu.BpIoWrite;
            anyIoCheckBox.Checked = Cpu.BpAnyIo;
            retBpCheckBox.Checked = Cpu.BpRet;
            intBpCheckBox.Checked = Cpu.BpInterrupt;
        }


        private void execBpUpDown_ValueChanged(object sender, EventArgs e)
        {
            Cpu.BpExecution = (int)execBpUpDown.Value;
        }

        private void readBpUpDown_ValueChanged(object sender, EventArgs e)
        {
            Cpu.BpMemoryRead = (int)readBpUpDown.Value;
        }

        private void writeBpUpDown_ValueChanged(object sender, EventArgs e)
        {
            Cpu.BpMemoryWrite = (int)writeBpUpDown.Value;
        }

        private void anyIoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Cpu.BpAnyIo = anyIoCheckBox.Checked;
        }

        private void inBpUpDown_ValueChanged(object sender, EventArgs e)
        {
            Cpu.BpIoRead = (int)inBpUpDown.Value;
        }

        private void outBpUpDown_ValueChanged(object sender, EventArgs e)
        {
            Cpu.BpIoWrite = (int)outBpUpDown.Value;
        }

        private void retBpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Cpu.BpRet = retBpCheckBox.Checked;
        }

        private void intBpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Cpu.BpInterrupt = intBpCheckBox.Checked;
        }

        private void Breakpoints_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


    }
}
