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
            RefreshData();
        }

        public void RefreshData()
        {
            unchecked
            {
                traceEnableCheckBox.Checked = Cpu.TraceLastExec;
                if (!Cpu.TraceLastExec)
                    return;
                Z80Disassembler.DisassembledInstruction disasm;
                byte[] instr = new byte[4];
                StringBuilder str = new StringBuilder();
                str.AppendLine("                               SP   AF   BC   DE   HL   AF'  BC'  DE'  HL'  IX   IY   IR   IFF");
                //int baseAddress = Cpu.PC;
                int pos = (Cpu.LastExecPtr - 1) & Z80Cpu.LastExecMask;
                int asdf = (int)instrCountUpDown.Value;
                for (int i = 0; i < asdf; i++)
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
                    str.Append(PC.ToString("X4"));
                    str.Append(": ");
                    disasm = Z80Disassembler.DisassembleInstruction(instr, PC);
                    for (int j = 0; j < 4; j++)
                    {
                        if (j < disasm.Length)
                            str.Append(instr[j].ToString("X2"));
                        else
                            str.Append("  ");
                    }
                    str.Append(" ");
                    str.Append(disasm.Disassembly);
                    for (int k = disasm.Disassembly.Length; k < 16; k++)
                        str.Append(" ");
                    for (int k = 3; k < 16; k++ )
                    {
                        str.Append(Cpu.LastExecData[pos, k].ToString("X4"));
                        str.Append(" ");
                    }
                    str.AppendLine();
                    pos = (pos - 1) & Z80Cpu.LastExecMask;
                }
                disassemblyTextBox.Text = str.ToString();
            }
        }

        private void traceEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Cpu.TraceLastExec = traceEnableCheckBox.Checked;
        }

        private void instrCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
