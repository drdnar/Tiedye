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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<Ti8xCalculator> Calculators = new List<Ti8xCalculator>();

        private Ti8xCalculator selectedCalculator = null;
        public Ti8xCalculator SelectedCalculator
        {
            get
            {
                return selectedCalculator;
            }
            set
            {
                selectedCalculator = value;
                if (value.Paused)
                    playToolStripMenuItem.Text = "&Play";
                else
                    playToolStripMenuItem.Text = "&Pause";
            }
        }

        public int CalculatorCounter = 1;

        private void newCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ti8xCalculator blah = new Ti8xCalculator(this, new Ti84PlusCSe());
            blah.CalculatorName = "Calc " + (CalculatorCounter++).ToString();
            blah.MdiParent = this;
            blah.Show();
            Calculators.Add(blah);
            SelectedCalculator = blah;
        }

        private void cPUDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.CpuDebug == null)
            {
                SelectedCalculator.CpuDebug = new CpuDebug(SelectedCalculator);
                SelectedCalculator.CpuDebug.MdiParent = this;
                SelectedCalculator.CpuDebug.Show();
            }
            SelectedCalculator.CpuDebug.Focus();
        }

        private void memoryDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.Calculator is Ti84PlusCSe)
            {
                if (SelectedCalculator.MemoryDebugSe == null)
                {
                    SelectedCalculator.MemoryDebugSe = new MemoryDebugSe(SelectedCalculator);
                    SelectedCalculator.MemoryDebugSe.MdiParent = this;
                    SelectedCalculator.MemoryDebugSe.Show();
                }
                SelectedCalculator.MemoryDebugSe.Focus();
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.Paused)
                SelectedCalculator.Play();
            else
                SelectedCalculator.Pause();
        }

        private void breakPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.Breakpoints == null)
            {
                SelectedCalculator.Breakpoints = new Breakpoints(SelectedCalculator.Calculator.Cpu);
                SelectedCalculator.Breakpoints.MdiParent = this;
                SelectedCalculator.Breakpoints.Show();
            }
            SelectedCalculator.Breakpoints.Focus();
        }

        private void hWSchedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.SchedulerDebug == null)
            {
                SelectedCalculator.SchedulerDebug = new SchedulerDebug(SelectedCalculator.Calculator);
                SelectedCalculator.SchedulerDebug.MdiParent = this;
                SelectedCalculator.SchedulerDebug.Show();
            }
            SelectedCalculator.SchedulerDebug.Focus();
        }

        private void lCDDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (!(SelectedCalculator.Calculator is Ti84PlusCSe))
                return;
            if (SelectedCalculator.ColorLcdDebug == null)
            {
                SelectedCalculator.ColorLcdDebug = new ColorLcdDebug(SelectedCalculator.Calculator as Ti84PlusCSe);
                SelectedCalculator.ColorLcdDebug.MdiParent = this;
                SelectedCalculator.ColorLcdDebug.Show();
            }
            SelectedCalculator.ColorLcdDebug.Focus();
        }

        private void keyboardDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.KeypadDebug == null)
            {
                SelectedCalculator.KeypadDebug = new KeypadDebug(SelectedCalculator);
                SelectedCalculator.KeypadDebug.MdiParent = this;
                SelectedCalculator.KeypadDebug.Show();
            }
            SelectedCalculator.KeypadDebug.Focus();
        }

        private void executionHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.ExecutionHistory == null)
            {
                SelectedCalculator.ExecutionHistory = new ExecutionHistory(SelectedCalculator);
                SelectedCalculator.ExecutionHistory.MdiParent = this;
                SelectedCalculator.ExecutionHistory.Show();
            }
            SelectedCalculator.ExecutionHistory.Focus();
        }
    }
}
