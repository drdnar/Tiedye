using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiedyeDesktop
{
    public partial class KeypadDebug : Form
    {
        public Ti8xCalculator Master;

        protected Tiedye.Hardware.Keypad Keypad
        {
            get
            {
                return Master.Calculator.Keypad;
            }
        }

        public KeypadDebug(Ti8xCalculator master)
        {
            Master = master;
            InitializeComponent();
            RefreshData();
            Master.Calculator.ExecutionFinished += Calculator_ExecutionFinished;
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            group0TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xFE), 2);
            group1TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xFD), 2);
            group2TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xBF), 2);
            group3TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xF7), 2);
            group4TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xEF), 2);
            group5TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xDF), 2);
            group6TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xBF), 2);
            onKeyCheckBox.Checked = Keypad.OnKey;
            maskTextBox.Text = Convert.ToString(Keypad.CurrentGroup, 2) + " (" + Keypad.CurrentGroup.ToString("X2") + ")";
        }
    }
}
