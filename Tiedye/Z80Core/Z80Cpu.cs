using System;

namespace Tiedye.Z80Core
{
    /// <summary>
    /// Based on the YAZE-AG Z80 core.
    /// Known issues: Invalid prefix sequences may cause the clock time and
    /// wall time counts not to be updated correctly.
    /// </summary>
    public class Z80Cpu
    {
        //public struct 

        #region Registers
        // The NMOS Z80 used inverted logic levels, so these are 0xFFFF in an NMOS Z80.
        // I guessing 0 is correct for a CMOS Z80.
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort AF = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort BC = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort DE = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort HL = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort ShadowAF = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort ShadowBC = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort ShadowDE = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort ShadowHL = 0;
        /// <summary>
        /// 16-bit Z80 register
        /// </summary>
        public ushort SP = 0xFFFF;
        /// <summary>
        /// 16-bit Z80 register
        /// </summary>
        public ushort IX = 0;
        /// <summary>
        /// 16-bit Z80 register
        /// </summary>
        public ushort IY = 0;
        /// <summary>
        /// 16-bit Z80 register pair
        /// </summary>
        public ushort IR = 0;
        /// <summary>
        /// 16-bit Z80 register
        /// </summary>
        public ushort PC = 0;
        /// <summary>
        /// Z80 interrupt flip-flops.
        /// It's a ushort because of YAZE.
        /// </summary>
        public ushort IFF = 0;
        /// <summary>
        /// Interrupt mode.
        /// </summary>
        public ushort IM = 0;
        #endregion


