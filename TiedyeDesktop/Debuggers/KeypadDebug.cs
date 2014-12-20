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
            //Master.Calculator.ExecutionFinished += Calculator_ExecutionFinished;
            Master.UpdateData += Calculator_ExecutionFinished;
            foreach (Tiedye.Hardware.Keypad.KeyScanCode k in Enum.GetValues(typeof(Tiedye.Hardware.Keypad.KeyScanCode)).Cast<Tiedye.Hardware.Keypad.KeyScanCode>())
                keysComboBox.Items.Add(k);
            /*keysComboBox.Items.AddRange(
                Enum.GetValues(typeof(Tiedye.Hardware.Keypad.KeyScanCode)).Cast<Tiedye.Hardware.Keypad.KeyScanCode>()
            );*/
                //Ke)keysComboBox.Items.Add()
        }

        void Calculator_ExecutionFinished(object sender, EventArgs e)
        {
            RefreshData();
        }

        bool RefreshingData = false;

        public void RefreshData()
        {
            if (RefreshingData)
                return;
            RefreshingData = true;
            group0TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xFE), 2);
            group1TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xFD), 2);
            group2TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xBF), 2);
            group3TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xF7), 2);
            group4TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xEF), 2);
            group5TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xDF), 2);
            group6TextBox.Text = Convert.ToString(Keypad.ReadGroup(0xBF), 2);
            onKeyCheckBox.Checked = Keypad.OnKey;
            interruptCheckBox.Checked = Keypad.HasInterrupt;
            interruptEnableCheckBox.Checked = Keypad.OnInterruptEnable;
            maskTextBox.Text = Convert.ToString(Keypad.CurrentGroup, 2) + " (" + Keypad.CurrentGroup.ToString("X2") + ")";
            if (keysComboBox.SelectedItem != null)
                keyPressedCheckBox.Checked = Keypad.TestKey((Tiedye.Hardware.Keypad.KeyScanCode)keysComboBox.SelectedItem);
            RefreshingData = false;
        }

        private void keysComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void keyPressedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            if (keysComboBox.SelectedItem == null)
                return;
            if (keyPressedCheckBox.Checked)
                Keypad.SetKey((Tiedye.Hardware.Keypad.KeyScanCode)keysComboBox.SelectedItem);
            else
                Keypad.ResetKey((Tiedye.Hardware.Keypad.KeyScanCode)keysComboBox.SelectedItem);
            RefreshData();
        }

        private void interruptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Keypad.HasInterrupt = interruptCheckBox.Checked;
        }

        private void interruptEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Keypad.OnInterruptEnable = interruptEnableCheckBox.Checked;
        }

        private void onKeyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RefreshingData)
                return;
            Keypad.OnKey = onKeyCheckBox.Checked;
        }

        

    }
}
