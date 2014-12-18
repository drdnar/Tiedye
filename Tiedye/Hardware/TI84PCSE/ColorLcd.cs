using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Hardware
{
    public class ColorLcd : Lcd
    {
        public override int Height
        {
            get
            {
                return 240;
            }
        }

        public override int Width
        {
            get
            {
                return 320;
            }
        }

        public int[,] Data = new int[320, 240];

        public override int this[int x, int y]
        {
            get
            {
                /*54321 098765 43210
                bbbbb gggggg rrrrr*/
                int r, g, b;
                int p = Data[x, y];
                r = ((p & 0x1F) <<  3);
                if ((r & 8) != 0)
                    r |= 7;
                g = ((p >> 5) & 0x3F) << 2;
                if ((g & 4) != 0)
                    g |= 3;
                b = ((p >> 11) & 0x1F) << 3;
                if ((b & 8) != 0)
                    b |= 7;
                return (b << 16) | (g << 8) | (r);
                //return Data[x, y];
            }
            set
            {
                // TODO: Actually implement this
                Data[x, y] = value;
            }
        }

        public bool PanicMode = false;

        public byte ByteBuffer = 0;

        public bool WaitingForByte = false;
        public bool ThreeBytes = false;

        public bool WritingData = false;

        /// <summary>
        /// Raw internal value of currently selected register for data.
        /// </summary>
        public int CurrentRegister = 0;
        /// <summary>
        /// Returns the currently selected register.
        /// </summary>
        /// <returns></returns>
        public byte GetCurrentRegister()
        {
            if (WritingData)
            {
                PanicMode = true;
                return 0;
            }
            if (!WaitingForByte)
            {
                WaitingForByte = true;
                return 0;
            }
            WaitingForByte = false;
            return (byte)CurrentRegister;
        }

        public int ResetCount = 0;

        public void SetCurrentRegister(byte value)
        {
            if (value != 0)
                ResetCount = 0;
            else
            {
                ResetCount++;
                if (ResetCount > 3)
                {
                    WaitingForByte = false;
                    WritingData = false;
                    return;
                }
            }
            if (WaitingForByte && !WritingData)
            {
                PanicMode = true;
                return;
            }
            if (!WaitingForByte)
            {
                WaitingForByte = true;
                WritingData = true;
                // And do nothing.
                return;
            }
            CurrentRegister = value;
            WaitingForByte = false;
            WritingData = false;
        }

        public byte ReadData()
        {
            if (WritingData)
            {
                PanicMode = true;
                return 0;
            }
            if (WaitingForByte)
            {
                WaitingForByte = false;
                return ByteBuffer;
            }
            WaitingForByte = true;
            switch (CurrentRegister)
            {
                case 0:
                    ByteBuffer = 0x35;
                    return 0x93;
                case 3: // Entry Mode
                    ByteBuffer = (byte)(RegEntryMode & 0xFF);
                    return (byte)(RegEntryMode >> 8);
                case 0x20: // Cursor Row
                    ByteBuffer = (byte)(CursorRow & 0xFF);
                    return (byte)(CursorRow >> 8);
                case 0x21: // Cursor Column
                    ByteBuffer = (byte)(CursorColumn & 0xFF);
                    return (byte)(CursorColumn >> 8);
                case 0x22: // GRAM Buffer
                    int r = GramBuffer;
                    GramBuffer = Data[CursorColumn, CursorRow];
                    IncCursor();
                    ByteBuffer = (byte)(r & 0xFF);
                    return (byte)(r>> 8);
                case 0x50: // Window Vertical Start
                    ByteBuffer = (byte)(WindowVerticalStart & 0xFF);
                    return (byte)(WindowVerticalStart >> 8);
                case 0x51: // Window Vertical End
                    ByteBuffer = (byte)(WindowVerticalEnd & 0xFF);
                    return (byte)(WindowVerticalEnd >> 8);
                case 0x52: // Window Horizontal Start
                    ByteBuffer = (byte)(WindowHorizontalStart & 0xFF);
                    return (byte)(WindowHorizontalStart >> 8);
                case 0x53: // Window Horizontal End
                    ByteBuffer = (byte)(WindowHorizontalEnd & 0xFF);
                    return (byte)(WindowHorizontalEnd >> 8);
            }
            return 0;
        }

        public const int LogSize = 4096;//256;

        public const int LogMask = 0xFFF;//0xFF;

        public ushort[,] LogData = new ushort[LogSize, 4];
        
        public int LogPtr = 0;

        public bool LogEnable = false;
        
        public void WriteData(byte value)
        {
            if (WaitingForByte && !WritingData)
            {
                PanicMode = true;
                return;
            }
            if (!WaitingForByte)
            {
                ByteBuffer = value;
                WritingData = true;
                WaitingForByte = true;
                return;
            }
            ushort val = (ushort)(value | (ByteBuffer << 8));
            switch (CurrentRegister)
            {
                case 3: // Entry Mode
                    RegEntryMode = val;
                    break;
                case 0x20: // Cursor Row
                    CursorRow = val;
                    break;
                case 0x21: // Cursor Column
                    CursorColumn = val;
                    break;
                case 0x22: // GRAM Buffer
                    if (LogEnable)
                    {
                        LogData[LogPtr, 0] = val;
                        LogData[LogPtr, 1] = (ushort)CursorColumn;
                        LogData[LogPtr, 2] = (ushort)CursorRow;
                        LogPtr = (LogPtr + 1) & LogMask;
                    }
                    Data[CursorColumn, CursorRow] = val;
                    IncCursor();
                    break;
                case 0x50: // Window Vertical Start
                    WindowVerticalStart = val;
                    break;
                case 0x51: // Window Vertical End
                    WindowVerticalEnd = val;
                    break;
                case 0x52: // Window Horizontal Start
                    WindowHorizontalStart = val;
                    break;
                case 0x53: // Window Horizontal End
                    WindowHorizontalEnd = val;
                    break;
            }
            WaitingForByte = false;
            WritingData = false;

        }


        #region LR:03 Entry Mode
        public bool MajorDirectionIsHorizontal = false;
        public int HorizontalWriteDirection = 1;
        public int VerticalWriteDirection = 1;
        public bool BGR = true;
        public bool TRI = false;
        public bool DFM = false;

        public ushort RegEntryMode
        {
            get
            {
                int ret = 0;
                if (MajorDirectionIsHorizontal)
                    ret |= 1 << 3;
                if (VerticalWriteDirection == 1)
                    ret |= 1 << 4;
                if (HorizontalWriteDirection == 1)
                    ret |= 1 << 5;
                if (BGR)
                    ret |= 1 << 12;
                if (TRI)
                    ret |= 1 << 14;
                if (DFM)
                    ret |= 1 << 15;
                return (ushort)ret;
            }
            set
            {
                MajorDirectionIsHorizontal = (value & (1 << 3)) != 0;
                VerticalWriteDirection = (value & (1 << 4)) != 0 ? 1 : -1;
                HorizontalWriteDirection = (value & (1 << 5)) != 0 ? 1 : -1;
                BGR = (value & (1 << 12)) != 0;
                TRI = (value & (1 << 14)) != 0;
                DFM = (value & (1 << 15)) != 0;
            }
        }
        #endregion



        

        public int CursorRow = 0;
        public int CursorColumn = 0;
        public int GramBuffer = 0;
        public void IncCursor()
        {
            if (MajorDirectionIsHorizontal)
            {
                CursorColumn += HorizontalWriteDirection;
                if (HorizontalWriteDirection == 1 && CursorColumn > WindowHorizontalEnd)
                {
                    CursorColumn = WindowHorizontalStart;
                    CursorRow += VerticalWriteDirection;
                    if (VerticalWriteDirection == 1 && CursorRow > WindowVerticalEnd)
                        CursorRow = WindowVerticalStart;
                    else if (VerticalWriteDirection == -1 && CursorRow < WindowVerticalStart)
                        CursorRow = WindowVerticalEnd;
                }
                else if (HorizontalWriteDirection == -1 && CursorColumn < WindowHorizontalStart)
                {
                    CursorColumn = WindowHorizontalEnd;
                    CursorRow += VerticalWriteDirection;
                    if (VerticalWriteDirection == 1 && CursorRow > WindowVerticalEnd)
                        CursorRow = WindowVerticalStart;
                    else if (VerticalWriteDirection == -1 && CursorRow < WindowVerticalStart)
                        CursorRow = WindowVerticalEnd;
                }
            }
            else
            {
                CursorRow += VerticalWriteDirection;
                if (VerticalWriteDirection == 1 && CursorRow > WindowVerticalEnd)
                {
                    CursorRow = WindowVerticalStart;
                    CursorColumn += HorizontalWriteDirection;
                    if (HorizontalWriteDirection == 1 && CursorColumn > WindowHorizontalEnd)
                        CursorColumn = WindowHorizontalStart;
                    else if (HorizontalWriteDirection == -1 && CursorColumn < WindowHorizontalStart)
                        CursorColumn = WindowHorizontalEnd;
                }
                else if (VerticalWriteDirection == -1 && CursorRow < WindowVerticalStart)
                {
                    CursorRow = WindowVerticalEnd;
                    CursorColumn += HorizontalWriteDirection;
                    if (HorizontalWriteDirection == 1 && CursorColumn > WindowHorizontalEnd)
                        CursorColumn = WindowHorizontalStart;
                    else if (HorizontalWriteDirection == -1 && CursorColumn < WindowHorizontalStart)
                        CursorColumn = WindowHorizontalEnd;
                }
            }
        }


        public int WindowHorizontalStart = 0;
        public int WindowHorizontalEnd = 319;
        public int WindowVerticalStart = 0;
        public int WindowVerticalEnd = 239;

    }
}
