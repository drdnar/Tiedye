using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiedyeDesktop
{
    /// <summary>
    /// Provides a control for editing blocks of memory.
    /// </summary>
    public partial class MemoryEditor : UserControl
    {
        public MemoryEditor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            
            exportDialogGroupBox.Left = this.Width / 2 - exportDialogGroupBox.Width / 2;
            exportDialogGroupBox.Top = this.Height / 2 - exportDialogGroupBox.Height / 2;

            //dataPanel.KeyHappens = new Debuggers.HexEditorPanel.KeyEventThing(DoMaskedKeys);
            memoryEditorPanel.PagedAddress = pagedAddressBox.Checked;

            setMetrics();
        }

        #region Properties
        
        /// <summary>
        /// True to make address only four hex characters long.
        /// </summary>
        public bool SixteenBitAddresses
        {
            get
            {
                return memoryEditorPanel.SixteenBitAddresses;
            }
            set
            {
                memoryEditorPanel.SixteenBitAddresses = value;
                pagedAddressBox.Visible = !value;
            }
        }

        /// <summary>
        /// Total number of bytes to be displayed for editing.
        /// </summary>
        public int DataLength
        {
            get
            {
                return memoryEditorPanel.DataLength;
            }
            set
            {
                memoryEditorPanel.DataLength = value;
                setMetrics();
            }
        }

        /// <summary>
        /// Size of a page in paged display mode.
        /// </summary>
        public int PageSize
        {
            get
            {
                return memoryEditorPanel.PageSize;
            }
            set
            {
                memoryEditorPanel.PageSize = value;
                setMetrics();
            }
        }

        /// <summary>
        /// Base offset displayed in paged mode.
        /// For example, if set to 4000, the 0th byte will be displayed with
        /// the address 00:4000.
        /// </summary>
        public int PageDisplayOffset
        {
            get
            {
                return memoryEditorPanel.PageDisplayOffset;
            }
            set
            {
                memoryEditorPanel.PageDisplayOffset = value;
                setMetrics();
            }
        }

        /// <summary>
        /// Base address that data starts from.
        /// </summary>
        public int DataBase
        {
            get
            {
                return memoryEditorPanel.DataBase;
            }
            set
            {
                memoryEditorPanel.DataBase = value;
                setMetrics();
            }
        }

        /// <summary>
        /// Displayed base address that data starts from.
        /// In paged mode, this affects the page number displayed.
        /// </summary>
        public int DataDisplayedBase
        {
            get
            {
                return memoryEditorPanel.DataDisplayedBase;
            }
            set
            {
                memoryEditorPanel.DataDisplayedBase = value;
                setMetrics();
            }
        }


        public MemoryEditorPanel.ReadByte ReadAByte
        {
            get
            {
                return memoryEditorPanel.ReadAByte;
            }
            set
            {
                memoryEditorPanel.ReadAByte = value;
            }
        }
        public MemoryEditorPanel.WriteByte WriteAByte
        {
            get
            {
                return memoryEditorPanel.WriteAByte;
            }
            set
            {
                memoryEditorPanel.WriteAByte = value;
            }
        }

        public byte[] PlainArray
        {
            get
            {
                return memoryEditorPanel.PlainArray;
            }
            set
            {
                memoryEditorPanel.PlainArray = value;
            }
        }
        public byte PlainArrayRead(object sender, int address)
        {
            return PlainArray[address];
        }

        public void PlainArrayWrite(object sender, int address, byte value)
        {
            PlainArray[address] = value;
        }

        #endregion

        /*
DataLength		Total number of bytes to be displayed for editing.
PageSize		Size of a page in paged display mode.
PageDisplayOffset	Base offset displayed in paged mode. For example, if set to 4000, the 0th byte will be displayed with the address 00:4000.
DataBase		Base address that data starts from.
DataDisplayedBase	Displayed base address that data starts from. In paged mode, this affects the page number displayed.
*/

        /// <summary>
        /// Sets the internal variables that tracks the logical size of the display area to match the visible display area.
        /// </summary>
        protected void setMetrics()
        {
            /*vScrollBar.Minimum = 0;
            vScrollBar.Maximum = memoryEditorPanel.TotalLines;
             * */
        }

        /// <summary>
        /// Fixes the focus so that the data panel gets key events.
        /// </summary>
        public void FixFocus()
        {
            dataPanel.Focus();
        }


        

        #region Misc. Event handlers
        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            /*if (vScrollBar.Value < memoryEditorPanel.TotalLines)
                memoryEditorPanel.TopLine = vScrollBar.Value;
            else
                vScrollBar.Maximum = memoryEditorPanel.TotalLines;
             */
            setMetrics();
            Refresh();
        }

        private void MemoryEditor_Resize(object sender, EventArgs e)
        {
            setMetrics();
        }

        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //dataPanel.Focus();
            DoMaskedKeys(e.KeyCode);
            base.OnKeyDown(e);
        }

        private void dataPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        // This also handles non-masked keys. . . . whatever.
        public void DoMaskedKeys(Keys k)
        {
            memoryEditorPanel.DoMaskedKeys(k);
        }

        #endregion

        private void pagedAddressBox_CheckedChanged(object sender, EventArgs e)
        {
            memoryEditorPanel.PagedAddress = pagedAddressBox.Checked;
            Refresh();
        }

        private void memoryEditorPanel_CursorMove(object sender, EventArgs e)
        {
            currentAddressBox.Text = memoryEditorPanel.CurrentAddressString;
        }


    }
}
