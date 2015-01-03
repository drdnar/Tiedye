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
    public partial class ColorLcdDebug : Form
    {
        Ti8xCalculator Master;
        ColorLcd Lcd;
        
        public ColorLcdDebug(Ti8xCalculator master)
        {
            InitializeComponent();
            Master = master;
            Lcd = ((Ti84PlusCSe)(Master.Calculator)).Lcd;
            //Master.ExecutionFinished += Calculator_ExecutionFinished;
            Master.UpdateData += Calculator_ExecutionFinished;
            traceCountUpDown.Minimum = 1;
            traceCountUpDown.Maximum = ColorLcd.LogSize;
            RefreshData();
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            if (Master.Executing)
                return;
            RefreshData();
        }

        StringBuilder str = new StringBuilder();

        //bool RefreshingData = false;

        public void RefreshData()
        {
            /*if (RefreshingData)
                return;
            RefreshingData = true;*/
            onCheckBox.Checked = true;
            panicCheckBox.Checked = Lcd.PanicMode;
            upDownRadio.Checked = !Lcd.MajorDirectionIsHorizontal;
            leftRightRadio.Checked = Lcd.MajorDirectionIsHorizontal;
            upCheckBox.Checked = Lcd.VerticalWriteDirection == -1;
            leftCheckBox.Checked = Lcd.HorizontalWriteDirection == -1;
            cursorRowUpDown.Value = Lcd.CursorRow;
            cursorColumnUpDown.Value = Lcd.CursorColumn;
            windowTopUpDown.Value = Lcd.WindowVerticalStart;
            windowBottomUpDown.Value = Lcd.WindowVerticalEnd;
            windowLeftUpDown.Value = Lcd.WindowHorizontalStart;
            windowRightUpDown.Value = Lcd.WindowHorizontalEnd;
            traceEnableCheckBox.Checked = Lcd.LogEnable;

            int pos = (Lcd.LogPtr - 1) & ColorLcd.LogMask;
            int asdf = (int)traceCountUpDown.Value;
            str.Clear();
            for (int i = 0; i < asdf; i++)
            {
                int reg = Lcd.LogData[pos, 0];
                int val = Lcd.LogData[pos, 1];
                int col = Lcd.LogData[pos, 2];
                int row = Lcd.LogData[pos, 3];
                if ((reg & 0xFF) == 0x22)
                {
                    str.Append(val.ToString("X4"));
                    if ((reg & 0x8000) != 0)
                        str.Append(" => (");
                    else
                        str.Append(" <= (");
                    str.Append(col);
                    str.Append(", ");
                    str.Append(row);
                    str.Append(")");
                }
                else
                {
                    str.Append(val.ToString("X4"));
                    if ((reg & 0x8000) != 0)
                        str.Append(" => ");
                    else
                        str.Append(" <= ");
                    str.Append((reg & 0xFF).ToString("X2"));
                    str.Append(" (");
                    str.Append(((ColorLcd.RegisterName)(reg & 0xFF)).ToString());
                    str.Append(")");
                }
                str.AppendLine();
                pos = (pos - 1) & ColorLcd.LogMask;
            }
            logTextBox.Text = str.ToString();
            //RefreshingData = false;
        }

        private void ColorLcdDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Master.ExecutionFinished -= Calculator_ExecutionFinished;
            Master.UpdateData -= Calculator_ExecutionFinished;
        }

        private void traceEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Lcd.LogEnable = traceEnableCheckBox.Checked;
        }

        private void traceCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
