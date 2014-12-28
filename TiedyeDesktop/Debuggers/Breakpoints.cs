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
using Tiedye.Hardware;

namespace TiedyeDesktop
{
    public partial class Breakpoints : Form
    {
        public Z80Cpu Cpu;
        Ti8xCalculator Master;

        MemoryMapper Mapper
        {
            get
            {
                return Master.Calculator.MemoryMapper;
            }
        }

        public Breakpoints(Ti8xCalculator master)
        {
            Master = master;
            Cpu = Master.Calculator.Cpu;
            InitializeComponent();
            Master.UpdateData += Calculator_ExecutionFinished;
            execBpUpDown.Value = Cpu.BpExecution;
            readBpUpDown.Value = Cpu.BpMemoryRead;
            writeBpUpDown.Value = Cpu.BpMemoryWrite;
            inBpUpDown.Value = Cpu.BpIoRead;
            outBpUpDown.Value = Cpu.BpIoWrite;
            anyIoCheckBox.Checked = Cpu.BpAnyIo;
            retBpCheckBox.Checked = Cpu.BpRet;
            intBpCheckBox.Checked = Cpu.BpInterrupt;
            Calculator_ExecutionFinished(this, null);
            RefreshMemBpList();
        }

        StringBuilder cpuStateStr = new StringBuilder();
        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            //if (!Cpu.Break)
            //    return;
            cpuStateStr.Clear();
            cpuStateStr.AppendLine("PC   INSTR    SP   AF   BC   DE   HL");
            cpuStateStr.Append(Cpu.BreakCpuState[2].ToString("X4"));
            cpuStateStr.Append(" ");
            cpuStateStr.Append((Cpu.BreakCpuState[0] & 0xFF).ToString("X2"));
            cpuStateStr.Append((Cpu.BreakCpuState[0] >> 8).ToString("X2"));
            cpuStateStr.Append((Cpu.BreakCpuState[1] & 0xFF).ToString("X2"));
            cpuStateStr.Append((Cpu.BreakCpuState[1] >> 8).ToString("X2"));
            cpuStateStr.Append(" ");
            for (int k = 3; k < 8; k++)
            {
                cpuStateStr.Append(Cpu.BreakCpuState[k].ToString("X4"));
                cpuStateStr.Append(" ");
            }
            cpuStateStr.AppendLine();
            cpuStateStr.AppendLine("AF'  BC'  DE'  HL'  IX   IY   IR   IFF");
            for (int k = 8; k < 16; k++)
            {
                cpuStateStr.Append(Cpu.BreakCpuState[k].ToString("X4"));
                cpuStateStr.Append(" ");
            }
            execTextBox.Text = cpuStateStr.ToString();
        }

        public void RefreshMemBpList()
        {
            memBpListBox.Items.Clear();
            foreach (KeyValuePair<MemoryMapper.Breakpoint, bool> item in Master.Calculator.MemoryMapper.GetBreakpoints())
                memBpListBox.Items.Add(item.Key);
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
            Master.UpdateData -= Calculator_ExecutionFinished;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Calculator_ExecutionFinished(this, null);
        }

        private void addMemBpButton_Click(object sender, EventArgs e)
        {
            if (memBpTypeComboBox.SelectedIndex < 0)
                return;
            MemoryMapper.Breakpoint bp = new MemoryMapper.Breakpoint();
            bp.IsRam = isRamCheckBox.Checked;
            bp.Page = (byte)pageUpDown.Value;
            bp.Address = (ushort)addressUpDown.Value;
            switch (memBpTypeComboBox.SelectedIndex)
            {
                case 0:
                    bp.Type = MemoryMapper.MemoryBreakpointType.Execution;
                    break;
                case 1:
                    bp.Type = MemoryMapper.MemoryBreakpointType.Read;
                    break;
                case 2:
                    bp.Type = MemoryMapper.MemoryBreakpointType.Write;
                    break;
            }
            Mapper.AddBreakpoint(bp, true);
            RefreshMemBpList();
        }

        private void deleteMemBpButton_Click(object sender, EventArgs e)
        {
            if (memBpTypeComboBox.SelectedIndex < 0)
                return;
            if (memBpListBox.SelectedItem is MemoryMapper.Breakpoint)
            {
                MemoryMapper.Breakpoint bp = (MemoryMapper.Breakpoint)(memBpListBox.SelectedItem);
                Mapper.DeleteBreakpoint(bp);
            }
            RefreshMemBpList();
        }
        
        private void memBpListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (memBpListBox.SelectedItem is MemoryMapper.Breakpoint)
            {
                MemoryMapper.Breakpoint bp = (MemoryMapper.Breakpoint)(memBpListBox.SelectedItem);
                memBpActiveCheckBox.Checked = Mapper.IsBreakpoint(bp);
            }
        }

        private void memBpActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (memBpListBox.SelectedItem is MemoryMapper.Breakpoint)
            {
                MemoryMapper.Breakpoint bp = (MemoryMapper.Breakpoint)(memBpListBox.SelectedItem);
                //memBpActiveCheckBox.Checked = Mapper.IsBreakpoint(bp);
                if (memBpActiveCheckBox.Checked)
                    Mapper.ActivateBreakpoint(bp);
                else
                    Mapper.DeactivateBreakpoint(bp);
            }
        }


    }
}
