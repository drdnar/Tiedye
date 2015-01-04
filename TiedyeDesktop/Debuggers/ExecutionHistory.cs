






































































































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
    public partial class ExecutionHistory : Form
    {
        public Ti8xCalculator Master;
        protected Z80Cpu Cpu;
 
        public ExecutionHistory(Ti8xCalculator master)
        {
            InitializeComponent();
            Master = master;
            Cpu = Master.Calculator.Cpu;
            //Master.Calculator.ExecutionFinished += Calculator_ExecutionFinished;
            Master.UpdateData += Calculator_ExecutionFinished;
            instrCountUpDown.Minimum = 1;
            instrCountUpDown.Maximum = Z80Cpu.LastExecSize;
            RefreshData();
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            if (Master.Executing)
                return;
            RefreshData();
        }

        public void RefreshData()
        {
            traceEnableCheckBox.Checked = Cpu.TraceLastExec;
            bcallLogCheckBox.Checked = Cpu.LogBCalls;
            if (showExecRadioButton.Checked)
            {
                instrCountUpDown.Value = instrShowCount;
                RefreshExecHistroy();
            }
            else
            {
                instrCountUpDown.Value = bcallShowCount;
                RefreshBCallHistory();
            }

        }

        int instrShowCount = 64;
        int bcallShowCount = 64;

        void RefreshBCallHistory()
        {
            unchecked
            {
                if (!Cpu.LogBCalls)
                    return;
                historyStrBuilder.Clear();
                historyStrBuilder.AppendLine("PC   BCal AF   BC   DE   HL   IX   IY   SP");
                //int baseAddress = Cpu.PC;
                int pos = (Cpu.BCallLogPtr - 1) & Z80Cpu.BCallLogMask;
                for (int i = 0; i < bcallShowCount; i++)
                {
                    
                    for (int k = 0; k < 9; k++)
                    {
                        historyStrBuilder.Append(Cpu.BCallLogData[pos, k].ToString("X4"));
                        historyStrBuilder.Append(" ");
                    }
                    historyStrBuilder.AppendLine();
                    pos = (pos - 1) & Z80Cpu.BCallLogMask;
                }
                disassemblyTextBox.Text = historyStrBuilder.ToString();
            }
        }

        StringBuilder historyStrBuilder = new StringBuilder();

        void RefreshExecHistroy()
        {
            unchecked
            {
                if (!Cpu.TraceLastExec)
                    return;
                Z80Disassembler.DisassembledInstruction disasm;
                byte[] instr = new byte[4];
                historyStrBuilder.Clear();
                historyStrBuilder.AppendLine("                               SP   AF   BC   DE   HL   AF'  BC'  DE'  HL'  IX   IY   IR   IFF");
                //int baseAddress = Cpu.PC;
                int pos = (Cpu.LastExecPtr - 1) & Z80Cpu.LastExecMask;
                for (int i = 0; i < instrShowCount; i++)
                {
                    /*for (int j = 0; j < 4; j++)
                    {
                        instr[j] = Cpu.LastExecOpcode[pos, j];
                    }*/
                    instr[0] = (byte)(Cpu.LastExecData[pos, 0] & 0xFF);
                    instr[1] = (byte)(Cpu.LastExecData[pos, 0] >> 8);
                    instr[2] = (byte)(Cpu.LastExecData[pos, 1] & 0xFF);
                    instr[3] = (byte)(Cpu.LastExecData[pos, 1] >> 8);
                    ushort PC = Cpu.LastExecData[pos, 2];
                    //str.Append(Cpu.LastExecAddress[pos].ToString("X4"));
                    historyStrBuilder.Append(PC.ToString("X4"));
                    historyStrBuilder.Append(": ");
                    disasm = Z80Disassembler.DisassembleInstruction(instr, PC);
                    for (int j = 0; j < 4; j++)
                    {
                        if (j < disasm.Length)
                            historyStrBuilder.Append(instr[j].ToString("X2"));
                        else
                            historyStrBuilder.Append("  ");
                    }
                    historyStrBuilder.Append(" ");
                    historyStrBuilder.Append(disasm.Disassembly);
                    for (int k = disasm.Disassembly.Length; k < 16; k++)
                        historyStrBuilder.Append(" ");
                    for (int k = 3; k < 16; k++)
                    {
                        historyStrBuilder.Append(Cpu.LastExecData[pos, k].ToString("X4"));
                        historyStrBuilder.Append(" ");
                    }
                    historyStrBuilder.AppendLine();
                    pos = (pos - 1) & Z80Cpu.LastExecMask;
                }
                disassemblyTextBox.Text = historyStrBuilder.ToString();
            }
        }

        private void traceEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Cpu.TraceLastExec = traceEnableCheckBox.Checked;
        }

        private void instrCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (showExecRadioButton.Checked)
            {
                instrShowCount = (int)instrCountUpDown.Value;
                RefreshExecHistroy();
            }
            else
            {
                bcallShowCount = (int)instrCountUpDown.Value;
                RefreshBCallHistory();
            }
        }

        private void bcallLogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Cpu.LogBCalls = bcallLogCheckBox.Checked;
        }

        private void showExecRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (showExecRadioButton.Checked)
            {
                instrCountUpDown.Maximum = Z80Cpu.LastExecSize;
                instrCountUpDown.Value = instrShowCount;
                RefreshExecHistroy();
            }
            else
            {
                instrCountUpDown.Maximum = Z80Cpu.BCallLogSize;
                instrCountUpDown.Value = bcallShowCount;
                RefreshBCallHistory();
            }
        }
    }
}
