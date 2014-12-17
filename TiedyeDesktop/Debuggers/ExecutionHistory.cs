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
            RefreshData();
            Master.Calculator.ExecutionFinished += Calculator_ExecutionFinished;
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            unchecked
            {
                Z80Disassembler.DisassembledInstruction disasm;
                byte[] instr = new byte[4];
                StringBuilder str = new StringBuilder();
                //int baseAddress = Cpu.PC;
                int pos = (Cpu.LastExecPtr - 1) & Z80Cpu.LastExecMask;
                for (int i = 0; i < Z80Cpu.LastExecSize; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        instr[j] = Cpu.LastExecOpcode[pos, j];
                    }
                    str.Append(Cpu.LastExecAddress[pos].ToString("X4"));
                    str.Append(": ");
                    disasm = Z80Disassembler.DisassembleInstruction(instr, Cpu.LastExecAddress[pos]);
                    for (int j = 0; j < 4; j++)
                    {
                        if (j < disasm.Length)
                            str.Append(instr[j].ToString("X2"));
                        else
                            str.Append("  ");
                    }
                    str.Append(" ");
                    str.Append(disasm.Disassembly);
                    str.AppendLine();
                    pos = (pos - 1) & Z80Cpu.LastExecMask;
                }
                disassemblyTextBox.Text = str.ToString();
            }
        }
    }
}
