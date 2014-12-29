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
    public partial class CpuDebug : Form
    {
        public Ti8xCalculator Master;
        protected Z80Cpu Cpu;
        public CpuDebug(Ti8xCalculator master)
        {
            InitializeComponent();
            Master = master;
            Cpu = Master.Calculator.Cpu;
            RefreshRegisters();
            //Master.Calculator.ExecutionFinished += Calculator_ExecutionFinished;
            Master.UpdateData += Calculator_ExecutionFinished;
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshRegisters();
        }

        protected bool RefreshingData = false;

        public void RefreshRegisters()
        {
            if (RefreshingData)
                return;
            RefreshingData = true;
            aUpDown.Value = Cpu.A;
            bUpDown.Value = Cpu.B;
            cUpDown.Value = Cpu.C;
            dUpDown.Value = Cpu.D;
            eUpDown.Value = Cpu.E;
            fUpDown.Value = Cpu.F;
            hUpDown.Value = Cpu.H;
            lUpDown.Value = Cpu.L;
            ixUpDown.Value = Cpu.IX;
            iyUpDown.Value = Cpu.IY;
            pcUpDown.Value = Cpu.PC;
            spUpDown.Value = Cpu.SP;
            irUpDown.Value = Cpu.IR;
            imUpDown.Value = Cpu.IM;
            aPrimeUpDown.Value = Cpu.ShadowA;
            bPrimeUpDown.Value = Cpu.ShadowB;
            cPrimeUpDown.Value = Cpu.ShadowC;
            dPrimeUpDown.Value = Cpu.ShadowD;
            ePrimeUpDown.Value = Cpu.ShadowE;
            fPrimeUpDown.Value = Cpu.ShadowF;
            hPrimeUpDown.Value = Cpu.ShadowH;
            lPrimeUpDown.Value = Cpu.ShadowL;
            sFlagBox.Checked = Cpu.FlagS;
            zFlagBox.Checked = Cpu.FlagZ;
            hFlagBox.Checked = Cpu.FlagH;
            pvFlagBox.Checked = Cpu.FlagPv;
            nFlagBox.Checked = Cpu.FlagN;
            cFlagBox.Checked = Cpu.FlagC;
            haltBox.Checked = Cpu.Halt;
            resetCheckBox.Checked = Cpu.ForceReset;
            RefreshDisassembly();
            RefreshingData = false;
        }

        private void RefreshDisassembly()
        {
            unchecked
            {
                Z80Disassembler.DisassembledInstruction disasm;
                byte[] instr = new byte[4];
                StringBuilder str = new StringBuilder();
                int baseAddress = Cpu.PC;
                for (int i = 0; i < 64; i += disasm.Length)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        instr[j] = Cpu.MemoryRead(this, (ushort)(baseAddress + i + j));
                    }
                    str.Append(((ushort)(baseAddress + i)).ToString("X4"));
                    str.Append(": ");
                    disasm = Z80Disassembler.DisassembleInstruction(instr, (ushort)(baseAddress + i));
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
                }
                disassemblyTextBox.Text = str.ToString();
            }
        }

        private void aUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return; 
            Cpu.A = (byte)aUpDown.Value;
        }

        private void bUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.B = (byte)bUpDown.Value;
        }

        private void dUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.D = (byte)dUpDown.Value;
        }

        private void hUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.H = (byte)hUpDown.Value;
        }

        private void ixUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.IX = (ushort)ixUpDown.Value;
        }

        private void pcUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.PC = (ushort)pcUpDown.Value;
        }

        private void fUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.F = (byte)fUpDown.Value;
        }

        private void cUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (cUpDown.Value > 0xFF)
                bUpDown.Value = (int)cUpDown.Value >> 8;
            Cpu.C = (byte)cUpDown.Value;
        }

        private void eUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (eUpDown.Value > 0xFF)
                dUpDown.Value = (int)eUpDown.Value >> 8;
            Cpu.E = (byte)eUpDown.Value;
        }

        private void lUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (lUpDown.Value > 0xFF)
                hUpDown.Value = (int)lUpDown.Value >> 8;
            Cpu.L = (byte)lUpDown.Value;
        }

        private void aPrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.ShadowA = (byte)aPrimeUpDown.Value;
        }

        private void fPrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.ShadowF = (byte)fPrimeUpDown.Value;
        }

        private void bPrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.ShadowB = (byte)bPrimeUpDown.Value;
        }

        private void dPrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.ShadowD = (byte)dPrimeUpDown.Value;
        }

        private void hPrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.ShadowH = (byte)hPrimeUpDown.Value;
        }

        private void cPrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (cPrimeUpDown.Value > 0xFF)
                bPrimeUpDown.Value = (int)cPrimeUpDown.Value >> 8;
            Cpu.ShadowC = (byte)cPrimeUpDown.Value;
        }

        private void ePrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (ePrimeUpDown.Value > 0xFF)
                dPrimeUpDown.Value = (int)ePrimeUpDown.Value >> 8;
            Cpu.ShadowE = (byte)ePrimeUpDown.Value;
        }

        private void lPrimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (lPrimeUpDown.Value > 0xFF)
                hPrimeUpDown.Value = (int)lPrimeUpDown.Value >> 8;
            Cpu.ShadowL = (byte)lPrimeUpDown.Value;
        }

        private void sFlagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.FlagS = sFlagBox.Checked;
        }

        private void zFlagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.FlagZ = zFlagBox.Checked;
        }

        private void hFlagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.FlagH = hFlagBox.Checked;
        }

        private void pvFlagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.FlagPv = pvFlagBox.Checked;
        }

        private void nFlagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.FlagN = nFlagBox.Checked;
        }

        private void cFlagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.FlagC = cFlagBox.Checked;
        }

        private void iff1Box_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.IFF1 = iff1Box.Checked;
        }

        private void iff2Box_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.IFF2 = iff2Box.Checked;
        }

        private void exAfAfButton_Click(object sender, EventArgs e)
        {
            Cpu.ExAfShadowAf();
            RefreshRegisters();
        }

        private void exxButton_Click(object sender, EventArgs e)
        {
            Cpu.Exx();
            RefreshRegisters();
        }

        private void stepOnceButton_Click(object sender, EventArgs e)
        {
            Cpu.BreakEnable = false;
            Master.Calculator.Step();
            Cpu.BreakEnable = true;
            Master.UpdateEverything();
        }

        private void stepForButton_Click(object sender, EventArgs e)
        {
            Master.Calculator.Step((long)stepForUpDown.Value);
            Master.UpdateEverything();
        }

        private void CpuDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            Master.Calculator.ExecutionFinished -= Calculator_ExecutionFinished;
        }

        private void spUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.SP = (ushort)(spUpDown.Value);
        }

        private void iyUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.IY = (ushort)iyUpDown.Value;
        }
        private void irUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.IR = (ushort)irUpDown.Value;
        }

        private void haltBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.Halt = haltBox.Checked;
        }

        private void resetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.ForceReset = resetCheckBox.Checked;
        }

        private void imUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Cpu.IM = (ushort)imUpDown.Value;
        }

    }
}
