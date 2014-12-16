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

        public KeypadDebug(Ti8xCalculator master)
        {
            Master = master;
            InitializeComponent();
        }
    }
}
