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
    public partial class MemoryEditorPanel : UserControl
    {
        public MemoryEditorPanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //dataPanel.Focus();
            DoMaskedKeys(e.KeyCode);
            //base.OnKeyDown(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.PageDown:
                case Keys.PageUp:
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Enter:
                    return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {

            base.OnMouseClick(e);
        }


        #region Properties
        public delegate byte ReadByte(object sender, int address);
        public delegate void WriteByte(object sender, int address, byte value);

        private bool sixteenBitAddresses = false;
        /// <summary>
        /// True to make address only four hex characters long.
        /// </summary>
        public bool SixteenBitAddresses
        {
            get
            {
                return sixteenBitAddresses;
            }
            set
            {
                sixteenBitAddresses = value;
            }
        }

        protected int dataLength = 0;
        /// <summary>
        /// Total number of bytes to be displayed for editing.
        /// </summary>
        public int DataLength
        {
            get
            {
                return dataLength;
            }
            set
            {
                dataLength = value;
                setMetrics();
            }
        }

        protected int pageSize = 0x4000;
        /// <summary>
        /// Size of a page in paged display mode.
        /// </summary>
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
                setMetrics();
            }
        }

        protected int pageDisplayOffset = 0;
        /// <summary>
        /// Base offset displayed in paged mode.
        /// For example, if set to 4000, the 0th byte will be displayed with
        /// the address 00:4000.
        /// </summary>
        public int PageDisplayOffset
        {
            get
            {
                return pageDisplayOffset;
            }
            set
            {
                pageDisplayOffset = value;
                setMetrics();
            }
        }

        protected int dataBase = 0;
        /// <summary>
        /// Base address that data starts from.
        /// </summary>
        public int DataBase
        {
            get
            {
                return dataBase;
            }
            set
            {
                dataBase = value;
                setMetrics();
            }
        }

        protected int dataDisplayedBase = 0;
        /// <summary>
        /// Displayed base address that data starts from.
        /// In paged mode, this affects the page number displayed.
        /// </summary>
        public int DataDisplayedBase
        {
            get
            {
                return dataDisplayedBase;
            }
            set
            {
                dataDisplayedBase = value;
                setMetrics();
            }
        }

        protected bool pagedAddress = false;
        /// <summary>
        /// True if addresses should be displayed in a paged format
        /// </summary>
        public bool PagedAddress
        {
            get
            {
                return pagedAddress;
            }
            set
            {
                pagedAddress = value;
            }
        }

        public ReadByte ReadAByte;
        public WriteByte WriteAByte;

        public byte[] PlainArray;
        public byte PlainArrayRead(object sender, int address)
        {
            return PlainArray[address];
        }

        public void PlainArrayWrite(object sender, int address, byte value)
        {
            PlainArray[address] = value;
        }

        #endregion

        //private Brush textColor = new SolidBrush(Color.Black);
        //private Brush highlightColor = new SolidBrush(Color.Blue);
        //private Brush highlightTextColor = new SolidBrush(Color.White);
        private Font textFont = new Font("Courier New", 9);

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
            BorderX = System.Windows.Forms.SystemInformation.BorderSize.Width + 2;
            BorderY = System.Windows.Forms.SystemInformation.BorderSize.Height + 2;
            float charWidth = TextRenderer.MeasureText("..", textFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width / 3;
            charHeight = textFont.Height;
            byteWidth = (int)(charWidth * 3);// *0.825f; // This metric appears to be font-specific.
            addrWidth = (int)(charWidth * 10);
            float width = this.Width - BorderX - BorderX - vScrollBar.Width;
            float height = this.Height - BorderY - BorderY;
            float bEndY = height + BorderY;
            bytesPerLine = (int)((width - addrWidth) / byteWidth);
            if (bytesPerLine <= 0)
                bytesPerLine = 1;
            linesPerPage = (int)((int)height / charHeight);//(int)Math.Floor((double)((int)height / charHeight));
            TotalLines = dataLength / bytesPerLine;
            OnCursorMove();
        }

        private int totalHexLines = 1;
        /// <summary>
        /// Returns the total number of lines of hex in the window
        /// </summary>
        public int TotalLines
        {
            get
            {
                return totalHexLines;
            }
            protected set
            {
                if (topLine >= value)
                    topLine = value;
                totalHexLines = value;
                vScrollBar.Maximum = value;
            }
        }

        private int topLine = 0;
        /// <summary>
        /// Top-most line visible in window
        /// </summary>
        public int TopLine
        {
            get
            {
                return topLine;
            }
            set
            {
                if (value < totalHexLines)
                {
                    topLine = value;
                    vScrollBar.Value = value;
                    Refresh();
                }
                /*else
                    throw new ArgumentOutOfRangeException("TopLine >= totalHexLines");*/
            }
        }

        /// <summary>
        /// Distance between left/right edge and start of display area.
        /// </summary>
        protected int BorderX;
        /// <summary>
        /// Distance between top/bottom edge of start of display area.
        /// </summary>
        protected int BorderY;
        /// <summary>
        /// Width, in pixels, of a single displayed byte.
        /// </summary>
        protected int byteWidth;
        /// <summary>
        /// Height, in pixels, of a single line of text.
        /// </summary>
        protected int charHeight;
        /// <summary>
        /// Width, in pixels, of the address display area.
        /// </summary>
        protected int addrWidth;
        //float pageWidth;
        //float pageHeight;

        /// <summary>
        /// Number of visible bytes on each line of hex.
        /// </summary>
        int bytesPerLine = 16;
        /// <summary>
        /// Number of lines visible on a page.
        /// </summary>
        int linesPerPage = 16;

        /// <summary>
        /// Current column of the editing cursor.
        /// </summary>
        int CursorX = 0;
        /// <summary>
        /// Current row of the editing cursor.
        /// </summary>
        int CursorY = 0;
        /// <summary>
        /// Toggle that alternates between inverse and normal.
        /// </summary>
        bool CursorToggle = false;


        /// <summary>
        /// Does what it says.
        /// </summary>
        /// <param name="g"></param>
        protected void DrawCursor(Graphics g)
        {
            if (ReadAByte == null || WriteAByte == null)
                return;
            if (dataLength < 1)
                return;
            if (EditingMode)
            {
                // TODO: Stuff
                ValidateCursorLocation();
                int loc = GetCurrentAddress();
                Point location = new Point(BorderX + addrWidth + byteWidth * CursorX, BorderY + charHeight * CursorY);
                string text;
                if (FirstNibble)
                    text = "--";
                else
                    text = EnteredNibble.ToString("X1") + "-";
                if (!CursorToggle)
                    TextRenderer.DrawText(g, text, textFont, location, Color.White, Color.Blue, TextFormatFlags.NoPadding);
                else
                    TextRenderer.DrawText(g, text, textFont, location, Color.Black, TextFormatFlags.NoPadding);

            }
            else
            {
                ValidateCursorLocation();
                int loc = GetCurrentAddress();
                Point location = new Point(BorderX + addrWidth + byteWidth * CursorX, BorderY + charHeight * CursorY);
                if (!CursorToggle)
                    TextRenderer.DrawText(g, ReadAByte(this, loc).ToString("X2"), textFont, location, Color.White, Color.Blue, TextFormatFlags.NoPadding);
                else
                    TextRenderer.DrawText(g, ReadAByte(this, loc).ToString("X2"), textFont, location, Color.Black, TextFormatFlags.NoPadding);
                //currentAddressBox.Text = GetCurrentAddressString();
            }
        }


        /// <summary>
        /// Returns the linear address of the byte under the cursor.
        /// </summary>
        /// <returns></returns>
        protected int GetCurrentAddress()
        {
            return dataBase + (topLine + CursorY) * bytesPerLine + CursorX;
        }

        public string CurrentAddressString
        {
            get
            {
                return GetAddressString(GetCurrentAddress());
            }
        }

        protected string GetAddressString(int addr)
        {
            if (SixteenBitAddresses)
                return addr.ToString("X4");
            if (pagedAddress)
            {
                int page = addr / pageSize;
                int offset = addr % pageSize;
                return page.ToString("X3") + ":" + offset.ToString("X4");
            }
            return addr.ToString("X6");
        }

        /// <summary>
        /// Ensures that the cursor's location is valid.
        /// </summary>
        protected void ValidateCursorLocation()
        {
            bool refresh = false;
            // Out-of-bounds conditions
            if (CursorX < 0)
            {
                CursorX = 0;
                refresh = true;
                //throw new Exception();
            }
            else if (CursorX >= bytesPerLine)
            {
                CursorX = bytesPerLine - 1;
                refresh = true;
                //throw new Exception();
            }
            if (CursorY < 0)
            {
                CursorY = 0;
                refresh = true;
                //throw new Exception();
            }
            else if (CursorY > linesPerPage)
            {
                CursorY = linesPerPage - 1;
                refresh = true;
                //throw new Exception();
            }
            // Test if cursor is past end of data
            while (GetCurrentAddress() >= dataBase + dataLength)
            {
                CursorX--;
                if (CursorX < 0)
                {
                    CursorX = bytesPerLine - 1;
                    CursorY--;
                    if (CursorY < 0)
                    {
                        /*if (vScrollBar.Value == vScrollBar.Minimum)
                            return; // I don't know. This shouldn't happen.
                        vScrollBar.Value--;*/
                        /*if (topLine > firstLine)
                            topLine--;
                        else
                            return;*/
                        if (topLine == firstLine)
                            return;
                        topLine--;
                        OnCursorMove();
                        CursorY = 0;
                        CursorX = bytesPerLine - 1;
                    }
                }
                refresh = true;
            }
            if (refresh)
            {
                ResetCursorTimer();
                OnCursorMove();
                //Refresh();
            }
        }

        public event EventHandler CursorMove;
        protected virtual void OnCursorMove()
        {
            if (CursorMove != null)
                CursorMove(this, null);
            vScrollBar.Value = TopLine;
        }

	// Minor performance optimization: Cache the StringBuilder so it doesn't get garbage collected after EVERY frame.
	// Might cause race condition if OnPaint is called by two different threads at the same time,
	// but I think the Framework will prevent that.  (It'd be weird, anyway.)
	StringBuilder onPaintStringBuilder = new StringBuilder(3 * 64 + 10);
        protected override void OnPaint(PaintEventArgs e)
        {
            if (ReadAByte == null || WriteAByte == null)
                return;
            if (dataLength < 1)
                return;
            Graphics g = e.Graphics;
            Point location = new Point(0, BorderY - (int)charHeight);
            int line = 0;
            StringBuilder str = onPaintStringBuilder;//new StringBuilder(3 * bytesPerLine + 20);
            for (int i = dataBase + topLine * bytesPerLine; i < dataBase + dataLength; )
            {
                location.X = BorderX;
                location.Y += charHeight;
                if (line++ > linesPerPage - 1)
                    break;
                str.Clear();
                string s = GetAddressString(i);
                str.Append(s);
                str.Append(":");
                int l = s.Length;
                for (; l < 9; l++)
                    str.Append(" ");
                //str.Append(":    ");
                //str.Append(":   ");
                for (int j = 0; j < bytesPerLine && i < dataBase + dataLength; j++)
                {
                    if (line - 1 == CursorY && j == CursorX)
                    {
                        i++;
                        str.Append("   ");
                        //str.Append(CursorY.ToString("X1") + " " + line.ToString("X1"));
                    }
                    else
                    {
                        str.Append(ReadAByte(this, i++).ToString("X2"));
                        str.Append(" ");
                    }
                }

                TextRenderer.DrawText(g, str.ToString(), textFont, location, Color.Black, TextFormatFlags.NoPadding);
            }
            DrawCursor(g);
            base.OnPaint(e);

        }

        /// <summary>
        /// Resets the cursor blinking timer and also sets it back to the more visible inverse state.
        /// </summary>
        protected void ResetCursorTimer()
        {
            CursorToggle = false;
            cursorTimer.Stop();
            cursorTimer.Start();
            OnCursorMove();
            Refresh();
        }


        /// <summary>
        /// True if the user is current editing a byte. If true, prohibit moving the cursor and scrolling.
        /// </summary>
        protected bool EditingMode
        {
            get
            {
                return editingMode;
            }
            set
            {
                editingMode = value;
                //vScrollBar.Enabled = !value;
                ResetCursorTimer();
            }
        }
        private bool editingMode = false;

        /// <summary>
        /// Caches the first nibble entered when entering a byte.
        /// </summary>
        protected int EnteredNibble = 0;

        /// <summary>
        /// True if waiting for first nibble; false if the next entered nibble will terminate entry.
        /// </summary>
        protected bool FirstNibble = false;

        protected int firstLine = 0;

        // This also handles non-masked keys. . . . whatever.
        public void DoMaskedKeys(Keys k)
        {
            switch (k)
            {
                case Keys.PageUp:
                    if (EditingMode)
                        break;
                    if (topLine - linesPerPage >= firstLine)
                    {
                        topLine -= linesPerPage;
                        ResetCursorTimer(); // Implicit redraw.
                    }
                    else if (topLine > firstLine)
                    {
                        topLine = firstLine;
                        ResetCursorTimer();
                    }
                    break;
                case Keys.PageDown:
                    if (EditingMode)
                        break;
                    if (topLine + linesPerPage < totalHexLines)//firstLine)
                    {
                        topLine += linesPerPage;
                        ResetCursorTimer();
                    }
                    else if (topLine < totalHexLines)//firstLine)
                    {
                        topLine = totalHexLines - 1;
                        ResetCursorTimer();
                    }
                    break;
                case Keys.Up:
                    if (EditingMode)
                        break;
                    if (CursorY > 0)
                    {
                        CursorY--;
                        ResetCursorTimer();
                    }
                    else if (topLine > firstLine)
                    {
                        topLine--;
                        ResetCursorTimer();
                    }
                    break;
                case Keys.Down:
                    if (EditingMode)
                        break;
                    if (CursorY < linesPerPage - 1)
                    {
                        CursorY++;
                        ResetCursorTimer();
                    }
                    else if (topLine < totalHexLines)
                    {
                        topLine++;
                        ResetCursorTimer();
                    }
                    break;
                case Keys.Left:
                    if (EditingMode)
                        break;
                    if (CursorX > 0)
                    {
                        CursorX--;
                        ResetCursorTimer();
                    }
                    else if (CursorY > 0)
                    {
                        CursorX = bytesPerLine - 1;
                        CursorY--;
                        ResetCursorTimer();
                    }
                    else if (topLine > firstLine)
                    {
                        CursorX = bytesPerLine - 1;
                        topLine--;
                        ResetCursorTimer();
                    }
                    break;
                case Keys.Right:
                    if (EditingMode)
                        break;
                    if (CursorX < bytesPerLine - 1)
                    {
                        CursorX++;
                        ResetCursorTimer();
                    }
                    else if (CursorY < linesPerPage)// - 1)
                    {
                        CursorX = 0;
                        CursorY++;
                        ResetCursorTimer();
                    }
                    else if (topLine < totalHexLines)//firstLine)
                    {
                        CursorX = 0;
                        topLine++;
                        ResetCursorTimer();
                    }
                    break;
                case Keys.Home:
                    if (EditingMode)
                        break;
                    CursorX = 0;
                    ResetCursorTimer();
                    break;
                case Keys.End:
                    if (EditingMode)
                        break;
                    CursorX = bytesPerLine - 1;
                    ResetCursorTimer();
                    break;
                case Keys.Enter:
                    if (!EditingMode && WriteAByte != null)
                    {
                        EditingMode = FirstNibble = true;
                    }
                    break;
                case Keys.Escape:
                    if (EditingMode)
                    {
                        EditingMode = false;
                    }
                    break;
                case Keys.A:
                case Keys.B:
                case Keys.C:
                case Keys.D:
                case Keys.E:
                case Keys.F:
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    if (EditingMode)
                    {
                        if (FirstNibble)
                        {
                            FirstNibble = false;
                            EnteredNibble = HexKeys[k];
                            ResetCursorTimer();
                        }
                        else
                        {
                            int b = EnteredNibble << 4 | HexKeys[k];
                            WriteAByte(this, GetCurrentAddress(), (byte)b);
                            EditingMode = false;
                            ResetCursorTimer();
                        }
                    }
                    break;
            }
            ValidateCursorLocation();
        }
        Dictionary<Keys, int> HexKeys = new Dictionary<Keys, int>
        {
            {Keys.A, 10},
            {Keys.B, 11},
            {Keys.C, 12},
            {Keys.D, 13},
            {Keys.E, 14},
            {Keys.F, 15},
            {Keys.D0, 0},
            {Keys.D1, 1},
            {Keys.D2, 2},
            {Keys.D3, 3},
            {Keys.D4, 4},
            {Keys.D5, 5},
            {Keys.D6, 6},
            {Keys.D7, 7},
            {Keys.D8, 8},
            {Keys.D9, 9},
            {Keys.NumPad0, 0},
            {Keys.NumPad1, 1},
            {Keys.NumPad2, 2},
            {Keys.NumPad3, 3},
            {Keys.NumPad4, 4},
            {Keys.NumPad5, 5},
            {Keys.NumPad6, 6},
            {Keys.NumPad7, 7},
            {Keys.NumPad8, 8},
            {Keys.NumPad9, 9},
        };

        private void MemoryEditorPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int row;
            int col;
            if (e.X < BorderX + addrWidth)
                return;
            if (e.Y < BorderY)
                return;
            col = (e.X - /*BorderX -*/ addrWidth) / byteWidth;
            row = (e.Y /*- BorderY*/) / charHeight;
            if (row >= linesPerPage)
                return;
            if (col >= bytesPerLine)
                return;
            CursorX = col;
            CursorY = row;
            ResetCursorTimer();
        }

        private void cursorTimer_Tick(object sender, EventArgs e)
        {
            CursorToggle = !CursorToggle;
            Refresh();
        }

        private void MemoryEditorPanel_Resize(object sender, EventArgs e)
        {
            setMetrics();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            TopLine = vScrollBar.Value;
            ResetCursorTimer();
        }
    }
}
