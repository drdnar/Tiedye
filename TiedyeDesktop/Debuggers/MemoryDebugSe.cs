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
    public partial class MemoryDebugSe : Form
    {
        protected MemoryMapperSE Mapper;
        protected Ti8xCalculator Master;
        public MemoryDebugSe(Ti8xCalculator master)
        {
            InitializeComponent();
            Master = master;
            Mapper = Master.Calculator.MemoryMapper as MemoryMapperSE;
            RefreshData();
            flashMemoryEditor.ReadAByte = new MemoryEditorPanel.ReadByte(Master.Calculator.Flash.ReadByte);
            flashMemoryEditor.WriteAByte = new MemoryEditorPanel.WriteByte(Master.Calculator.Flash.WriteByteForced);
            //flashMemoryEditor.WriteAByte = 
            flashMemoryEditor.DataLength = Master.Calculator.Flash.Data.Length;
            ramMemoryEditor.ReadAByte = new MemoryEditorPanel.ReadByte(Master.Calculator.Ram.ReadByte);
            ramMemoryEditor.WriteAByte = new MemoryEditorPanel.WriteByte(Master.Calculator.Ram.WriteByte);
            ramMemoryEditor.DataLength = Master.Calculator.Ram.Data.Length;
            z80MemoryEditor.ReadAByte = new MemoryEditorPanel.ReadByte(GetZ80Byte);
            z80MemoryEditor.WriteAByte = new MemoryEditorPanel.WriteByte(PutZ80Byte);
            z80MemoryEditor.DataLength = 0x10000;
            //Master.Calculator.ExecutionFinished += Calculator_ExecutionFinished;
            Master.UpdateData += Calculator_ExecutionFinished;
        }

        private byte GetZ80Byte(object sender, int address)
        {
            unchecked
            {
                return Master.Calculator.MemoryMapper.BusRead(sender, (ushort)address);
            }
        }

        private void PutZ80Byte(object sender, int address, byte value)
        {
            unchecked
            {
                Master.Calculator.MemoryMapper.BusWrite(sender, (ushort)address, value);
            }
        }


        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            /*if (Mapper.PageAIsRam)
                bankAUpDown.Value = Mapper.PageA & 0x0F;
            else*/
                bankAUpDown.Value = Mapper.PageA;
            bankAIsRam.Checked = Mapper.PageAIsRam;
            bankAIsFlash.Checked = !Mapper.PageAIsRam;
            /*if (Mapper.PageBIsRam)
                bankBUpDown.Value = Mapper.PageB & 0x0F;
            else*/
                bankBUpDown.Value = Mapper.PageB;
            bankBIsRam.Checked = Mapper.PageBIsRam;
            bankBIsFlash.Checked = !Mapper.PageBIsRam;
            bankCUpDown.Value = Mapper.PageC;
            //bankCIsRam.Checked = true;
            //bankCIsFlash.Checked = false;
            bootModeBox.Checked = Mapper.BootMode;
            mode1Box.Checked = Mapper.MemoryMappingMode == 1;
            ramTypeUpDown.Value = Mapper.RamType;
            flashTypeUpDown.Value = Mapper.FlashType;
            actual0000UpDown.Value = Mapper.Bank0000;
            actual0000IsRam.Checked = Mapper.Bank0000IsRam;
            actual0000IsFlash.Checked = !Mapper.Bank0000IsRam;
            actual4000UpDown.Value = Mapper.Bank4000;
            actual4000IsRam.Checked = Mapper.Bank4000IsRam;
            actual4000IsFlash.Checked = !Mapper.Bank4000IsRam;
            actual8000UpDown.Value = Mapper.Bank8000;
            actual8000IsRam.Checked = Mapper.Bank8000IsRam;
            actual8000IsFlash.Checked = !Mapper.Bank8000IsRam;
            actualC000UpDown.Value = Mapper.BankC000;
            actualC000IsRam.Checked = Mapper.BankC000IsRam;
            actualC000IsFlash.Checked = !Mapper.BankC000IsRam;
            ramLowerLimitUpDown.Value = Mapper.RamLowerLimit;
            ramUpperLimitUpDown.Value = Mapper.RamUpperLimit;
            flashLowerLimitUpDown.Value = Mapper.FlashLowerLimit;
            flashUpperLimitUpDown.Value = Mapper.FlashUpperLimit;
            ramPage1AlwaysPresentUpDown.Value = Mapper.LowerPageAlwaysPresentAmount;
            ramPage0AlwaysPresentUpDown.Value = Mapper.UpperPageAlwaysPresentAmount;
        }

        private void bankAUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.PageA = (int)bankAUpDown.Value;
            RefreshData();
        }
        private void bankAIsRam_CheckedChanged(object sender, EventArgs e)
        {
            Mapper.PageAIsRam = bankAIsRam.Checked;
            RefreshData();
        }
        private void bankAIsFlash_CheckedChanged(object sender, EventArgs e)
        {
            Mapper.PageAIsRam = !bankAIsFlash.Checked;
            RefreshData();
        }

        private void bankBUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.PageB = (int)bankBUpDown.Value;
            RefreshData();
        }

        private void bankBIsRam_CheckedChanged(object sender, EventArgs e)
        {
            Mapper.PageBIsRam = bankBIsRam.Checked;
            RefreshData();
        }

        private void bankBIsFlash_CheckedChanged(object sender, EventArgs e)
        {
            Mapper.PageBIsRam = !bankBIsFlash.Checked;
            RefreshData();
        }

        private void bankCUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.PageC = (int)bankCUpDown.Value;
            RefreshData();
        }

        private void bootModeBox_CheckedChanged(object sender, EventArgs e)
        {
            Mapper.BootMode = bootModeBox.Checked;
            RefreshData();
        }

        private void mode1Box_CheckedChanged(object sender, EventArgs e)
        {
            if (mode1Box.Checked)
                Mapper.MemoryMappingMode = 1;
            else
                Mapper.MemoryMappingMode = 0;
            RefreshData();
        }

        private void ramTypeUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.RamType = (int)ramTypeUpDown.Value;
            RefreshData();
        }

        private void flashTypeUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.FlashType = (int)flashTypeUpDown.Value;
            RefreshData();
        }

        private void ramLowerLimitUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.RamLowerLimit = (int)ramLowerLimitUpDown.Value;
            RefreshData();
        }

        private void ramUpperLimitUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.RamUpperLimit = (int)ramUpperLimitUpDown.Value;
            RefreshData();
        }

        private void flashLowerLimitUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.FlashLowerLimit = (int)flashLowerLimitUpDown.Value;
            RefreshData();
        }

        private void flashUpperLimitUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.FlashUpperLimit = (int)flashUpperLimitUpDown.Value;
            RefreshData();
        }

        private void ramPage1AlwaysPresentUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.LowerPageAlwaysPresentAmount = (int)ramPage1AlwaysPresentUpDown.Value;
            RefreshData();
        }

        private void ramPage0AlwaysPresentUpDown_ValueChanged(object sender, EventArgs e)
        {
            Mapper.UpperPageAlwaysPresentAmount = (int)ramPage0AlwaysPresentUpDown.Value;
            RefreshData();
        }

        private void MemoryDebugSe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Master.Calculator.ExecutionFinished -= Calculator_ExecutionFinished;
        }




    }
}