        #region 8-bit Registers

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte A
        {
            get
            {
                return hreg(AF);
            }
            set
            {
                AF = (ushort)(AF & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte F
        {
            get
            {
                return lreg(AF);
            }
            set
            {
                AF = (ushort)(AF & 0xFF00 | value);
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte B
        {
            get
            {
                return hreg(BC);
            }
            set
            {
                BC = (ushort)(BC & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte C
        {
            get
            {
                return lreg(BC);
            }
            set
            {
                BC = (ushort)(BC & 0xFF00 | value);
            }
        }


        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte D
        {
            get
            {
                return hreg(DE);
            }
            set
            {
                DE = (ushort)(DE & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte E
        {
            get
            {
                return lreg(DE);
            }
            set
            {
                DE = (ushort)(DE & 0xFF00 | value);
            }
        }


        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte H
        {
            get
            {
                return hreg(HL);
            }
            set
            {
                HL = (ushort)(HL & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte L
        {
            get
            {
                return lreg(HL);
            }
            set
            {
                HL = (ushort)(HL & 0xFF00 | value);
            }
        }
        
        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowA
        {
            get
            {
                return hreg(ShadowAF);
            }
            set
            {
                ShadowAF = (ushort)(ShadowAF & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowF
        {
            get
            {
                return lreg(ShadowAF);
            }
            set
            {
                ShadowAF = (ushort)(ShadowAF & 0xFF00 | value);
            }
        }


        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowB
        {
            get
            {
                return hreg(ShadowBC);
            }
            set
            {
                ShadowBC = (ushort)(ShadowBC & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowC
        {
            get
            {
                return lreg(ShadowBC);
            }
            set
            {
                ShadowBC = (ushort)(ShadowBC & 0xFF00 | value);
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowD
        {
            get
            {
                return hreg(ShadowDE);
            }
            set
            {
                ShadowDE = (ushort)(ShadowDE & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowE
        {
            get
            {
                return lreg(ShadowDE);
            }
            set
            {
                ShadowDE = (ushort)(ShadowDE & 0xFF00 | value);
            }
        }


        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowH
        {
            get
            {
                return hreg(ShadowHL);
            }
            set
            {
                ShadowHL = (ushort)(ShadowHL & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte ShadowL
        {
            get
            {
                return lreg(ShadowHL);
            }
            set
            {
                ShadowHL = (ushort)(ShadowHL & 0xFF00 | value);
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte IXH
        {
            get
            {
                return hreg(IX);
            }
            set
            {
                IX = (ushort)(IX & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte IXL
        {
            get
            {
                return lreg(IX);
            }
            set
            {
                IX = (ushort)(IX & 0xFF00 | value);
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte IYH
        {
            get
            {
                return hreg(IY);
            }
            set
            {
                IY = (ushort)(IY & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte IYL
        {
            get
            {
                return lreg(IY);
            }
            set
            {
                IY = (ushort)(IY & 0xFF00 | value);
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte I
        {
            get
            {
                return hreg(IR);
            }
            set
            {
                IR = (ushort)(IR & 0xFF | (value << 8));
            }
        }

        /// <summary>
        /// 8-bit Z80 register
        /// </summary>
        public byte R
        {
            get
            {
                return lreg(IR);
            }
            set
            {
                IR = (ushort)(IR & 0xFF00 | value);
            }
        }
        
        /// <summary>
        /// Z80 Interrupt Flip-flop
        /// </summary>
        public bool IFF1
        {
            get
            {
                return (IFF & 1) != 0;
            }
            set
            {
                if (value)
                {
                    IFF = (ushort)(IFF | 1);
                }
                else
                {
                    IFF = (ushort)(IFF & 2);
                }
            }
        }

        /// <summary>
        /// Z80 Interrupt Flip-flop
        /// </summary>
        public bool IFF2
        {
            get
            {
                return (IFF & 2) != 0;
            }
            set
            {
                if (value)
                {
                    IFF = (ushort)(IFF | 2);
                }
                else
                {
                    IFF = (ushort)(IFF & 1);
                }
            }
        }

        #endregion


        #region Flags

        public bool FlagS
        {
            get
            {
                return (AF & FLAG_S) != 0;
            }
            set
            {
                unchecked
                {
                    if (value)
                        AF |= (ushort)FLAG_S;
                    else
                        AF &= (ushort)(~FLAG_S);
                }
            }
        }

        public bool FlagZ
        {
            get
            {
                return (AF & FLAG_Z) != 0;
            }
            set
            {
                unchecked
                {
                    if (value)
                        AF |= (ushort)FLAG_Z;
                    else
                        AF &= (ushort)(~FLAG_Z);
                }
            }
        }

        public bool FlagH
        {
            get
            {
                return (AF & FLAG_H) != 0;
            }
            set
            {
                unchecked
                {
                    if (value)
                        AF |= (ushort)FLAG_H;
                    else
                        AF &= (ushort)(~FLAG_H);
                }
            }
        }

        public bool FlagPv
        {
            get
            {
                return (AF & FLAG_P) != 0;
            }
            set
            {
                unchecked
                {
                    if (value)
                        AF |= (ushort)FLAG_P;
                    else
                        AF &= (ushort)(~FLAG_P);
                }
            }
        }

        public bool FlagN
        {
            get
            {
                return (AF & FLAG_N) != 0;
            }
            set
            {
                unchecked
                {
                    if (value)
                        AF |= (ushort)FLAG_N;
                    else
                        AF &= (ushort)(~FLAG_N);
                }
            }
        }

        public bool FlagC
        {
            get
            {
                return (AF & FLAG_C) != 0;
            }
            set
            {
                unchecked
                {
                    if (value)
                        AF |= (ushort)FLAG_C;
                    else
                        AF &= (ushort)(~FLAG_C);
                }
            }
        }
        #endregion


        #region Exchanges

        public void ExAfShadowAf()
        {
            ushort a = ShadowAF;
            ShadowAF = AF;
            AF = a;
        }

        public void Exx()
        {
            ushort t;
            t = ShadowBC;
            ShadowBC = BC;
            BC = t;
            t = ShadowDE;
            ShadowDE = DE;
            DE = t;
            t = ShadowHL;
            ShadowHL = HL;
            HL = t;
        }

        #endregion


        #region Constants
        public const byte FLAG_C = 1;
        public const byte FLAG_N = 2;
        public const byte FLAG_P = 4;
        public const byte FLAG_X = 8;
        public const byte FLAG_H = 16;
        public const byte FLAG_Y = 32;
        public const byte FLAG_Z = 64;
        public const byte FLAG_S = 128;
        #endregion


        #region I/O Interface

        public bool Break = false;

        bool forceReset = false;

        /// <summary>
        /// Set to true to tell the Z80 to reset immediately.
        /// </summary>
        public bool ForceReset// = false;
        {
            get
            {
                return forceReset;
            }
            set
            {
                forceReset = value;
                if (value)
                    forceReset = value;
            }
        }

        private bool intAck = false;
        /// <summary>
        /// Returns true if the current bus read is getting the interrupt ID from an external device.
        /// </summary>
        public bool InterruptAcknowledge
        {
            get
            {
                return intAck;
            }
        }

        private bool m1 = false;
        /// <summary>
        /// True if the Z80 is fetching an opcode.
        /// </summary>
        public bool M1
        {
            get
            {
                return m1;
            }
        }

        private bool halt = false;
        /// <summary>
        /// True if the Z80 is in the HALT state.
        /// </summary>
        public bool Halt
        {
            get
            {
                return halt;
            }
            set
            {
                halt = value;
            }
        }

        private int interrupt = 0;
        /*public void Interrupt()
        {
            interrupt++;
        }

        public void Uninterrupt()
        {
            if (interrupt > 0)
                interrupt--;
        }*/
        public bool Interrupt;


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void AssertM1()
        {
            /*if (PC == BpExecution)
                BreakExecution();*/
            m1 = true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void ReleaseM1()
        {
            m1 = false;
            //MemoryRead(this, IR);
            // Oddly enough, the Z80 itself actually implements IR as a 16-bit
            // register. The circuit that increments R uses the same 16-bit
            // incrementer as the one that increments PC. I isn't incremented
            // because it has a 7-bit cutoff mode.
            // Ref: http://www.righto.com/2013/11/the-z-80s-16-bit-incrementdecrement.html
            IR = (ushort)(IR & 0xFF80 | ((IR + 1) & 0x7F));
        }

        /// <summary>
        /// This delegate type is used for read requests to both memory and I/O
        /// ports.
        /// </summary>
        /// <param name="sender">Object requesting data. If this is a Z80,
        /// receiver may check sender's state, e.g. state of M1 or RFSH lines.
        /// Some hardware mutates its state upon reads. However, if sender is
        /// not a Z80, receiver may choose not to mutate its state, because
        /// a debugger may be doing the read.
        /// </param>
        /// <param name="address">Address being requested.</param>
        /// <returns>A byte.</returns>
        public delegate byte BusRead(object sender, ushort address);

        /// <summary>
        /// This delegate is invoked when the Z80 performs a memory read request.
        /// This delegate MUST be not-null.
        /// </summary>
        public BusRead MemoryRead;

        /// <summary>
        /// This delegate is invoked when the Z80 performs an I/O port read request.
        /// This delegate MUST be not-null. (Or, all Z80 code executed must not
        /// contain any IN, INI, IND, INIR, or INDR instructions.)
        /// </summary>
        public BusRead IoPortRead;

        /// <summary>
        /// This delegate type is used for both memory and I/O port write
        /// requests.
        /// </summary>
        /// <param name="sender">Object requesting data. If this is a Z80,
        /// receiver may check sender's state, e.g. state of M1 or RFSH lines. 
        /// </param>
        /// <param name="address">Address to which write is placed.</param>
        /// <param name="value">Byte being written.</param>
        public delegate void BusWrite(object sender, ushort address, byte value);

        /// <summary>
        /// This delegate is invoked when the Z80 performs a memory write
        /// request.
        /// </summary>
        public BusWrite MemoryWrite;

        /// <summary>
        /// This delegate is invoked when the Z80 performs an I/O port write
        /// request.
        /// </summary>
        public BusWrite IoPortWrite;

        #endregion


        #region Break Points

        //public int BpExecution = -1;
        //public int BpMemoryRead = -1;
        //public int BpMemoryWrite = -1;
        public int BpIoRead = -1;
        public int BpIoWrite = -1;
        public bool BpAnyIo = false;
        public bool BpRet = false;
        public bool BpInterrupt = false;
        public bool BpReti = false;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void BreakExecution()
        {
            Break = true;
            if (TraceLastExec)
                for (int i = 0; i < BreakCpuState.Length; i++)
                    BreakCpuState[i] = LastExecData[(LastExecPtr - 1) & LastExecMask, i];
            else
            {
                BreakCpuState[0] = 0;
                BreakCpuState[1] = 0;
                BreakCpuState[2] = PC;
                BreakCpuState[3] = SP;
                BreakCpuState[4] = AF;
                BreakCpuState[5] = BC;
                BreakCpuState[6] = DE;
                BreakCpuState[7] = HL;
                BreakCpuState[8] = ShadowAF;
                BreakCpuState[9] = ShadowBC;
                BreakCpuState[10] = ShadowDE;
                BreakCpuState[11] = ShadowHL;
                BreakCpuState[12] = IX;
                BreakCpuState[13] = IY;
                BreakCpuState[14] = IR;
                BreakCpuState[15] = IFF;
            }
        }

        public ushort[] BreakCpuState = new ushort[16];

        #endregion


        #region Macros
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void SETFLAG(byte f, bool c)
        {
            AF = (ushort)(c ? AF | f : AF & ~f);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private bool TSTFLAG(byte f)
        {
            return (AF & f) != 0;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte ldig(int x)
        {
            return (byte)(x & 0xF);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte hdig(int x)
        {
            return (byte)((x >> 4) & 0xF);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte lreg(int x)
        {
            return (byte)(x & 0xFF);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte hreg(int  x)
        {
            return (byte)((x >> 8) & 0xFF);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private ushort Setlreg(ref ushort x, int v)
        {
            return x = (ushort)(x & 0xFF00 | ((ushort)v & 0xFF));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private ushort Sethreg(ref ushort x, int v)
        {
            return x = (ushort)(x & 0xFF | (((ushort)v & 0xFF) << 8));
        }

        private static byte[] partab = {
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        0,4,4,0,4,0,0,4,4,0,0,4,0,4,4,0,
	        4,0,0,4,0,4,4,0,0,4,4,0,4,0,0,4,
        };

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private ushort parity(int x)
        {
            return (ushort)partab[(x) & 0xFF];
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private ushort POP(ref ushort d)
        {
            return d = (ushort)(RAM_pp(ref SP) + (RAM_pp(ref SP) << 8));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PUSH(ushort x)
        {
            unchecked
            {
                MemoryWrite(this, --SP, (byte)(x >> 8));
                /*if (SP == BpMemoryWrite)
                    BreakExecution();*/
                MemoryWrite(this, --SP, (byte)(x & 0xFF));
                /*if (SP == BpMemoryWrite)
                    BreakExecution();*/
            }
        }
        
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte Read(ushort address)
        {
            /*if (address == BpMemoryRead)
                BreakExecution();*/
            return MemoryRead(this, address);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte RAM_pp(ref ushort a)
        {
            unchecked
            {
                /*if (a == BpMemoryRead)
                    BreakExecution();*/
                return MemoryRead(this, a++);
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte RAM_mm(ref ushort a)
        {
            unchecked
            {
                /*if (a == BpMemoryRead)
                    BreakExecution();*/
                return MemoryRead(this, a--);
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte mm_RAM(ref ushort a)
        {
            unchecked
            {
                byte t = MemoryRead(this, --a);
                /*if (a == BpMemoryRead)
                    BreakExecution();*/
                return t;
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte GetBYTE(ushort a)
        {
            /*if (a == BpMemoryRead)
                BreakExecution();*/
            return MemoryRead(this, a);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte GetBYTE_pp(ref ushort a)
        {
            unchecked
            {
                /*if (a == BpMemoryRead)
                    BreakExecution();*/
                return MemoryRead(this, a++);
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte GetBYTE_mm(ref ushort a)
        {
            unchecked
            {
                /*if (a == BpMemoryRead)
                    BreakExecution();*/
                return MemoryRead(this, a--);
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private byte mm_GetBYTE(ref ushort a)
        {
            unchecked
            {
                byte t = MemoryRead(this, --a);
                /*if (a == BpMemoryRead)
                    BreakExecution();*/
                return t;
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PutBYTE(ushort a, int v)
        {
            /*if (a == BpMemoryWrite)
                BreakExecution();*/
            MemoryWrite(this, a, (byte)v);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PutBYTE_pp(ref ushort a, int v)
        {
            unchecked
            {
                /*if (a == BpMemoryWrite)
                    BreakExecution();*/
                MemoryWrite(this, a++, (byte)v);
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PutBYTE_mm(ref ushort a, int v)
        {
            unchecked
            {
                MemoryWrite(this, a--, (byte)v);
                /*if (a == BpMemoryWrite)
                    BreakExecution();*/
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private ushort GetWORD(int a)
        {
            unchecked
            {
                /*if (a == BpMemoryRead)
                    BreakExecution();
                if ((ushort)(a + 1) == BpMemoryRead)
                    BreakExecution();*/
                return (ushort)(
                        MemoryRead(this, (ushort)a)
                        | (MemoryRead(this, (ushort)((ushort)(a + 1))) << 8)
                    );
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PutWORD(ushort a, int v)
        {
            /*if (a == BpMemoryWrite)
                BreakExecution();*/
            MemoryWrite(this, a, (byte)(v & 0xFF));
            /*if ((ushort)(a + 1) == BpMemoryWrite)
                BreakExecution();*/
            MemoryWrite(this, (ushort)(a + 1), (byte)((v >> 8) & 0xFF));
        }

        #endregion


        public SystemClock Clock = new SystemClock();

        public Z80Cpu()
        {
            
        }

        /// <summary>
        /// Called when the CPU is reset.
        /// </summary>
        public EventHandler ResetEvent;

        public void Reset()
        {
            // TODO: I think AF or SP gets set to FF/FF, not 0.
            AF = BC = DE = HL = 0;
            ShadowAF = ShadowBC = ShadowDE = ShadowHL = 0;
            IX = IY = SP = PC = 0;
            IM = IFF = 0;
            BreakExecution();
            if (ResetEvent != null)
                ResetEvent(this, null);
        }



        public const int LastExecSize = 65536;//4096;//256;

        public const int LastExecMask = 0xFFFF;//0xFFF;//0xFF;

        /*
        public readonly ushort[] LastExecAddress = new ushort[LastExecSize];

        public readonly byte[,] LastExecOpcode = new byte[LastExecSize, 4];
        */
        public readonly ushort[,] LastExecData = new ushort[LastExecSize, 16];

        public int LastExecPtr = 0;

        public bool TraceLastExec = false;

        public bool LogBCalls = false;
        public const int BCallLogSize = 4096;
        public const int BCallLogMask = 0xFFF;
        public int BCallLogPtr = 0;
        public readonly ushort[,] BCallLogData = new ushort[BCallLogSize, 10];
        
        public bool BreakEnable = true;

        public void Step()
        {
            unchecked
            {
                Break = false;
                int temp, acu, sum, cbits, adr, op;
                if (IFF1 & Interrupt /* && interrupt > 0*/)
                {
                    if (BpInterrupt)
                        BreakExecution();
                    IFF = 0;
                    interrupt = 0;
                    halt = false;
                    if (IM == 0)
                    {
                        intAck = true;
                        temp = MemoryRead(this, 0);
                        intAck = false;
                        Clock.IncTime(2);
                        goto thisIsATerribleHack;
                    }
                    else if (IM == 1)
                    {
                        PUSH(PC); PC = 0x38;
                        Clock.IncTime(13);
                        return;
                    }
                    else if (IM == 2)
                    {
                        PUSH(PC);
                        intAck = true;
                        temp = IR & 0xFF00 | MemoryRead(this, 0);
                        intAck = false;
                        PC = GetWORD(temp);
                        Clock.IncTime(19);
                        return;
                    }
                }
                if (halt)
                {
                    Clock.IncTime(10);
                    return;
                }
                if (TraceLastExec)
                {
                    /*LastExecOpcode[LastExecPtr, 0] = MemoryRead(null, PC);
                    LastExecOpcode[LastExecPtr, 1] = MemoryRead(null, (ushort)(PC + 1));
                    LastExecOpcode[LastExecPtr, 2] = MemoryRead(null, (ushort)(PC + 2));
                    LastExecOpcode[LastExecPtr, 3] = MemoryRead(null, (ushort)(PC + 3));
                    LastExecAddress[LastExecPtr] = PC;
                    */
                    LastExecData[LastExecPtr, 0] = (ushort)(MemoryRead(null, PC) | (MemoryRead(null, (ushort)(PC + 1)) << 8));
                    LastExecData[LastExecPtr, 1] = (ushort)(MemoryRead(null, (ushort)(PC + 2)) | (MemoryRead(null, (ushort)(PC + 3)) << 8));
                    LastExecData[LastExecPtr, 2] = PC;
                    LastExecData[LastExecPtr, 3] = SP;
                    LastExecData[LastExecPtr, 4] = AF;
                    LastExecData[LastExecPtr, 5] = BC;
                    LastExecData[LastExecPtr, 6] = DE;
                    LastExecData[LastExecPtr, 7] = HL;
                    LastExecData[LastExecPtr, 8] = ShadowAF;
                    LastExecData[LastExecPtr, 9] = ShadowBC;
                    LastExecData[LastExecPtr, 10] = ShadowDE;
                    LastExecData[LastExecPtr, 11] = ShadowHL;
                    LastExecData[LastExecPtr, 12] = IX;
                    LastExecData[LastExecPtr, 13] = IY;
                    LastExecData[LastExecPtr, 14] = IR;
                    LastExecData[LastExecPtr, 15] = IFF;
                    LastExecPtr = (LastExecPtr + 1) & LastExecMask;
                    
                }
                AssertM1();
                temp = RAM_pp(ref PC);
                if (Break && BreakEnable)
                {
                    PC--;
                    return;
                }
                ReleaseM1();
                if (ForceReset)
                {
                    Reset();
                    return;
                }
                thisIsATerribleHack:
                switch (temp)
                {
                    case 0x00:			/* NOP */
                        Clock.IncTime(4);
                        break;
                    case 0x01:			/* LD BC,nnnn */
                        BC = GetWORD(PC);
                        PC += 2;
                        Clock.IncTime(10);
                        break;
                    case 0x02:			/* LD (BC),A */
                        PutBYTE(BC, hreg(AF));
                        Clock.IncTime(7);
                        break;
                    case 0x03:			/* INC BC */
                        ++BC;
                        Clock.IncTime(6);
                        break;
                    case 0x04:			/* INC B */
                        BC += 0x100;
                        temp = hreg(BC);
                        AF = (ushort)(
                            (AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x05:			/* DEC B */
                        BC -= 0x100;
                        temp = hreg(BC);
                        AF = (ushort)(
                            (AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x06:			/* LD B,nn */
                        Sethreg(ref BC, GetBYTE_pp(ref PC));
                        Clock.IncTime(7);
                        break;
                    case 0x07:			/* RLCA */
                        AF = (ushort)(((AF >> 7) & 0x0128) | ((AF << 1) & ~0x1ff) |
                            (AF & 0xc4) | ((AF >> 15) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x08:			/* EX AF,AF' */
                        ExAfShadowAf();
                        Clock.IncTime(4);
                        break;
                    case 0x09:			/* ADD HL,BC */
                        HL &= 0xffff;
                        BC &= 0xffff;
                        sum = HL + BC;
                        cbits = (HL ^ BC ^ sum) >> 8;
                        HL = (ushort)(sum);
                        AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                            (cbits & 0x10) | ((cbits >> 8) & 1));
                        Clock.IncTime(11);
                        break;
                    case 0x0A:			/* LD A,(BC) */
                        Sethreg(ref AF, GetBYTE(BC));
                        Clock.IncTime(7);
                        break;
                    case 0x0B:			/* DEC BC */
                        --BC;
                        Clock.IncTime(6);
                        break;
                    case 0x0C:			/* INC C */
                        temp = lreg(BC) + 1;
                        Setlreg(ref BC, temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x0D:			/* DEC C */
                        temp = lreg(BC) - 1;
                        Setlreg(ref BC, temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x0E:			/* LD C,nn */
                        Setlreg(ref BC, GetBYTE_pp(ref PC));
                        Clock.IncTime(7);
                        break;
                    case 0x0F:			/* RRCA */
                        temp = hreg(AF);
                        sum = temp >> 1;
                        AF = (ushort)(((temp & 1) << 15) | (sum << 8) |
                            (sum & 0x28) | (AF & 0xc4) | (temp & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x10:			/* DJNZ dd */
                        if (B != 1)
                            Clock.IncTime(5);
                        PC += (ushort)(((BC -= 0x100) & 0xff00) != 0 ? (sbyte)GetBYTE(PC) + 1 : 1);
                        Clock.IncTime(8);
                        break;
                    case 0x11:			/* LD DE,nnnn */
                        DE = GetWORD(PC);
                        PC += 2;
                        Clock.IncTime(10);
                        break;
                    case 0x12:			/* LD (DE),A */
                        PutBYTE(DE, hreg(AF));
                        Clock.IncTime(7);
                        break;
                    case 0x13:			/* INC DE */
                        ++DE;
                        Clock.IncTime(6);
                        break;
                    case 0x14:			/* INC D */
                        DE += 0x100;
                        temp = hreg(DE);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x15:			/* DEC D */
                        DE -= 0x100;
                        temp = hreg(DE);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x16:			/* LD D,nn */
                        Sethreg(ref DE, GetBYTE_pp(ref PC));
                        Clock.IncTime(7);
                        break;
                    case 0x17:			/* RLA */
                        AF = (ushort)(((AF << 8) & 0x0100) | ((AF >> 7) & 0x28) | ((AF << 1) & ~0x01ff) |
                            (AF & 0xc4) | ((AF >> 15) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x18:			/* JR dd */
                        PC += (ushort)((sbyte)GetBYTE(PC) + 1);
                        Clock.IncTime(12);
                        break;
                    case 0x19:			/* ADD HL,DE */
                        HL &= 0xffff;
                        DE &= 0xffff;
                        sum = HL + DE;
                        cbits = (HL ^ DE ^ sum) >> 8;
                        HL = (ushort)sum;
                        AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                            (cbits & 0x10) | ((cbits >> 8) & 1));
                        Clock.IncTime(11);
                        break;
                    case 0x1A:			/* LD A,(DE) */
                        Sethreg(ref AF, GetBYTE(DE));
                        Clock.IncTime(7);
                        break;
                    case 0x1B:			/* DEC DE */
                        --DE;
                        Clock.IncTime(6);
                        break;
                    case 0x1C:			/* INC E */
                        temp = lreg(DE) + 1;
                        Setlreg(ref DE, temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x1D:			/* DEC E */
                        temp = lreg(DE) - 1;
                        Setlreg(ref DE, temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x1E:			/* LD E,nn */
                        Setlreg(ref DE, GetBYTE_pp(ref PC));
                        Clock.IncTime(7);
                        break;
                    case 0x1F:			/* RRA */
                        temp = hreg(AF);
                        sum = temp >> 1;
                        AF = (ushort)(((AF & 1) << 15) | (sum << 8) |
                            (sum & 0x28) | (AF & 0xc4) | (temp & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x20:			/* JR NZ,dd */
                        PC += (ushort)((!TSTFLAG(FLAG_Z)) ? (sbyte)GetBYTE(PC) + 1 : 1);
                        if (!TSTFLAG(FLAG_Z))
                            Clock.IncTime(12);
                        else
                            Clock.IncTime(7);
                        break;
                    case 0x21:			/* LD HL,nnnn */
                        HL = GetWORD(PC);
                        PC += 2;
                        Clock.IncTime(10);
                        break;
                    case 0x22:			/* LD (nnnn),HL */
                        PutWORD(GetWORD(PC), HL);
                        PC += 2;
                        Clock.IncTime(16);
                        break;
                    case 0x23:			/* INC HL */
                        ++HL;
                        Clock.IncTime(6);
                        break;
                    case 0x24:			/* INC H */
                        HL += 0x100;
                        temp = hreg(HL);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x25:			/* DEC H */
                        HL -= 0x100;
                        temp = hreg(HL);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x26:			/* LD H,nn */
                        Sethreg(ref HL, GetBYTE_pp(ref PC));
                        Clock.IncTime(7);
                        break;
                    case 0x27:			/* DAA */
                        acu = hreg(AF);
                        temp = ldig((ushort)acu);
                        cbits = TSTFLAG(FLAG_C) ? 1 : 0;
                        if (TSTFLAG(FLAG_N))
                        {	/* last operation was a subtract */
                            bool hd = (cbits != 0) || acu > 0x99;
                            if (TSTFLAG(FLAG_H) || (temp > 9))
                            { /* adjust low digit */
                                if (temp > 5)
                                    SETFLAG(FLAG_H, false);
                                acu -= 6;
                                acu &= 0xff;
                            }
                            if (hd)		/* adjust high digit */
                                acu -= 0x160;
                        }
                        else
                        {			/* last operation was an add */
                            if (TSTFLAG(FLAG_H) || (temp > 9))
                            { /* adjust low digit */
                                SETFLAG(FLAG_H, (temp > 9));
                                acu += 6;
                            }
                            if (cbits != 0 || ((acu & 0x1f0) > 0x90)) /* adjust high digit */
                                acu += 0x60;
                        }
                        cbits |= (acu >> 8) & 1;
                        acu &= 0xff;
                        AF = (ushort)((acu << 8) | (acu & 0xa8) | ((acu == 0) ? 1 << 6 : 0) |
                            (AF & 0x12) | partab[acu] | cbits);
                        Clock.IncTime(4);
                        break;
                    case 0x28:			/* JR Z,dd */
                        PC += (ushort)((TSTFLAG(FLAG_Z)) ? (sbyte)GetBYTE(PC) + 1 : 1);
                        if (TSTFLAG(FLAG_Z))
                            Clock.IncTime(12);
                        else
                            Clock.IncTime(7);
                        break;
                    case 0x29:			/* ADD HL,HL */
                        HL &= 0xffff;
                        sum = HL + HL;
                        cbits = (HL ^ HL ^ sum) >> 8;
                        HL = (ushort)sum;
                        AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                            (cbits & 0x10) | ((cbits >> 8) & 1));
                        Clock.IncTime(11);
                        break;
                    case 0x2A:			/* LD HL,(nnnn) */
                        temp = GetWORD(PC);
                        HL = GetWORD((ushort)temp);
                        PC += 2;
                        Clock.IncTime(16);
                        break;
                    case 0x2B:			/* DEC HL */
                        --HL;
                        Clock.IncTime(6);
                        break;
                    case 0x2C:			/* INC L */
                        temp = lreg(HL) + 1;
                        Setlreg(ref HL, temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x2D:			/* DEC L */
                        temp = lreg(HL) - 1;
                        Setlreg(ref HL, temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x2E:			/* LD L,nn */
                        Setlreg(ref HL, GetBYTE_pp(ref PC));
                        Clock.IncTime(7);
                        break;
                    case 0x2F:			/* CPL */
                        AF = (ushort)((~AF & ~0xff) | (AF & 0xc5) | ((~AF >> 8) & 0x28) | 0x12);
                        Clock.IncTime(4);
                        break;
                    case 0x30:			/* JR NC,dd */
                        PC += (ushort)((!TSTFLAG(FLAG_C)) ? (sbyte)GetBYTE(PC) + 1 : 1);
                        if (!TSTFLAG(FLAG_C))
                            Clock.IncTime(12);
                        else
                            Clock.IncTime(7);
                        break;
                    case 0x31:			/* LD SP,nnnn */
                        SP = GetWORD(PC);
                        PC += 2;
                        Clock.IncTime(10);
                        break;
                    case 0x32:			/* LD (nnnn),A */
                        PutBYTE(GetWORD(PC), hreg(AF));
                        PC += 2;
                        Clock.IncTime(13);
                        break;
                    case 0x33:			/* INC SP */
                        ++SP;
                        Clock.IncTime(6);
                        break;
                    case 0x34:			/* INC (HL) */
                        temp = GetBYTE(HL) + 1;
                        PutBYTE(HL, (byte)temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                            );
                        Clock.IncTime(11);
                        break;
                    case 0x35:			/* DEC (HL) */
                        temp = GetBYTE(HL) - 1;
                        PutBYTE(HL, (byte)temp);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(11);
                        break;
                    case 0x36:			/* LD (HL),nn */
                        PutBYTE(HL, GetBYTE_pp(ref PC));
                        Clock.IncTime(10);
                        break;
                    case 0x37:			/* SCF */
                        AF = (ushort)((AF & ~0x3b) | ((AF >> 8) & 0x28) | 1);
                        Clock.IncTime(4);
                        break;
                    case 0x38:			/* JR C,dd */
                        PC += (ushort)((TSTFLAG(FLAG_C)) ? (sbyte)GetBYTE(PC) + 1 : 1);
                        if (TSTFLAG(FLAG_C))
                            Clock.IncTime(12);
                        else
                            Clock.IncTime(7);
                        break;
                    case 0x39:			/* ADD HL,SP */
                        HL &= 0xffff;
                        SP &= 0xffff;
                        sum = HL + SP;
                        cbits = (HL ^ SP ^ sum) >> 8;
                        HL = (ushort)sum;
                        AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                            (cbits & 0x10) | ((cbits >> 8) & 1));
                        Clock.IncTime(11);
                        break;
                    case 0x3A:			/* LD A,(nnnn) */
                        temp = GetWORD(PC);
                        Sethreg(ref AF, GetBYTE((ushort)temp));
                        PC += 2;
                        Clock.IncTime(13);
                        break;
                    case 0x3B:			/* DEC SP */
                        --SP;
                        Clock.IncTime(13);
                        break;
                    case 0x3C:			/* INC A */
                        AF += 0x100;
                        temp = hreg(AF);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                            ((temp == 0x80 ? 1 : 0) << 2)
                                );
                        Clock.IncTime(4);
                        break;
                    case 0x3D:			/* DEC A */
                        AF -= 0x100;
                        temp = hreg(AF);
                        AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                            (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                            ((temp == 0x7f ? 1 : 0) << 2) | 2
                            );
                        Clock.IncTime(4);
                        break;
                    case 0x3E:			/* LD A,nn */
                        Sethreg(ref AF, GetBYTE_pp(ref PC));
                        Clock.IncTime(7);
                        break;
                    case 0x3F:			/* CCF */
                        AF = (ushort)((AF & ~0x3b) | ((AF >> 8) & 0x28) | ((AF & 1) << 4) | (~AF & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x40:			/* LD B,B */
                        /* nop */
                        Clock.IncTime(4);
                        break;
                    case 0x41:			/* LD B,C */
                        BC = (ushort)((BC & 255) | ((BC & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x42:			/* LD B,D */
                        BC = (ushort)((BC & 255) | (DE & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x43:			/* LD B,E */
                        BC = (ushort)((BC & 255) | ((DE & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x44:			/* LD B,H */
                        BC = (ushort)((BC & 255) | (HL & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x45:			/* LD B,L */
                        BC = (ushort)((BC & 255) | ((HL & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x46:			/* LD B,(HL) */
                        Sethreg(ref BC, GetBYTE(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x47:			/* LD B,A */
                        BC = (ushort)((BC & 255) | (AF & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x48:			/* LD C,B */
                        BC = (ushort)((BC & ~255) | ((BC >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x49:			/* LD C,C */
                        /* nop */
                        Clock.IncTime(4);
                        break;
                    case 0x4A:			/* LD C,D */
                        BC = (ushort)((BC & ~255) | ((DE >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x4B:			/* LD C,E */
                        BC = (ushort)((BC & ~255) | (DE & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x4C:			/* LD C,H */
                        BC = (ushort)((BC & ~255) | ((HL >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x4D:			/* LD C,L */
                        BC = (ushort)((BC & ~255) | (HL & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x4E:			/* LD C,(HL) */
                        Setlreg(ref BC, GetBYTE(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x4F:			/* LD C,A */
                        BC = (ushort)((BC & ~255) | ((AF >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x50:			/* LD D,B */
                        DE = (ushort)((DE & 255) | (BC & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x51:			/* LD D,C */
                        DE = (ushort)((DE & 255) | ((BC & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x52:			/* LD D,D */
                        /* nop */
                        Clock.IncTime(4);
                        break;
                    case 0x53:			/* LD D,E */
                        DE = (ushort)((DE & 255) | ((DE & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x54:			/* LD D,H */
                        DE = (ushort)((DE & 255) | (HL & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x55:			/* LD D,L */
                        DE = (ushort)((DE & 255) | ((HL & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x56:			/* LD D,(HL) */
                        Sethreg(ref DE, GetBYTE(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x57:			/* LD D,A */
                        DE = (ushort)((DE & 255) | (AF & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x58:			/* LD E,B */
                        DE = (ushort)((DE & ~255) | ((BC >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x59:			/* LD E,C */
                        DE = (ushort)((DE & ~255) | (BC & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x5A:			/* LD E,D */
                        DE = (ushort)((DE & ~255) | ((DE >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x5B:			/* LD E,E */
                        /* nop */
                        Clock.IncTime(4);
                        break;
                    case 0x5C:			/* LD E,H */
                        DE = (ushort)((DE & ~255) | ((HL >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x5D:			/* LD E,L */
                        DE = (ushort)((DE & ~255) | (HL & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x5E:			/* LD E,(HL) */
                        Setlreg(ref DE, GetBYTE(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x5F:			/* LD E,A */
                        DE = (ushort)((DE & ~255) | ((AF >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x60:			/* LD H,B */
                        HL = (ushort)((HL & 255) | (BC & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x61:			/* LD H,C */
                        HL = (ushort)((HL & 255) | ((BC & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x62:			/* LD H,D */
                        HL = (ushort)((HL & 255) | (DE & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x63:			/* LD H,E */
                        HL = (ushort)((HL & 255) | ((DE & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x64:			/* LD H,H */
                        /* nop */
                        Clock.IncTime(4);
                        break;
                    case 0x65:			/* LD H,L */
                        HL = (ushort)((HL & 255) | ((HL & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x66:			/* LD H,(HL) */
                        Sethreg(ref HL, GetBYTE(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x67:			/* LD H,A */
                        HL = (ushort)((HL & 255) | (AF & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x68:			/* LD L,B */
                        HL = (ushort)((HL & ~255) | ((BC >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x69:			/* LD L,C */
                        HL = (ushort)((HL & ~255) | (BC & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x6A:			/* LD L,D */
                        HL = (ushort)((HL & ~255) | ((DE >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x6B:			/* LD L,E */
                        HL = (ushort)((HL & ~255) | (DE & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x6C:			/* LD L,H */
                        HL = (ushort)((HL & ~255) | ((HL >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x6D:			/* LD L,L */
                        /* nop */
                        Clock.IncTime(4);
                        break;
                    case 0x6E:			/* LD L,(HL) */
                        Setlreg(ref HL, GetBYTE(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x6F:			/* LD L,A */
                        HL = (ushort)((HL & ~255) | ((AF >> 8) & 255));
                        Clock.IncTime(4);
                        break;
                    case 0x70:			/* LD (HL),B */
                        PutBYTE(HL, hreg(BC));
                        Clock.IncTime(7);
                        break;
                    case 0x71:			/* LD (HL),C */
                        PutBYTE(HL, lreg(BC));
                        Clock.IncTime(7);
                        break;
                    case 0x72:			/* LD (HL),D */
                        PutBYTE(HL, hreg(DE));
                        Clock.IncTime(7);
                        break;
                    case 0x73:			/* LD (HL),E */
                        PutBYTE(HL, lreg(DE));
                        Clock.IncTime(7);
                        break;
                    case 0x74:			/* LD (HL),H */
                        PutBYTE(HL, hreg(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x75:			/* LD (HL),L */
                        PutBYTE(HL, lreg(HL));
                        break;
                    case 0x76:			/* HALT */
                        //SAVE_STATE();
                        //return PC&0xffff;
                        halt = true;
                        Clock.IncTime(4);
                        return;
                    case 0x77:			/* LD (HL),A */
                        PutBYTE(HL, hreg(AF));
                        Clock.IncTime(7);
                        break;
                    case 0x78:			/* LD A,B */
                        AF = (ushort)((AF & 255) | (BC & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x79:			/* LD A,C */
                        AF = (ushort)((AF & 255) | ((BC & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x7A:			/* LD A,D */
                        AF = (ushort)((AF & 255) | (DE & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x7B:			/* LD A,E */
                        AF = (ushort)((AF & 255) | ((DE & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x7C:			/* LD A,H */
                        AF = (ushort)((AF & 255) | (HL & ~255));
                        Clock.IncTime(4);
                        break;
                    case 0x7D:			/* LD A,L */
                        AF = (ushort)((AF & 255) | ((HL & 255) << 8));
                        Clock.IncTime(4);
                        break;
                    case 0x7E:			/* LD A,(HL) */
                        Sethreg(ref AF, GetBYTE(HL));
                        Clock.IncTime(7);
                        break;
                    case 0x7F:			/* LD A,A */
                        /* nop */
                        Clock.IncTime(4);
                        break;
                    case 0x80:			/* ADD A,B */
                        temp = hreg(BC);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x81:			/* ADD A,C */
                        temp = lreg(BC);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x82:			/* ADD A,D */
                        temp = hreg(DE);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x83:			/* ADD A,E */
                        temp = lreg(DE);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x84:			/* ADD A,H */
                        temp = hreg(HL);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x85:			/* ADD A,L */
                        temp = lreg(HL);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x86:			/* ADD A,(HL) */
                        temp = GetBYTE(HL);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(7);
                        break;
                    case 0x87:			/* ADD A,A */
                        temp = hreg(AF);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x88:			/* ADC A,B */
                        temp = hreg(BC);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x89:			/* ADC A,C */
                        temp = lreg(BC);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x8A:			/* ADC A,D */
                        temp = hreg(DE);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x8B:			/* ADC A,E */
                        temp = lreg(DE);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x8C:			/* ADC A,H */
                        temp = hreg(HL);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x8D:			/* ADC A,L */
                        temp = lreg(HL);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x8E:			/* ADC A,(HL) */
                        temp = GetBYTE(HL);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(7);
                        break;
                    case 0x8F:			/* ADC A,A */
                        temp = hreg(AF);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x90:			/* SUB B */
                        temp = hreg(BC);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x91:			/* SUB C */
                        temp = lreg(BC);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x92:			/* SUB D */
                        temp = hreg(DE);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x93:			/* SUB E */
                        temp = lreg(DE);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x94:			/* SUB H */
                        temp = hreg(HL);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x95:			/* SUB L */
                        temp = lreg(HL);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x96:			/* SUB (HL) */
                        temp = GetBYTE(HL);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(7);
                        break;
                    case 0x97:			/* SUB A */
                        temp = hreg(AF);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x98:			/* SBC A,B */
                        temp = hreg(BC);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x99:			/* SBC A,C */
                        temp = lreg(BC);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x9A:			/* SBC A,D */
                        temp = hreg(DE);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x9B:			/* SBC A,E */
                        temp = lreg(DE);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x9C:			/* SBC A,H */
                        temp = hreg(HL);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x9D:			/* SBC A,L */
                        temp = lreg(HL);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0x9E:			/* SBC A,(HL) */
                        temp = GetBYTE(HL);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(7);
                        break;
                    case 0x9F:			/* SBC A,A */
                        temp = hreg(AF);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1));
                        Clock.IncTime(4);
                        break;
                    case 0xA0:			/* AND B */
                        sum = ((AF & (BC)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) |
                            ((sum == 0 ? 1 : 0) << 6) | 0x10 | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA1:			/* AND C */
                        sum = ((AF >> 8) & BC) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                            ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA2:			/* AND D */
                        sum = ((AF & (DE)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) |
                            ((sum == 0 ? 1 : 0) << 6) | 0x10 | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA3:			/* AND E */
                        sum = ((AF >> 8) & DE) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                            ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA4:			/* AND H */
                        sum = ((AF & (HL)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) |
                            ((sum == 0 ? 1 : 0) << 6) | 0x10 | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA5:			/* AND L */
                        sum = ((AF >> 8) & HL) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                            ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA6:			/* AND (HL) */
                        sum = ((AF >> 8) & GetBYTE(HL)) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                            ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(7);
                        break;
                    case 0xA7:			/* AND A */
                        sum = ((AF & (AF)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) |
                            ((sum == 0 ? 1 : 0) << 6) | 0x10 | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA8:			/* XOR B */
                        sum = ((AF ^ (BC)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xA9:			/* XOR C */
                        sum = ((AF >> 8) ^ BC) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xAA:			/* XOR D */
                        sum = ((AF ^ (DE)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xAB:			/* XOR E */
                        sum = ((AF >> 8) ^ DE) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xAC:			/* XOR H */
                        sum = ((AF ^ (HL)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xAD:			/* XOR L */
                        sum = ((AF >> 8) ^ HL) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xAE:			/* XOR (HL) */
                        sum = ((AF >> 8) ^ GetBYTE(HL)) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(7);
                        break;
                    case 0xAF:			/* XOR A */
                        sum = ((AF ^ (AF)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB0:			/* OR B */
                        sum = ((AF | (BC)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB1:			/* OR C */
                        sum = ((AF >> 8) | BC) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB2:			/* OR D */
                        sum = ((AF | (DE)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB3:			/* OR E */
                        sum = ((AF >> 8) | DE) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB4:			/* OR H */
                        sum = ((AF | (HL)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB5:			/* OR L */
                        sum = ((AF >> 8) | HL) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB6:			/* OR (HL) */
                        sum = ((AF >> 8) | GetBYTE(HL)) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(7);
                        break;
                    case 0xB7:			/* OR A */
                        sum = ((AF | (AF)) >> 8) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(4);
                        break;
                    case 0xB8:			/* CP B */
                        temp = hreg(BC);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xB9:			/* CP C */
                        temp = lreg(BC);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xBA:			/* CP D */
                        temp = hreg(DE);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xBB:			/* CP E */
                        temp = lreg(DE);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xBC:			/* CP H */
                        temp = hreg(HL);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xBD:			/* CP L */
                        temp = lreg(HL);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xBE:			/* CP (HL) */
                        temp = GetBYTE(HL);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(7);
                        break;
                    case 0xBF:			/* CP A */
                        temp = hreg(AF);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xC0:			/* RET NZ */
                        if (!TSTFLAG(FLAG_Z)) POP(ref PC);
                        if (!TSTFLAG(FLAG_Z))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xC1:			/* POP BC */
                        POP(ref BC);
                        Clock.IncTime(10);
                        break;
                    case 0xC2:			/* JP NZ,nnnn */
                        PC = (ushort)(!TSTFLAG(FLAG_Z) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);
                        break;
                    case 0xC3:			/* JP nnnn */
                        PC = GetWORD(PC);
                        Clock.IncTime(10);
                        break;
                    case 0xC4:			/* CALL NZ,nnnn */
                        if (!TSTFLAG(FLAG_Z))
                        {
                            temp = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = (ushort)temp;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xC5:			/* PUSH BC */
                        PUSH(BC);
                        Clock.IncTime(11);
                        break;
                    case 0xC6:			/* ADD A,nn */
                        temp = GetBYTE_pp(ref PC);
                        acu = hreg(AF);
                        sum = acu + temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(7);
                        break;
                    case 0xC7:			/* RST 0 */
                        PUSH(PC); PC = 0;
                        Clock.IncTime(11);
                        break;
                    case 0xC8:			/* RET Z */
                        if (TSTFLAG(FLAG_Z)) POP(ref PC);
                        if (TSTFLAG(FLAG_Z))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xC9:			/* RET */
                        POP(ref PC);
                        Clock.IncTime(10);
                        if (BpRet)
                            BreakExecution();
                        break;
                    case 0xCA:			/* JP Z,nnnn */
                        /*if (TSTFLAG(FLAG_Z))
                        {
                            temp = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = (ushort)temp;
                        }
                        else
                            PC += 2;
                        Clock.IncTime(10);*/
                        PC = (ushort)(TSTFLAG(FLAG_Z) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);    
                        break;
                    case 0xCB:			/* CB prefix */
                        adr = HL;
                        AssertM1();
                        op = GetBYTE(PC);
                        ReleaseM1();
                        if (ForceReset)
                        {
                            Reset();
                            return;
                        }
                        acu = temp = 0;
                        Clock.IncTime(8);
                        switch (op & 7)
                        {
                            case 0: ++PC; acu = hreg(BC); break;
                            case 1: ++PC; acu = lreg(BC); break;
                            case 2: ++PC; acu = hreg(DE); break;
                            case 3: ++PC; acu = lreg(DE); break;
                            case 4: ++PC; acu = hreg(HL); break;
                            case 5: ++PC; acu = lreg(HL); break;
                            case 6: ++PC; acu = GetBYTE((ushort)adr); Clock.IncTime(4); break;
                            case 7: ++PC; acu = hreg(AF); break;
                        }
                        switch (op & 0xc0)
                        {
                            case 0x00:		/* shift/rotate */
                                switch (op & 0x38)
                                {
                                    default:
                                    case 0x00:	/* RLC */
                                        temp = (acu << 1) | (acu >> 7);
                                        cbits = temp & 1;
                                        goto cbshflg1;
                                    case 0x08:	/* RRC */
                                        temp = (acu >> 1) | (acu << 7);
                                        cbits = temp & 0x80;
                                        goto cbshflg1;
                                    case 0x10:	/* RL */
                                        temp = (acu << 1) | (TSTFLAG(FLAG_C) ? 1 : 0);
                                        cbits = acu & 0x80;
                                        goto cbshflg1;
                                    case 0x18:	/* RR */
                                        temp = (acu >> 1) | (TSTFLAG(FLAG_C) ? 1 << 7 : 0);
                                        cbits = acu & 1;
                                        goto cbshflg1;
                                    case 0x20:	/* SLA */
                                        temp = acu << 1;
                                        cbits = acu & 0x80;
                                        goto cbshflg1;
                                    case 0x28:	/* SRA */
                                        temp = (acu >> 1) | (acu & 0x80);
                                        cbits = acu & 1;
                                        goto cbshflg1;
                                    case 0x30:	/* SLIA */
                                        temp = (acu << 1) | 1;
                                        cbits = acu & 0x80;
                                        goto cbshflg1;
                                    case 0x38:	/* SRL */
                                        temp = acu >> 1;
                                        cbits = acu & 1;
                                    cbshflg1:
                                        AF = (ushort)((AF & ~0xff) | (temp & 0xa8) |
                                            (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                            parity((ushort)temp) | (cbits != 0 ? 1 : 0)
                                            );
                                        break;
                                }
                                break;
                            case 0x40:		/* BIT */
                                if ((acu & (1 << ((op >> 3) & 7))) != 0)
                                    AF = (ushort)((AF & ~0xfe) | 0x10 |
                                        (((op & 0x38) == 0x38 ? 1 : 0) << 7));
                                else
                                    AF = (ushort)((AF & ~0xfe) | 0x54);
                                if ((op & 7) != 6)
                                    AF |= (ushort)(acu & 0x28);
                                temp = acu;
                                break;
                            case 0x80:		/* RES */
                                temp = acu & ~(1 << ((op >> 3) & 7));
                                break;
                            case 0xc0:		/* SET */
                                temp = acu | (1 << ((op >> 3) & 7));
                                break;
                        }
                        switch (op & 7)
                        {
                            case 0: Sethreg(ref BC, temp); break;
                            case 1: Setlreg(ref BC, temp); break;
                            case 2: Sethreg(ref DE, temp); break;
                            case 3: Setlreg(ref DE, temp); break;
                            case 4: Sethreg(ref HL, temp); break;
                            case 5: Setlreg(ref HL, temp); break;
                            case 6: PutBYTE((ushort)adr, (byte)temp); Clock.IncTime(3); break;
                            case 7: Sethreg(ref AF, temp); break;
                        }
                        break;
                    case 0xCC:			/* CALL Z,nnnn */
                        if (TSTFLAG(FLAG_Z))
                        {
                            temp = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = (ushort)temp;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xCD:			/* CALL nnnn */
                        temp = GetWORD(PC);
                        PUSH((ushort)(PC + 2));
                        PC = (ushort)temp;
                        Clock.IncTime(17);
                        break;
                    case 0xCE:			/* ADC A,nn */
                        temp = GetBYTE_pp(ref PC);
                        acu = hreg(AF);
                        sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                            ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(7);
                        break;
                    case 0xCF:			/* RST 8 */
                        if (LogBCalls)
                        {
                            BCallLogData[BCallLogPtr, 0] = PC;
                            BCallLogData[BCallLogPtr, 1] = 8;
                            BCallLogData[BCallLogPtr, 2] = AF;
                            BCallLogData[BCallLogPtr, 3] = BC;
                            BCallLogData[BCallLogPtr, 4] = DE;
                            BCallLogData[BCallLogPtr, 5] = HL;
                            BCallLogData[BCallLogPtr, 6] = IX;
                            BCallLogData[BCallLogPtr, 7] = IY;
                            BCallLogData[BCallLogPtr, 8] = SP;
                            BCallLogPtr = (BCallLogPtr + 1) & BCallLogMask;
                        }
                        PUSH(PC); PC = 8;
                        Clock.IncTime(11);
                        break;
                    case 0xD0:			/* RET NC */
                        if (!TSTFLAG(FLAG_C)) POP(ref PC);
                        if (!TSTFLAG(FLAG_C))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xD1:			/* POP DE */
                        POP(ref DE);
                        Clock.IncTime(10);
                        break;
                    case 0xD2:			/* JP NC,nnnn */
                        PC = (ushort)(!TSTFLAG(FLAG_C) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);
                        break;
                    case 0xD3:			/* OUT (nn),A */
                        temp = GetBYTE_pp(ref PC);
                        temp = temp | (temp << 8);
                        IoPortWrite(this, (ushort)temp, hreg(AF));
                        if (BpAnyIo || (temp & 0xFF) == BpIoWrite)
                            BreakExecution();
                        Clock.IncTime(11);
                        break;
                    case 0xD4:			/* CALL NC,nnnn */
                        if (!TSTFLAG(FLAG_C))
                        {
                            temp = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = (ushort)temp;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xD5:			/* PUSH DE */
                        PUSH(DE);
                        Clock.IncTime(11);
                        break;
                    case 0xD6:			/* SUB nn */
                        temp = GetBYTE_pp(ref PC);
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(7);
                        break;
                    case 0xD7:			/* RST 10H */
                        if (LogBCalls)
                        {
                            BCallLogData[BCallLogPtr, 0] = PC;
                            BCallLogData[BCallLogPtr, 1] = 16;
                            BCallLogData[BCallLogPtr, 2] = AF;
                            BCallLogData[BCallLogPtr, 3] = BC;
                            BCallLogData[BCallLogPtr, 4] = DE;
                            BCallLogData[BCallLogPtr, 5] = HL;
                            BCallLogData[BCallLogPtr, 6] = IX;
                            BCallLogData[BCallLogPtr, 7] = IY;
                            BCallLogData[BCallLogPtr, 8] = SP;
                            BCallLogPtr = (BCallLogPtr + 1) & BCallLogMask;
                        }
                        PUSH(PC); PC = 0x10;
                        Clock.IncTime(11);
                        break;
                    case 0xD8:			/* RET C */
                        if (TSTFLAG(FLAG_C)) POP(ref PC);
                        if (TSTFLAG(FLAG_C))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xD9:			/* EXX */
                        Exx();
                        Clock.IncTime(4);
                        break;
                    case 0xDA:			/* JP C,nnnn */
                        PC = (ushort)(TSTFLAG(FLAG_C) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);
                        break;
                    case 0xDB:			/* IN A,(nn) */
                        //Sethreg(ref AF, Input(GetBYTE_pp(ref PC)));
                        temp = GetBYTE_pp(ref PC);
                        temp = temp | (temp << 8);
                        Sethreg(ref AF, IoPortRead(this, (ushort)temp));
                        if (BpAnyIo || (temp & 0xFF) == BpIoRead)
                            BreakExecution();
                        Clock.IncTime(11);
                        break;
                    case 0xDC:			/* CALL C,nnnn */
                        if (TSTFLAG(FLAG_C))
                        {
                            temp = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = (ushort)temp;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xDD:			/* DD prefix */
                        AssertM1();
                        op = GetBYTE_pp(ref PC);// = GetBYTE_pp(ref PC)
                        ReleaseM1();
                        if (ForceReset)
                        {
                            Reset();
                            return;
                        }
                        switch (op)
                        {
                            case 0x09:			/* ADD IX,BC */
                                IX &= 0xffff;
                                BC &= 0xffff;
                                sum = IX + BC;
                                cbits = (IX ^ BC ^ sum) >> 8;
                                IX = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x19:			/* ADD IX,DE */
                                IX &= 0xffff;
                                DE &= 0xffff;
                                sum = IX + DE;
                                cbits = (IX ^ DE ^ sum) >> 8;
                                IX = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x21:			/* LD IX,nnnn */
                                IX = GetWORD(PC);
                                PC += 2;
                                Clock.IncTime(14);
                                break;
                            case 0x22:			/* LD (nnnn),IX */
                                temp = GetWORD(PC);
                                PutWORD((ushort)temp, IX);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x23:			/* INC IX */
                                ++IX;
                                Clock.IncTime(10);
                                break;
                            case 0x24:			/* INC IXH */
                                IX += 0x100;
                                temp = hreg(IX);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                                    ((temp == 0x80 ? 1 : 0) << 2)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x25:			/* DEC IXH */
                                IX -= 0x100;
                                temp = hreg(IX);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                                    ((temp == 0x7F ? 1 : 0) << 2) | 2
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x26:			/* LD IXH,nn */
                                Sethreg(ref IX, GetBYTE_pp(ref PC));
                                Clock.IncTime(11);
                                break;
                            case 0x29:			/* ADD IX,IX */
                                IX &= 0xffff;
                                sum = IX + IX;
                                cbits = (IX ^ IX ^ sum) >> 8;
                                IX = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x2A:			/* LD IX,(nnnn) */
                                temp = GetWORD(PC);
                                IX = GetWORD((ushort)temp);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x2B:			/* DEC IX */
                                --IX;
                                Clock.IncTime(10);
                                break;
                            case 0x2C:			/* INC IXL */
                                temp = lreg(IX) + 1;
                                Setlreg(ref IX, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                                    ((temp == 0x80 ? 1 : 0) << 2)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x2D:			/* DEC IXL */
                                temp = lreg(IX) - 1;
                                Setlreg(ref IX, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                                    ((temp == 0x7F ? 1 : 0) << 2) | 2
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x2E:			/* LD IXL,nn */
                                Setlreg(ref IX, GetBYTE_pp(ref PC));
                                Clock.IncTime(11);
                                break;
                            case 0x34:			/* INC (IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr) + 1;
                                PutBYTE((ushort)adr, (byte)temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                                    ((temp == 0x80 ? 1 : 0) << 2)
                                    );
                                Clock.IncTime(23);
                                break;
                            case 0x35:			/* DEC (IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr) - 1;
                                PutBYTE((ushort)adr, (byte)temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                                    ((temp == 0x7F ? 1 : 0) << 2) | 2
                                    );
                                Clock.IncTime(23);
                                break;
                            case 0x36:			/* LD (IX+dd),nn */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, GetBYTE_pp(ref PC));
                                Clock.IncTime(19);
                                break;
                            case 0x39:			/* ADD IX,SP */
                                IX &= 0xffff;
                                SP &= 0xffff;
                                sum = IX + SP;
                                cbits = (IX ^ SP ^ sum) >> 8;
                                IX = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x44:			/* LD B,IXH */
                                Sethreg(ref BC, hreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x45:			/* LD B,IXL */
                                Sethreg(ref BC, lreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x46:			/* LD B,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref BC, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x4C:			/* LD C,IXH */
                                Setlreg(ref BC, hreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x4D:			/* LD C,IXL */
                                Setlreg(ref BC, lreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x4E:			/* LD C,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                Setlreg(ref BC, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x54:			/* LD D,IXH */
                                Sethreg(ref DE, hreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x55:			/* LD D,IXL */
                                Sethreg(ref DE, lreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x56:			/* LD D,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref DE, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x5C:			/* LD E,H */
                                Setlreg(ref DE, hreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x5D:			/* LD E,L */
                                Setlreg(ref DE, lreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x5E:			/* LD E,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                Setlreg(ref DE, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x60:			/* LD IXH,B */
                                Sethreg(ref IX, hreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x61:			/* LD IXH,C */
                                Sethreg(ref IX, lreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x62:			/* LD IXH,D */
                                Sethreg(ref IX, hreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x63:			/* LD IXH,E */
                                Sethreg(ref IX, lreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x64:			/* LD IXH,IXH */
                                /* nop */
                                Clock.IncTime(8);
                                break;
                            case 0x65:			/* LD IXH,IXL */
                                Sethreg(ref IX, lreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x66:			/* LD H,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref HL, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x67:			/* LD IXH,A */
                                Sethreg(ref IX, hreg(AF));
                                Clock.IncTime(8);
                                break;
                            case 0x68:			/* LD IXL,B */
                                Setlreg(ref IX, hreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x69:			/* LD IXL,C */
                                Setlreg(ref IX, lreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x6A:			/* LD IXL,D */
                                Setlreg(ref IX, hreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x6B:			/* LD IXL,E */
                                Setlreg(ref IX, lreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x6C:			/* LD IXL,IXH */
                                Setlreg(ref IX, hreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x6D:			/* LD IXL,IXL */
                                /* nop */
                                Clock.IncTime(8);
                                break;
                            case 0x6E:			/* LD L,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                Setlreg(ref HL, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x6F:			/* LD IXL,A */
                                Setlreg(ref IX, hreg(AF));
                                Clock.IncTime(8);
                                break;
                            case 0x70:			/* LD (IX+dd),B */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(BC));
                                Clock.IncTime(19);
                                break;
                            case 0x71:			/* LD (IX+dd),C */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, lreg(BC));
                                Clock.IncTime(19);
                                break;
                            case 0x72:			/* LD (IX+dd),D */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(DE));
                                Clock.IncTime(19);
                                break;
                            case 0x73:			/* LD (IX+dd),E */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, lreg(DE));
                                Clock.IncTime(19);
                                break;
                            case 0x74:			/* LD (IX+dd),H */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(HL));
                                Clock.IncTime(19);
                                break;
                            case 0x75:			/* LD (IX+dd),L */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, lreg(HL));
                                Clock.IncTime(19);
                                break;
                            case 0x77:			/* LD (IX+dd),A */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(AF));
                                Clock.IncTime(19);
                                break;
                            case 0x7C:			/* LD A,IXH */
                                Sethreg(ref AF, hreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x7D:			/* LD A,IXL */
                                Sethreg(ref AF, lreg(IX));
                                Clock.IncTime(8);
                                break;
                            case 0x7E:			/* LD A,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref AF, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x84:			/* ADD A,IXH */
                                temp = hreg(IX);
                                acu = hreg(AF);
                                sum = acu + temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x85:			/* ADD A,IXL */
                                temp = lreg(IX);
                                acu = hreg(AF);
                                sum = acu + temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x86:			/* ADD A,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu + temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0x8C:			/* ADC A,IXH */
                                temp = hreg(IX);
                                acu = hreg(AF);
                                sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x8D:			/* ADC A,IXL */
                                temp = lreg(IX);
                                acu = hreg(AF);
                                sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x8E:			/* ADC A,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0x94:			/* SUB IXH */
                                temp = hreg(IX);
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x95:			/* SUB IXL */
                                temp = lreg(IX);
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x96:			/* SUB (IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0x9C:			/* SBC A,IXH */
                                temp = hreg(IX);
                                acu = hreg(AF);
                                sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x9D:			/* SBC A,IXL */
                                temp = lreg(IX);
                                acu = hreg(AF);
                                sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x9E:			/* SBC A,(IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0xA4:			/* AND IXH */
                                sum = ((AF & (IX)) >> 8) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) |
                                    ((sum == 0 ? 1 : 0) << 6) | 0x10 | partab[sum]
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xA5:			/* AND IXL */
                                sum = ((AF >> 8) & IX) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                                    ((sum == 0 ? 1 : 0) << 6) | partab[sum]
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xA6:			/* AND (IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                sum = ((AF >> 8) & GetBYTE((ushort)adr)) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                                    ((sum == 0 ? 1 : 0) << 6) | partab[sum]
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0xAC:			/* XOR IXH */
                                sum = ((AF ^ (IX)) >> 8) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xAD:			/* XOR IXL */
                                sum = ((AF >> 8) ^ IX) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xAE:			/* XOR (IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                sum = ((AF >> 8) ^ GetBYTE((ushort)adr)) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(19);
                                break;
                            case 0xB4:			/* OR IXH */
                                sum = ((AF | (IX)) >> 8) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xB5:			/* OR IXL */
                                sum = ((AF >> 8) | IX) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xB6:			/* OR (IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                sum = ((AF >> 8) | GetBYTE((ushort)adr)) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(19);
                                break;
                            case 0xBC:			/* CP IXH */
                                temp = hreg(IX);
                                AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xBD:			/* CP IXL */
                                temp = lreg(IX);
                                AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xBE:			/* CP (IX+dd) */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0xCB:			/* CB prefix */
                                adr = IX + (sbyte)GetBYTE_pp(ref PC);
                                switch ((op = GetBYTE(PC)) & 7)
                                {
                                    /*
                                     * default: supresses compiler warning: "warning:
                                     * 'acu' may be used uninitialized in this function"
                                     */
                                    default:
                                    case 0: ++PC; acu = hreg(BC); break;
                                    case 1: ++PC; acu = lreg(BC); break;
                                    case 2: ++PC; acu = hreg(DE); break;
                                    case 3: ++PC; acu = lreg(DE); break;
                                    case 4: ++PC; acu = hreg(HL); break;
                                    case 5: ++PC; acu = lreg(HL); break;
                                    case 6: ++PC; acu = GetBYTE((ushort)adr); break;
                                    case 7: ++PC; acu = hreg(AF); break;
                                }
                                switch (op & 0xc0)
                                {
                                    /*
                                     * Use default to supress compiler warning: "warning:
                                     * 'temp' may be used uninitialized in this function"
                                     */
                                    default:
                                    case 0x00:		/* shift/rotate */
                                        switch (op & 0x38)
                                        {
                                            /*
                                             * Use default: to supress compiler warning
                                             * about 'temp' being used uninitialized
                                             */
                                            default:
                                            case 0x00:	/* RLC */
                                                temp = (acu << 1) | (acu >> 7);
                                                cbits = temp & 1;
                                                goto cbshflg2;
                                            case 0x08:	/* RRC */
                                                temp = (acu >> 1) | (acu << 7);
                                                cbits = temp & 0x80;
                                                goto cbshflg2;
                                            case 0x10:	/* RL */
                                                temp = (acu << 1) | (TSTFLAG(FLAG_C) ? 1 : 0);
                                                cbits = acu & 0x80;
                                                goto cbshflg2;
                                            case 0x18:	/* RR */
                                                temp = (acu >> 1) | ((TSTFLAG(FLAG_C) ? 1 : 0) << 7);
                                                cbits = acu & 1;
                                                goto cbshflg2;
                                            case 0x20:	/* SLA */
                                                temp = acu << 1;
                                                cbits = acu & 0x80;
                                                goto cbshflg2;
                                            case 0x28:	/* SRA */
                                                temp = (acu >> 1) | (acu & 0x80);
                                                cbits = acu & 1;
                                                goto cbshflg2;
                                            case 0x30:	/* SLIA */
                                                temp = (acu << 1) | 1;
                                                cbits = acu & 0x80;
                                                goto cbshflg2;
                                            case 0x38:	/* SRL */
                                                temp = acu >> 1;
                                                cbits = acu & 1;
                                            cbshflg2:
                                                AF = (ushort)((AF & ~0xff) | (temp & 0xa8) |
                                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                                    parity(temp) | (cbits != 0 ? 1 : 0)
                                                    );
                                                break;
                                        }
                                        Clock.IncTime(23);
                                        break;
                                    case 0x40:		/* BIT */
                                        if ((acu & (1 << ((op >> 3) & 7))) != 0)
                                            AF = (ushort)((AF & ~0xfe) | 0x10 |
                                                (((op & 0x38) == 0x38 ? 1 : 0) << 7)
                                        );
                                        else
                                            AF = (ushort)((AF & ~0xfe) | 0x54);
                                        if ((op & 7) != 6)
                                            AF |= (ushort)(acu & 0x28);
                                        temp = acu;
                                        Clock.IncTime(20);
                                        break;
                                    case 0x80:		/* RES */
                                        temp = acu & ~(1 << ((op >> 3) & 7));
                                        Clock.IncTime(23);
                                        break;
                                    case 0xc0:		/* SET */
                                        temp = acu | (1 << ((op >> 3) & 7));
                                        Clock.IncTime(23);
                                        break;
                                }
                                switch (op & 7)
                                {
                                    case 0: Sethreg(ref BC, temp); break;
                                    case 1: Setlreg(ref BC, temp); break;
                                    case 2: Sethreg(ref DE, temp); break;
                                    case 3: Setlreg(ref DE, temp); break;
                                    case 4: Sethreg(ref HL, temp); break;
                                    case 5: Setlreg(ref HL, temp); break;
                                    case 6: PutBYTE((ushort)adr, (byte)temp); break;
                                    case 7: Sethreg(ref AF, temp); break;
                                }
                                break;
                            case 0xE1:			/* POP IX */
                                POP(ref IX);
                                break;
                            case 0xE3:			/* EX (SP),IX */
                                temp = IX; POP(ref IX); PUSH((ushort)temp);
                                break;
                            case 0xE5:			/* PUSH IX */
                                PUSH((ushort)IX);
                                break;
                            case 0xE9:			/* JP (IX) */
                                PC = IX;
                                break;
                            case 0xF9:			/* LD SP,IX */
                                SP = IX;
                                break;
                            default: PC--;		/* ignore DD */
                                Clock.IncTime(4);
                                break;
                        }
                        break;
                    case 0xDE:			/* SBC A,nn */
                        temp = GetBYTE_pp(ref PC);
                        acu = hreg(AF);
                        sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(4);
                        break;
                    case 0xDF:			/* RST 18H */
                        if (LogBCalls)
                        {
                            BCallLogData[BCallLogPtr, 0] = PC;
                            BCallLogData[BCallLogPtr, 1] = 0x18;
                            BCallLogData[BCallLogPtr, 2] = AF;
                            BCallLogData[BCallLogPtr, 3] = BC;
                            BCallLogData[BCallLogPtr, 4] = DE;
                            BCallLogData[BCallLogPtr, 5] = HL;
                            BCallLogData[BCallLogPtr, 6] = IX;
                            BCallLogData[BCallLogPtr, 7] = IY;
                            BCallLogData[BCallLogPtr, 8] = SP;
                            BCallLogPtr = (BCallLogPtr + 1) & BCallLogMask;
                        }
                        PUSH(PC); PC = 0x18;
                        Clock.IncTime(11);
                        break;
                    case 0xE0:			/* RET PO */
                        if (!TSTFLAG(FLAG_P)) POP(ref PC);
                        if (!TSTFLAG(FLAG_P))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xE1:			/* POP HL */
                        POP(ref HL);
                        Clock.IncTime(10);
                        break;
                    case 0xE2:			/* JP PO,nnnn */
                        PC = (ushort)(!TSTFLAG(FLAG_P) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);
                        break;
                    case 0xE3:			/* EX (SP),HL */
                        temp = HL; POP(ref HL); PUSH((ushort)temp);
                        Clock.IncTime(19);
                        break;
                    case 0xE4:			/* CALL PO,nnnn */
                        if (!TSTFLAG(FLAG_P))
                        {
                            ushort adrr = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = adrr;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xE5:			/* PUSH HL */
                        PUSH(HL);
                        Clock.IncTime(11);
                        break;
                    case 0xE6:			/* AND nn */
                        sum = ((AF >> 8) & GetBYTE_pp(ref PC)) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                            ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(7);
                        break;
                    case 0xE7:			/* RST 20H */
                        if (LogBCalls)
                        {
                            BCallLogData[BCallLogPtr, 0] = PC;
                            BCallLogData[BCallLogPtr, 1] = 0x20;
                            BCallLogData[BCallLogPtr, 2] = AF;
                            BCallLogData[BCallLogPtr, 3] = BC;
                            BCallLogData[BCallLogPtr, 4] = DE;
                            BCallLogData[BCallLogPtr, 5] = HL;
                            BCallLogData[BCallLogPtr, 6] = IX;
                            BCallLogData[BCallLogPtr, 7] = IY;
                            BCallLogData[BCallLogPtr, 8] = SP;
                            BCallLogPtr = (BCallLogPtr + 1) & BCallLogMask;
                        }
                        PUSH(PC); PC = 0x20;
                        Clock.IncTime(11);
                        break;
                    case 0xE8:			/* RET PE */
                        if (TSTFLAG(FLAG_P)) POP(ref PC);
                        if (TSTFLAG(FLAG_P))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xE9:			/* JP (HL) */
                        PC = HL;
                        Clock.IncTime(4);
                        break;
                    case 0xEA:			/* JP PE,nnnn */
                        PC = (ushort)(TSTFLAG(FLAG_P) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);
                        break;
                    case 0xEB:			/* EX DE,HL */
                        temp = HL; HL = DE; DE = (ushort)temp;
                        Clock.IncTime(4);
                        break;
                    case 0xEC:			/* CALL PE,nnnn */
                        if (TSTFLAG(FLAG_P))
                        {
                            ushort adrr = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = adrr;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xED:			/* ED prefix */
                        AssertM1();
                        op = GetBYTE_pp(ref PC);
                        ReleaseM1();
                        if (ForceReset)
                        {
                            Reset();
                            return;
                        }
                        switch (op)
                        {
                            case 0x40:			/* IN B,(C) */
                                temp = IoPortRead(this, BC);//lreg(BC)
                                Sethreg(ref BC, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x41:			/* OUT (C),B */
                                IoPortWrite(this, BC, hreg(BC));//lreg(BC)
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x42:			/* SBC HL,BC */
                                HL &= 0xffff;
                                BC &= 0xffff;
                                sum = HL - BC - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = (HL ^ BC ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | 2 | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x43:			/* LD (nnnn),BC */
                                temp = GetWORD(PC);
                                PutWORD((ushort)temp, BC);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x44:			/* NEG */
                                temp = hreg(AF);
                                AF = (ushort)(-(AF & 0xff00) & 0xff00);
                                AF |= (ushort)(((AF >> 8) & 0xa8) | (((AF & 0xff00) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0x0f) != 0 ? 1 : 0) << 4) | ((temp == 0x80 ? 1 : 0) << 2) |
                                    2 | (temp != 0 ? 1 : 0)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x45:			/* RETN */
                                IFF |= (ushort)(IFF >> 1);
                                POP(ref PC);
                                Clock.IncTime(14);
                                if (BpRet)
                                    BreakExecution();
                                break;
                            case 0x46:			/* IM 0 */
                                /* interrupt mode 0 */
                                IM = 0;
                                Clock.IncTime(8);
                                break;
                            case 0x47:			/* LD I,A */
                                IR = (ushort)((IR & 255) | (AF & ~255));
                                Clock.IncTime(9);
                                break;
                            case 0x48:			/* IN C,(C) */
                                temp = IoPortRead(this, BC);//Input(lreg(BC));
                                Setlreg(ref BC, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x49:			/* OUT (C),C */
                                IoPortWrite(this, BC, lreg(BC));//Output(lreg(BC), BC);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x4A:			/* ADC HL,BC */
                                HL &= 0xffff;
                                BC &= 0xffff;
                                sum = HL + BC + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = (HL ^ BC ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x4B:			/* LD BC,(nnnn) */
                                temp = GetWORD(PC);
                                BC = GetWORD((ushort)temp);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x4D:			/* RETI */
                                IFF |= (ushort)(IFF >> 1);
                                POP(ref PC);
                                Clock.IncTime(14);
                                if (BpReti)
                                    BreakExecution();
                                break;
                            case 0x4F:			/* LD R,A */
                                IR = (ushort)((IR & ~255) | ((AF >> 8) & 255));
                                Clock.IncTime(9);
                                break;
                            case 0x50:			/* IN D,(C) */
                                temp = IoPortRead(this, BC);//Input(lreg(BC));
                                Sethreg(ref DE, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x51:			/* OUT (C),D */
                                IoPortWrite(this, BC, hreg(DE));//Output(lreg(BC), DE);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x52:			/* SBC HL,DE */
                                HL &= 0xffff;
                                DE &= 0xffff;
                                sum = HL - DE - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = (HL ^ DE ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | 2 | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x53:			/* LD (nnnn),DE */
                                temp = GetWORD(PC);
                                PutWORD((ushort)temp, DE);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x56:			/* IM 1 */
                                IM = 1;
                                /* interrupt mode 1 */
                                Clock.IncTime(8);
                                break;
                            case 0x57:			/* LD A,I */
                                AF = (ushort)((AF & 0x29) | (IR & ~255) | ((IR >> 8) & 0x80) | (((IR & ~255) == 0 ? 1 : 0) << 6) | ((IFF & 2) << 1));
                                Clock.IncTime(9);
                                break;
                            case 0x58:			/* IN E,(C) */
                                temp = IoPortRead(this, BC);//Input(lreg(BC));
                                Setlreg(ref DE, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x59:			/* OUT (C),E */
                                IoPortWrite(this, BC, lreg(DE));
                                //Output(lreg(BC), DE);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x5A:			/* ADC HL,DE */
                                HL &= 0xffff;
                                DE &= 0xffff;
                                sum = HL + DE + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = (HL ^ DE ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x5B:			/* LD DE,(nnnn) */
                                temp = GetWORD(PC);
                                DE = GetWORD((ushort)temp);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x5E:			/* IM 2 */
                                /* interrupt mode 2 */
                                IM = 2;
                                Clock.IncTime(8);
                                break;
                            case 0x5F:			/* LD A,R */
                                AF = (ushort)((AF & 0x29) | ((IR & 255) << 8) | (IR & 0x80) | (((IR & 255) == 0 ? 1 : 0) << 6) | ((IFF & 2) << 1));
                                Clock.IncTime(9);
                                break;
                            case 0x60:			/* IN H,(C) */
                                temp = IoPortRead(this, BC);//Input(lreg(BC));
                                Sethreg(ref HL, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x61:			/* OUT (C),H */
                                //Output(lreg(BC), HL);
                                IoPortWrite(this, BC, hreg(HL));
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x62:			/* SBC HL,HL */
                                HL &= 0xffff;
                                sum = HL - HL - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = (HL ^ HL ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | 2 | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x63:			/* LD (nnnn),HL */
                                temp = GetWORD(PC);
                                PutWORD((ushort)temp, HL);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x67:			/* RRD */
                                temp = GetBYTE(HL);
                                acu = hreg(AF);
                                PutBYTE(HL, (byte)(hdig((ushort)temp) | (ldig((ushort)acu) << 4)));
                                acu = (acu & 0xf0) | ldig((ushort)temp);
                                AF = (ushort)((acu << 8) | (acu & 0xa8) | (((acu & 0xff) == 0 ? 1 : 0) << 6) |
                                    partab[acu] | (AF & 1));
                                Clock.IncTime(18);
                                break;
                            case 0x68:			/* IN L,(C) */
                                temp = IoPortRead(this, BC);//Input(lreg(BC));
                                Setlreg(ref HL, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x69:			/* OUT (C),L */
                                IoPortWrite(this, BC, lreg(HL));//Output(lreg(BC), HL);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x6A:			/* ADC HL,HL */
                                HL &= 0xffff;
                                sum = (ushort)(HL + HL + (TSTFLAG(FLAG_C) ? 1 : 0));
                                cbits = (HL ^ HL ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x6B:			/* LD HL,(nnnn) */
                                temp = GetWORD(PC);
                                HL = GetWORD((ushort)temp);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x6F:			/* RLD */
                                temp = GetBYTE(HL);
                                acu = hreg(AF);
                                PutBYTE(HL, (byte)((ldig(temp) << 4) | ldig(acu)));
                                acu = (acu & 0xf0) | hdig(temp);
                                AF = (ushort)((acu << 8) | (acu & 0xa8) | (((acu & 0xff) == 0 ? 1 : 0) << 6) |
                                    partab[acu] | (AF & 1));
                                Clock.IncTime(18);
                                break;
                            case 0x70:			/* IN (C) */
                                temp = IoPortRead(this, BC);//Input(lreg(BC));
                                //Setlreg(ref temp, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x71:			/* OUT (C),0 */
                                IoPortWrite(this, BC, 0);//Output(lreg(BC), 0);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x72:			/* SBC HL,SP */
                                HL &= 0xffff;
                                SP &= 0xffff;
                                sum = (ushort)(HL - SP - (TSTFLAG(FLAG_C) ? 1 : 0));
                                cbits = (HL ^ SP ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | 2 | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x73:			/* LD (nnnn),SP */
                                temp = GetWORD(PC);
                                PutWORD((ushort)temp, SP);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x78:			/* IN A,(C) */
                                temp = IoPortRead(this, BC);//Input(lreg(BC));
                                Sethreg(ref AF, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    parity(temp)
                                    );
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x79:			/* OUT (C),A */
                                IoPortWrite(this, BC, hreg(AF));//Output(lreg(BC), AF);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(12);
                                break;
                            case 0x7A:			/* ADC HL,SP */
                                HL &= 0xffff;
                                SP &= 0xffff;
                                sum = (ushort)(HL + SP + (TSTFLAG(FLAG_C) ? 1 : 0));
                                cbits = (HL ^ SP ^ sum) >> 8;
                                HL = (ushort)sum;
                                AF = (ushort)((AF & ~0xff) | ((sum >> 8) & 0xa8) |
                                    (((sum & 0xffff) == 0 ? 1 : 0) << 6) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x7B:			/* LD SP,(nnnn) */
                                temp = GetWORD(PC);
                                SP = GetWORD(temp);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0xA0:			/* LDI */
                                acu = GetBYTE_pp(ref HL);
                                PutBYTE_pp(ref DE, (byte)acu);
                                acu += hreg(AF);
                                AF = (ushort)((AF & ~0x3e) | (acu & 8) | ((acu & 2) << 4) |
                                    (((--BC & 0xffff) != 0 ? 1 : 0) << 2));
                                Clock.IncTime(16);
                                break;
                            case 0xA1:			/* CPI */
                                acu = hreg(AF);
                                temp = GetBYTE_pp(ref HL);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                //AF = (AF & ~0xfe) | (sum & 0x80) | (!(sum & 0xff) << 6) |
                                //  sum & 0xff != 0
                                AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | ((!((sum & 0xff) != 0) ? 1 : 0) << 6) |
                                    (((sum - ((cbits & 16) >> 4)) & 2) << 4) | (cbits & 16) |
                                    ((sum - ((cbits >> 4) & 1)) & 8) |
                                    ((--BC & 0xffff) != 0 ? 1 : 0) << 2 | 2
                                    );
                                if ((sum & 15) == 8 && (cbits & 16) != 0)
                                    AF &= (ushort)(~8);
                                Clock.IncTime(16);
                                break;
                            case 0xA2:			/* INI */
                                PutBYTE(HL, IoPortRead(this, BC)); ++HL;//PutBYTE(HL, Input(lreg(BC))); ++HL;
                                SETFLAG(FLAG_N, true);
                                SETFLAG(FLAG_P, (--BC & 0xffff) != 0);
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(16);
                                break;
                            case 0xA3:			/* OUTI */
                                IoPortWrite(this, BC, GetBYTE(HL)); ++HL;//Output(lreg(BC), GetBYTE(HL)); ++HL;
                                SETFLAG(FLAG_N, true);
                                Sethreg(ref BC, hreg(BC) - 1);
                                SETFLAG(FLAG_Z, hreg(BC) == 0);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(16);
                                break;
                            case 0xA8:			/* LDD */
                                acu = GetBYTE_mm(ref HL);
                                PutBYTE_mm(ref DE, (byte)acu);
                                acu += hreg(AF);
                                AF = (ushort)((AF & ~0x3e) | (acu & 8) | ((acu & 2) << 4) |
                                    (((--BC & 0xffff) != 0 ? 1 : 0) << 2));
                                Clock.IncTime(16);
                                break;
                            case 0xA9:			/* CPD */
                                acu = hreg(AF);
                                temp = GetBYTE_mm(ref HL);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                //AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | (!(sum & 0xff) << 6) |
                                AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | ((!((sum & 0xff) != 0) ? 1 : 0) << 6) |
                                    (((sum - ((cbits & 16) >> 4)) & 2) << 4) | (cbits & 16) |
                                    ((sum - ((cbits >> 4) & 1)) & 8) |
                                    ((--BC & 0xffff) != 0 ? 1 : 0) << 2 | 2);
                                if ((sum & 15) == 8 && (cbits & 16) != 0)
                                    AF &= (ushort)(~8);
                                Clock.IncTime(16);
                                break;
                            case 0xAA:			/* IND */
                                //PutBYTE(HL, Input(lreg(BC))); --HL;
                                PutBYTE(HL, IoPortRead(this, BC)); --HL;
                                SETFLAG(FLAG_N, true);
                                Sethreg(ref BC, lreg(BC) - 1);
                                SETFLAG(FLAG_Z, lreg(BC) == 0);
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                Clock.IncTime(16);
                                break;
                            case 0xAB:			/* OUTD */
                                //Output(lreg(BC), GetBYTE(HL)); --HL;
                                IoPortWrite(this, BC, GetBYTE(HL)); --HL;
                                SETFLAG(FLAG_N, true);
                                Sethreg(ref BC, hreg(BC) - 1);
                                SETFLAG(FLAG_Z, hreg(BC) == 0);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                Clock.IncTime(16);
                                break;
                            case 0xB0:			/* LDIR */
                                /*acu = hreg(AF);
                                BC &= 0xffff;
                                if (BC == 0)
                                    BC = 0x10000;
                                do {
                                    acu = GetBYTE_pp(ref HL);
                                    PutBYTE_pp(ref DE, acu);
                                } while (--BC);*/
                                acu = GetBYTE_pp(ref HL);
                                PutBYTE_pp(ref DE, acu);
                                BC--;
                                acu += hreg(AF);
                                AF = (ushort)((AF & ~0x3e) | (acu & 8) | ((acu & 2) << 4));
                                if (BC != 0)
                                {
                                    PC -= 2;
                                    AF |= FLAG_P;
                                    Clock.IncTime(21);
                                }
                                else
                                {
                                    AF &= (ushort)(~FLAG_P);
                                    Clock.IncTime(16);
                                }
                                break;
                            case 0xB1:			/* CPIR */
                                /*acu = hreg(AF);
                                BC &= 0xffff;
                                if (BC == 0)
                                    BC = 0x10000;
                                do {
                                    temp = GetBYTE_pp(ref HL);
                                    op = --BC != 0;
                                    sum = acu - temp;
                                } while (op && sum != 0);*/
                                /*acu = A;
                                temp = GetBYTE_pp(ref HL);
                                op = --BC != 0 ? 1 : 0;
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | ((!((sum & 0xff) != 0) ? 1 : 0) << 6) |
                                    (((sum - ((cbits & 16) >> 4)) & 2) << 4) |
                                    (cbits & 16) | ((sum - ((cbits >> 4) & 1)) & 8) |
                                    op << 2 | 2
                                    );
                                if ((sum & 15) == 8 && (cbits & 16) != 0)
                                    AF &= (ushort)(~8);
                                if (op != 0 && sum != 0)
                                {
                                    PC -= 2;
                                    //AF |= FLAG_P;
                                    Clock.IncTime(21);
                                }
                                else
                                {
                                    //AF &= (ushort)(~FLAG_P);
                                    Clock.IncTime(16);
                                }
                                if (BC != 0)
                                    AF |= FLAG_P;
                                else
                                    AF &= (ushort)(~FLAG_P);*/
                                acu = hreg(AF);
                                temp = GetBYTE_pp(ref HL);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                //AF = (AF & ~0xfe) | (sum & 0x80) | (!(sum & 0xff) << 6) |
                                //  sum & 0xff != 0
                                AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | ((!((sum & 0xff) != 0) ? 1 : 0) << 6) |
                                    (((sum - ((cbits & 16) >> 4)) & 2) << 4) | (cbits & 16) |
                                    ((sum - ((cbits >> 4) & 1)) & 8) |
                                    ((--BC & 0xffff) != 0 ? 1 : 0) << 2 | 2
                                    );
                                if ((sum & 15) == 8 && (cbits & 16) != 0)
                                    AF &= (ushort)(~8);
                                if (BC != 0 && sum != 0)
                                {
                                    PC -= 2;
                                    Clock.IncTime(21);
                                }
                                else
                                {
                                    Clock.IncTime(16);
                                }
                                if (BC != 0)
                                    AF |= FLAG_P;
                                else
                                    AF &= (ushort)(~FLAG_P);
                                break;
                            case 0xB2:			/* INIR */
                                /*temp = hreg(BC);
                                do {
                                    PutBYTE(HL, Input(lreg(BC))); ++HL;
                                } while (--temp);
                                Sethreg(ref BC, 0);
                                SETFLAG(FLAG_N, 1);
                                SETFLAG(FLAG_Z, 1);*/
                                PutBYTE(HL, IoPortRead(this, BC)); ++HL;
                                SETFLAG(FLAG_N, true);
                                SETFLAG(FLAG_Z, --B == 0);
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                if (B != 0)
                                {
                                    PC -= 2;
                                    Clock.IncTime(21);
                                }
                                else
                                    Clock.IncTime(16);
                                break;
                            case 0xB3:			/* OTIR */
                                /*temp = hreg(BC);
                                do {
                                    Output(lreg(BC), GetBYTE(HL)); ++HL;
                                } while (--temp);
                                Sethreg(ref BC, 0);
                                SETFLAG(FLAG_N, 1);
                                SETFLAG(FLAG_Z, 1);*/
                                IoPortWrite(this, BC, GetBYTE(HL)); ++HL;
                                SETFLAG(FLAG_N, true);
                                SETFLAG(FLAG_Z, --B == 0);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                if (B != 0)
                                {
                                    PC -= 2;
                                    Clock.IncTime(21);
                                }
                                else
                                    Clock.IncTime(16);
                                break;
                            case 0xB8:			/* LDDR */
                                /*BC &= 0xffff;
                                if (BC == 0)
                                    BC = 0x10000;
                                do {
                                    acu = GetBYTE_mm(ref HL);
                                    PutBYTE_mm(ref DE, acu);
                                } while (--BC);
                                acu += hreg(AF);
                                AF = (AF & ~0x3e) | (acu & 8) | ((acu & 2) << 4);*/
                                acu = GetBYTE_mm(ref HL);
                                PutBYTE_mm(ref DE, acu);
                                BC--;
                                acu += hreg(AF);
                                AF = (ushort)((AF & ~0x3e) | (acu & 8) | ((acu & 2) << 4));
                                if (BC != 0)
                                {
                                    PC -= 2;
                                    AF |= FLAG_P;
                                    Clock.IncTime(21);
                                }
                                else
                                {
                                    AF &= (ushort)(~FLAG_P);
                                    Clock.IncTime(16);
                                }
                                break;
                            case 0xB9:			/* CPDR */
                                /*acu = hreg(AF);
                                BC &= 0xffff;
                                if (BC == 0)
                                    BC = 0x10000;
                                do {
                                    temp = GetBYTE_mm(ref HL);
                                    op = --BC != 0;
                                    sum = acu - temp;
                                } while (op && sum != 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (AF & ~0xfe) | (sum & 0x80) | (!(sum & 0xff) << 6) |
                                    (((sum - ((cbits&16)>>4))&2) << 4) |
                                    (cbits & 16) | ((sum - ((cbits >> 4) & 1)) & 8) |
                                    op << 2 | 2;
                                if ((sum & 15) == 8 && (cbits & 16) != 0)
                                    AF &= ~8;*/
                                /*acu = A;
                                temp = GetBYTE_mm(ref HL);
                                op = --BC != 0 ? 1 : 0;
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | ((!((sum & 0xff) != 0) ? 1 : 0) << 6) |
                                    (((sum - ((cbits & 16) >> 4)) & 2) << 4) |
                                    (cbits & 16) | ((sum - ((cbits >> 4) & 1)) & 8) |
                                    op << 2 | 2
                                    );
                                if ((sum & 15) == 8 && (cbits & 16) != 0)
                                    AF &= (ushort)(~8);
                                if (op != 0 && sum != 0)
                                {
                                    PC -= 2;
                                    //AF |= FLAG_P;
                                    Clock.IncTime(21);
                                }
                                else
                                {
                                    //AF &= (ushort)(~FLAG_P);
                                    Clock.IncTime(16);
                                }
                                if (BC != 0)
                                    AF |= FLAG_P;
                                else
                                    AF &= (ushort)(~FLAG_P);*/
                                acu = hreg(AF);
                                temp = GetBYTE_mm(ref HL);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                //AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | (!(sum & 0xff) << 6) |
                                AF = (ushort)((AF & ~0xfe) | (sum & 0x80) | ((!((sum & 0xff) != 0) ? 1 : 0) << 6) |
                                    (((sum - ((cbits & 16) >> 4)) & 2) << 4) | (cbits & 16) |
                                    ((sum - ((cbits >> 4) & 1)) & 8) |
                                    ((--BC & 0xffff) != 0 ? 1 : 0) << 2 | 2);
                                if ((sum & 15) == 8 && (cbits & 16) != 0)
                                    AF &= (ushort)(~8);
                                Clock.IncTime(16);
                                if (BC != 0 && sum != 0)
                                {
                                    PC -= 2;
                                    Clock.IncTime(21);
                                }
                                else
                                {
                                    Clock.IncTime(16);
                                }
                                if (BC != 0)
                                    AF |= FLAG_P;
                                else
                                    AF &= (ushort)(~FLAG_P);
                                break;
                            case 0xBA:			/* INDR */
                                /*temp = hreg(BC);
                                do {
                                    PutBYTE(HL, Input(lreg(BC))); --HL;
                                } while (--temp);
                                Sethreg(ref BC, 0);
                                SETFLAG(FLAG_N, 1);
                                SETFLAG(FLAG_Z, 1);*/
                                PutBYTE(HL, IoPortRead(this, BC)); --HL;
                                SETFLAG(FLAG_N, true);
                                SETFLAG(FLAG_Z, --B == 0);
                                if (BpAnyIo || C == BpIoRead)
                                    BreakExecution();
                                if (B != 0)
                                {
                                    PC -= 2;
                                    Clock.IncTime(21);
                                }
                                else
                                    Clock.IncTime(16);
                                break;
                            case 0xBB:			/* OTDR */
                                /*temp = hreg(BC);
                                do {
                                    Output(lreg(BC), GetBYTE(HL)); --HL;
                                } while (--temp);
                                Sethreg(ref BC, 0);
                                SETFLAG(FLAG_N, 1);
                                SETFLAG(FLAG_Z, 1);*/
                                IoPortWrite(this, BC, GetBYTE(HL)); --HL;
                                SETFLAG(FLAG_N, true);
                                SETFLAG(FLAG_Z, --B == 0);
                                if (BpAnyIo || C == BpIoWrite)
                                    BreakExecution();
                                if (B != 0)
                                {
                                    PC -= 2;
                                    Clock.IncTime(21);
                                }
                                else
                                    Clock.IncTime(16);
                                break;
                            //default: if (0x40 <= op && op <= 0x7f) PC--;		/* ignore ED */
                            // Do nothing.
                        }
                        break;
                    case 0xEE:			/* XOR nn */
                        sum = ((AF >> 8) ^ GetBYTE_pp(ref PC)) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(7);
                        break;
                    case 0xEF:			/* RST 28H */
                        if (LogBCalls)
                        {
                            BCallLogData[BCallLogPtr, 0] = PC;
                            BCallLogData[BCallLogPtr, 1] = (ushort)(MemoryRead(null, (ushort)(PC + 0)) | (MemoryRead(null, (ushort)(PC + 1)) << 8));
                            BCallLogData[BCallLogPtr, 2] = AF;
                            BCallLogData[BCallLogPtr, 3] = BC;
                            BCallLogData[BCallLogPtr, 4] = DE;
                            BCallLogData[BCallLogPtr, 5] = HL;
                            BCallLogData[BCallLogPtr, 6] = IX;
                            BCallLogData[BCallLogPtr, 7] = IY;
                            BCallLogData[BCallLogPtr, 8] = SP;
                            BCallLogPtr = (BCallLogPtr + 1) & BCallLogMask;
                        }
                        PUSH(PC); PC = 0x28;
                        Clock.IncTime(11);
                        break;
                    case 0xF0:			/* RET P */
                        if (!TSTFLAG(FLAG_S)) POP(ref PC);
                        if (!TSTFLAG(FLAG_S))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xF1:			/* POP AF */
                        POP(ref AF);
                        Clock.IncTime(10);  
                        break;
                    case 0xF2:			/* JP P,nnnn */
                        PC = (ushort)(!TSTFLAG(FLAG_S) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);
                        break;
                    case 0xF3:			/* DI */
                        IFF = 0;
                        Clock.IncTime(4);
                        break;
                    case 0xF4:			/* CALL P,nnnn */
                        if (!TSTFLAG(FLAG_S))
                        {
                            temp = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = (ushort)temp;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xF5:			/* PUSH AF */
                        PUSH(AF);
                        break;
                    case 0xF6:			/* OR nn */
                        sum = ((AF >> 8) | GetBYTE_pp(ref PC)) & 0xff;
                        AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                        Clock.IncTime(7);
                        break;
                    case 0xF7:			/* RST 30H */
                        if (LogBCalls)
                        {
                            BCallLogData[BCallLogPtr, 0] = PC;
                            BCallLogData[BCallLogPtr, 1] = 0x30;
                            BCallLogData[BCallLogPtr, 2] = AF;
                            BCallLogData[BCallLogPtr, 3] = BC;
                            BCallLogData[BCallLogPtr, 4] = DE;
                            BCallLogData[BCallLogPtr, 5] = HL;
                            BCallLogData[BCallLogPtr, 6] = IX;
                            BCallLogData[BCallLogPtr, 7] = IY;
                            BCallLogData[BCallLogPtr, 8] = SP;
                            BCallLogPtr = (BCallLogPtr + 1) & BCallLogMask;
                        }
                        PUSH(PC); PC = 0x30;
                        Clock.IncTime(11);
                        break;
                    case 0xF8:			/* RET M */
                        if (TSTFLAG(FLAG_S)) POP(ref PC);
                        if (TSTFLAG(FLAG_S))
                        {
                            Clock.IncTime(11);
                            if (BpRet)
                                BreakExecution();
                        }
                        else
                            Clock.IncTime(5);
                        break;
                    case 0xF9:			/* LD SP,HL */
                        SP = HL;
                        Clock.IncTime(6);
                        break;
                    case 0xFA:			/* JP M,nnnn */
                        PC = (ushort)(TSTFLAG(FLAG_S) ? GetWORD(PC) : PC + 2);
                        Clock.IncTime(10);
                        break;
                    case 0xFB:			/* EI */
                        IFF = 3;
                        Clock.IncTime(4);
                        break;
                    case 0xFC:			/* CALL M,nnnn */
                        if (TSTFLAG(FLAG_S))
                        {
                            temp = GetWORD(PC);
                            PUSH((ushort)(PC + 2));
                            PC = (ushort)temp;
                            Clock.IncTime(17);
                        }
                        else
                        {
                            PC += 2;
                            Clock.IncTime(10);
                        }
                        break;
                    case 0xFD:			/* FD prefix */
                        AssertM1();
                        op = GetBYTE_pp(ref PC);// = GetBYTE_pp(ref PC)
                        ReleaseM1();
                        if (ForceReset)
                        {
                            Reset();
                            return;
                        }
                        switch (op)
                        {
                            case 0x09:			/* ADD IY,BC */
                                IY &= 0xffff;
                                BC &= 0xffff;
                                sum = IY + BC;
                                cbits = (IY ^ BC ^ sum) >> 8;
                                IY = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x19:			/* ADD IY,DE */
                                IY &= 0xffff;
                                DE &= 0xffff;
                                sum = IY + DE;
                                cbits = (IY ^ DE ^ sum) >> 8;
                                IY = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x21:			/* LD IY,nnnn */
                                IY = GetWORD(PC);
                                PC += 2;
                                Clock.IncTime(14);
                                break;
                            case 0x22:			/* LD (nnnn),IY */
                                temp = GetWORD(PC);
                                PutWORD((ushort)temp, IY);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x23:			/* INC IY */
                                ++IY;
                                Clock.IncTime(10);
                                break;
                            case 0x24:			/* INC IYH */
                                IY += 0x100;
                                temp = hreg(IY);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                                    ((temp == 0x80 ? 1 : 0) << 2)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x25:			/* DEC IYH */
                                IY -= 0x100;
                                temp = hreg(IY);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                                    ((temp == 0x7F ? 1 : 0) << 2) | 2
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x26:			/* LD IYH,nn */
                                Sethreg(ref IY, GetBYTE_pp(ref PC));
                                Clock.IncTime(11);
                                break;
                            case 0x29:			/* ADD IY,IY */
                                IY &= 0xffff;
                                sum = IY + IY;
                                cbits = (IY ^ IY ^ sum) >> 8;
                                IY = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x2A:			/* LD IY,(nnnn) */
                                temp = GetWORD(PC);
                                IY = GetWORD((ushort)temp);
                                PC += 2;
                                Clock.IncTime(20);
                                break;
                            case 0x2B:			/* DEC IY */
                                --IY;
                                Clock.IncTime(10);
                                break;
                            case 0x2C:			/* INC IYL */
                                temp = lreg(IY) + 1;
                                Setlreg(ref IY, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                                    ((temp == 0x80 ? 1 : 0) << 2)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x2D:			/* DEC IYL */
                                temp = lreg(IY) - 1;
                                Setlreg(ref IY, temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                                    ((temp == 0x7F ? 1 : 0) << 2) | 2
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x2E:			/* LD IYL,nn */
                                Setlreg(ref IY, GetBYTE_pp(ref PC));
                                Clock.IncTime(11);
                                break;
                            case 0x34:			/* INC (IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr) + 1;
                                PutBYTE((ushort)adr, (byte)temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0 ? 1 : 0) << 4) |
                                    ((temp == 0x80 ? 1 : 0) << 2)
                                    );
                                Clock.IncTime(23);
                                break;
                            case 0x35:			/* DEC (IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr) - 1;
                                PutBYTE((ushort)adr, (byte)temp);
                                AF = (ushort)((AF & ~0xfe) | (temp & 0xa8) |
                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                    (((temp & 0xf) == 0xf ? 1 : 0) << 4) |
                                    ((temp == 0x7F ? 1 : 0) << 2) | 2
                                    );
                                Clock.IncTime(23);
                                break;
                            case 0x36:			/* LD (IY+dd),nn */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, GetBYTE_pp(ref PC));
                                Clock.IncTime(19);
                                break;
                            case 0x39:			/* ADD IY,SP */
                                IY &= 0xffff;
                                SP &= 0xffff;
                                sum = IY + SP;
                                cbits = (IY ^ SP ^ sum) >> 8;
                                IY = (ushort)sum;
                                AF = (ushort)((AF & ~0x3b) | ((sum >> 8) & 0x28) |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(15);
                                break;
                            case 0x44:			/* LD B,IYH */
                                Sethreg(ref BC, hreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x45:			/* LD B,IYL */
                                Sethreg(ref BC, lreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x46:			/* LD B,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref BC, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x4C:			/* LD C,IYH */
                                Setlreg(ref BC, hreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x4D:			/* LD C,IYL */
                                Setlreg(ref BC, lreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x4E:			/* LD C,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                Setlreg(ref BC, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x54:			/* LD D,IYH */
                                Sethreg(ref DE, hreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x55:			/* LD D,IYL */
                                Sethreg(ref DE, lreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x56:			/* LD D,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref DE, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x5C:			/* LD E,H */
                                Setlreg(ref DE, hreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x5D:			/* LD E,L */
                                Setlreg(ref DE, lreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x5E:			/* LD E,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                Setlreg(ref DE, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x60:			/* LD IYH,B */
                                Sethreg(ref IY, hreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x61:			/* LD IYH,C */
                                Sethreg(ref IY, lreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x62:			/* LD IYH,D */
                                Sethreg(ref IY, hreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x63:			/* LD IYH,E */
                                Sethreg(ref IY, lreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x64:			/* LD IYH,IYH */
                                /* nop */
                                Clock.IncTime(8);
                                break;
                            case 0x65:			/* LD IYH,IYL */
                                Sethreg(ref IY, lreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x66:			/* LD H,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref HL, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x67:			/* LD IYH,A */
                                Sethreg(ref IY, hreg(AF));
                                Clock.IncTime(8);
                                break;
                            case 0x68:			/* LD IYL,B */
                                Setlreg(ref IY, hreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x69:			/* LD IYL,C */
                                Setlreg(ref IY, lreg(BC));
                                Clock.IncTime(8);
                                break;
                            case 0x6A:			/* LD IYL,D */
                                Setlreg(ref IY, hreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x6B:			/* LD IYL,E */
                                Setlreg(ref IY, lreg(DE));
                                Clock.IncTime(8);
                                break;
                            case 0x6C:			/* LD IYL,IYH */
                                Setlreg(ref IY, hreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x6D:			/* LD IYL,IYL */
                                /* nop */
                                Clock.IncTime(8);
                                break;
                            case 0x6E:			/* LD L,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                Setlreg(ref HL, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x6F:			/* LD IYL,A */
                                Setlreg(ref IY, hreg(AF));
                                Clock.IncTime(8);
                                break;
                            case 0x70:			/* LD (IY+dd),B */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(BC));
                                Clock.IncTime(19);
                                break;
                            case 0x71:			/* LD (IY+dd),C */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, lreg(BC));
                                Clock.IncTime(19);
                                break;
                            case 0x72:			/* LD (IY+dd),D */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(DE));
                                Clock.IncTime(19);
                                break;
                            case 0x73:			/* LD (IY+dd),E */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, lreg(DE));
                                Clock.IncTime(19);
                                break;
                            case 0x74:			/* LD (IY+dd),H */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(HL));
                                Clock.IncTime(19);
                                break;
                            case 0x75:			/* LD (IY+dd),L */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, lreg(HL));
                                Clock.IncTime(19);
                                break;
                            case 0x77:			/* LD (IY+dd),A */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                PutBYTE((ushort)adr, hreg(AF));
                                Clock.IncTime(19);
                                break;
                            case 0x7C:			/* LD A,IYH */
                                Sethreg(ref AF, hreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x7D:			/* LD A,IYL */
                                Sethreg(ref AF, lreg(IY));
                                Clock.IncTime(8);
                                break;
                            case 0x7E:			/* LD A,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                Sethreg(ref AF, GetBYTE((ushort)adr));
                                Clock.IncTime(19);
                                break;
                            case 0x84:			/* ADD A,IYH */
                                temp = hreg(IY);
                                acu = hreg(AF);
                                sum = acu + temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x85:			/* ADD A,IYL */
                                temp = lreg(IY);
                                acu = hreg(AF);
                                sum = acu + temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x86:			/* ADD A,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu + temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0x8C:			/* ADC A,IYH */
                                temp = hreg(IY);
                                acu = hreg(AF);
                                sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x8D:			/* ADC A,IYL */
                                temp = lreg(IY);
                                acu = hreg(AF);
                                sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x8E:			/* ADC A,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu + temp + (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0x94:			/* SUB IYH */
                                temp = hreg(IY);
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x95:			/* SUB IYL */
                                temp = lreg(IY);
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x96:			/* SUB (IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0x9C:			/* SBC A,IYH */
                                temp = hreg(IY);
                                acu = hreg(AF);
                                sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x9D:			/* SBC A,IYL */
                                temp = lreg(IY);
                                acu = hreg(AF);
                                sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0x9E:			/* SBC A,(IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                acu = hreg(AF);
                                sum = acu - temp - (TSTFLAG(FLAG_C) ? 1 : 0);
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)(((sum & 0xff) << 8) | (sum & 0xa8) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (cbits & 0x10) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0xA4:			/* AND IYH */
                                sum = ((AF & (IY)) >> 8) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) |
                                    ((sum == 0 ? 1 : 0) << 6) | 0x10 | partab[sum]
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xA5:			/* AND IYL */
                                sum = ((AF >> 8) & IY) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                                    ((sum == 0 ? 1 : 0) << 6) | partab[sum]
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xA6:			/* AND (IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                sum = ((AF >> 8) & GetBYTE((ushort)adr)) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | 0x10 |
                                    ((sum == 0 ? 1 : 0) << 6) | partab[sum]
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0xAC:			/* XOR IYH */
                                sum = ((AF ^ (IY)) >> 8) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xAD:			/* XOR IYL */
                                sum = ((AF >> 8) ^ IY) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xAE:			/* XOR (IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                sum = ((AF >> 8) ^ GetBYTE((ushort)adr)) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(19);
                                break;
                            case 0xB4:			/* OR IYH */
                                sum = ((AF | (IY)) >> 8) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xB5:			/* OR IYL */
                                sum = ((AF >> 8) | IY) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(8);
                                break;
                            case 0xB6:			/* OR (IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                sum = ((AF >> 8) | GetBYTE((ushort)adr)) & 0xff;
                                AF = (ushort)((sum << 8) | (sum & 0xa8) | ((sum == 0 ? 1 : 0) << 6) | partab[sum]);
                                Clock.IncTime(19);
                                break;
                            case 0xBC:			/* CP IYH */
                                temp = hreg(IY);
                                AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xBD:			/* CP IYL */
                                temp = lreg(IY);
                                AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(8);
                                break;
                            case 0xBE:			/* CP (IY+dd) */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                temp = GetBYTE((ushort)adr);
                                AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                                acu = hreg(AF);
                                sum = acu - temp;
                                cbits = acu ^ temp ^ sum;
                                AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                                    (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                                    (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                                    (cbits & 0x10) | ((cbits >> 8) & 1)
                                    );
                                Clock.IncTime(19);
                                break;
                            case 0xCB:			/* CB prefIY */
                                adr = IY + (sbyte)GetBYTE_pp(ref PC);
                                switch ((op = GetBYTE(PC)) & 7)
                                {
                                    /*
                                     * default: supresses compiler warning: "warning:
                                     * 'acu' may be used uninitialized in this function"
                                     */
                                    default:
                                    case 0: ++PC; acu = hreg(BC); break;
                                    case 1: ++PC; acu = lreg(BC); break;
                                    case 2: ++PC; acu = hreg(DE); break;
                                    case 3: ++PC; acu = lreg(DE); break;
                                    case 4: ++PC; acu = hreg(HL); break;
                                    case 5: ++PC; acu = lreg(HL); break;
                                    case 6: ++PC; acu = GetBYTE((ushort)adr); break;
                                    case 7: ++PC; acu = hreg(AF); break;
                                }
                                switch (op & 0xc0)
                                {
                                    /*
                                     * Use default to supress compiler warning: "warning:
                                     * 'temp' may be used uninitialized in this function"
                                     */
                                    default:
                                    case 0x00:		/* shift/rotate */
                                        switch (op & 0x38)
                                        {
                                            /*
                                             * Use default: to supress compiler warning
                                             * about 'temp' being used uninitialized
                                             */
                                            default:
                                            case 0x00:	/* RLC */
                                                temp = (acu << 1) | (acu >> 7);
                                                cbits = temp & 1;
                                                goto cbshflg2;
                                            case 0x08:	/* RRC */
                                                temp = (acu >> 1) | (acu << 7);
                                                cbits = temp & 0x80;
                                                goto cbshflg2;
                                            case 0x10:	/* RL */
                                                temp = (acu << 1) | (TSTFLAG(FLAG_C) ? 1 : 0);
                                                cbits = acu & 0x80;
                                                goto cbshflg2;
                                            case 0x18:	/* RR */
                                                temp = (acu >> 1) | ((TSTFLAG(FLAG_C) ? 1 : 0) << 7);
                                                cbits = acu & 1;
                                                goto cbshflg2;
                                            case 0x20:	/* SLA */
                                                temp = acu << 1;
                                                cbits = acu & 0x80;
                                                goto cbshflg2;
                                            case 0x28:	/* SRA */
                                                temp = (acu >> 1) | (acu & 0x80);
                                                cbits = acu & 1;
                                                goto cbshflg2;
                                            case 0x30:	/* SLIA */
                                                temp = (acu << 1) | 1;
                                                cbits = acu & 0x80;
                                                goto cbshflg2;
                                            case 0x38:	/* SRL */
                                                temp = acu >> 1;
                                                cbits = acu & 1;
                                            cbshflg2:
                                                AF = (ushort)((AF & ~0xff) | (temp & 0xa8) |
                                                    (((temp & 0xff) == 0 ? 1 : 0) << 6) |
                                                    parity(temp) | (cbits != 0 ? 1 : 0)
                                                    );
                                                break;
                                        }
                                        Clock.IncTime(23);
                                        break;
                                    case 0x40:		/* BIT */
                                        if ((acu & (1 << ((op >> 3) & 7))) != 0)
                                            AF = (ushort)((AF & ~0xfe) | 0x10 |
                                                (((op & 0x38) == 0x38 ? 1 : 0) << 7)
                                        );
                                        else
                                            AF = (ushort)((AF & ~0xfe) | 0x54);
                                        if ((op & 7) != 6)
                                            AF |= (ushort)(acu & 0x28);
                                        temp = acu;
                                        Clock.IncTime(20);
                                        break;
                                    case 0x80:		/* RES */
                                        temp = acu & ~(1 << ((op >> 3) & 7));
                                        Clock.IncTime(23);
                                        break;
                                    case 0xc0:		/* SET */
                                        temp = acu | (1 << ((op >> 3) & 7));
                                        Clock.IncTime(23);
                                        break;
                                }
                                switch (op & 7)
                                {
                                    case 0: Sethreg(ref BC, temp); break;
                                    case 1: Setlreg(ref BC, temp); break;
                                    case 2: Sethreg(ref DE, temp); break;
                                    case 3: Setlreg(ref DE, temp); break;
                                    case 4: Sethreg(ref HL, temp); break;
                                    case 5: Setlreg(ref HL, temp); break;
                                    case 6: PutBYTE((ushort)adr, (byte)temp); break;
                                    case 7: Sethreg(ref AF, temp); break;
                                }
                                break;
                            case 0xE1:			/* POP IY */
                                POP(ref IY);
                                break;
                            case 0xE3:			/* EX (SP),IY */
                                temp = IY; POP(ref IY); PUSH((ushort)temp);
                                break;
                            case 0xE5:			/* PUSH IY */
                                PUSH((ushort)IY);
                                break;
                            case 0xE9:			/* JP (IY) */
                                PC = IY;
                                break;
                            case 0xF9:			/* LD SP,IY */
                                SP = IY;
                                break;
                            default: PC--;		/* ignore DD */
                                Clock.IncTime(4);
                                break;
                        }
                        break;
                    case 0xFE:			/* CP nn */
                        temp = GetBYTE_pp(ref PC);
                        AF = (ushort)((AF & ~0x28) | (temp & 0x28));
                        acu = hreg(AF);
                        sum = acu - temp;
                        cbits = acu ^ temp ^ sum;
                        AF = (ushort)((AF & ~0xff) | (sum & 0x80) |
                            (((sum & 0xff) == 0 ? 1 : 0) << 6) | (temp & 0x28) |
                            (((cbits >> 6) ^ (cbits >> 5)) & 4) | 2 |
                            (cbits & 0x10) | ((cbits >> 8) & 1)
                            );
                        Clock.IncTime(7);
                        break;
                    case 0xFF:			/* RST 38H */
                        PUSH(PC); PC = 0x38;
                        Clock.IncTime(11);
                        break;
                }
            }
        }
    }
}
