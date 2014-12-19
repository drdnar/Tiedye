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
            RefreshData();
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        StringBuilder str = new StringBuilder();

        public void RefreshData()
        {
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

            int pos = (Lcd.LogPtr - 1) & Lcd.LogPtr;
            int asdf = ColorLcd.LogSize;
            str.Clear();
            for (int i = 0; i < asdf; i++)
            {
                str.Append("(");
                str.Append(Lcd.LogData[pos, 1]);
                str.Append(", ");
                str.Append(Lcd.LogData[pos, 2]);
                str.Append(") <= ");
                str.Append(Lcd.LogData[pos, 0]);
                str.AppendLine();
                pos = (pos - 1) & ColorLcd.LogMask;
            }
            logTextBox.Text = str.ToString();
        }

        private void ColorLcdDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Master.ExecutionFinished -= Calculator_ExecutionFinished;
            Master.UpdateData -= Calculator_ExecutionFinished;
        }
    }
}
