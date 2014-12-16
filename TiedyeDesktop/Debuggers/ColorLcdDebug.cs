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
        Ti84PlusCSe Master;
        ColorLcd Lcd;
        
        public ColorLcdDebug(Ti84PlusCSe master)
        {
            InitializeComponent();
            Master = master;
            Lcd = Master.Lcd;
            Master.ExecutionFinished += Calculator_ExecutionFinished;
            RefreshData();
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

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
        }

        private void ColorLcdDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            Master.ExecutionFinished -= Calculator_ExecutionFinished;
        }
    }
}
