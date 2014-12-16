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
            CpuDebug blah = new CpuDebug(SelectedCalculator);
            blah.MdiParent = this;
            blah.Show();
        }

        private void memoryDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (SelectedCalculator.Calculator is Ti84PlusCSe)
            {
                MemoryDebugSe blah = new MemoryDebugSe(SelectedCalculator);
                blah.MdiParent = this;
                blah.Show();
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
            Breakpoints blah = new Breakpoints(SelectedCalculator.Calculator.Cpu);
            blah.MdiParent = this;
            blah.Show();
        }

        private void hWSchedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            SchedulerDebug blah = new SchedulerDebug(SelectedCalculator.Calculator);
            blah.MdiParent = this;
            blah.Show();
        }

        private void lCDDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            if (!(SelectedCalculator.Calculator is Ti84PlusCSe))
                return;
            ColorLcdDebug blah = new ColorLcdDebug(SelectedCalculator.Calculator as Ti84PlusCSe);
            blah.MdiParent = this;
            blah.Show();
        }

        private void keyboardDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCalculator == null)
                return;
            KeypadDebug blah = new KeypadDebug(SelectedCalculator);
            blah.MdiParent = this;
            blah.Show();
        }
    }
}
