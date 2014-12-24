using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiedye.Z80Core
{
    public static class Z80Disassembler
    {
        // This is based on http://www.z80.info/decoding.htm
        public struct DisassembledInstruction
        {
            public string Disassembly;
            public int Length;
        }
        public static DisassembledInstruction DisassembleInstruction(byte[] instr)
        {
            return DisassembleInstruction(instr, 0, false);
        }
        
        public static DisassembledInstruction DisassembleInstruction(byte[] instr, ushort baseAddress)
        {
            return DisassembleInstruction(instr, baseAddress, true);
        }

        public static DisassembledInstruction DisassembleInstruction(byte[] instr, ushort baseAddress, bool hasBaseAddress)
        {
            unchecked {
                DisassembledInstruction disasm = default(DisassembledInstruction);
                disasm.Length = 1;
                disasm.Disassembly = "ERROR";
                int b = instr[0];
                // Do you like switches? 'Cause that's kind of kinky.
                switch (GetField(Field.x, b))
                {
                    case 0:
                        switch (GetField(Field.z, b))
                        {
                            case 0:
                                switch (GetField(Field.y, b))
                                {
                                    case 0:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "NOP";
                                        break;
                                    case 1:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "EX AF, AF'";
                                        break;
                                    case 2:
                                        disasm.Length = 2;
                                        if (hasBaseAddress)
                                            disasm.Disassembly = "DJNZ " + ((sbyte)instr[1] + (baseAddress + 2)).ToString("X4");
                                        else
                                            disasm.Disassembly = "DJNZ " + ((sbyte)instr[1]).ToString("X2");
                                        break;
                                    case 3:
                                        disasm.Length = 2;
                                        if (hasBaseAddress)
                                            disasm.Disassembly = "JR " + ((sbyte)instr[1] + (baseAddress + 2)).ToString("X4");
                                        else
                                            disasm.Disassembly = "JR " + ((sbyte)instr[1]).ToString("X2");
                                        break;
                                    case 4:
                                    case 5:
                                    case 6:
                                    case 7:
                                        disasm.Length = 2;
                                        if (hasBaseAddress)
                                            disasm.Disassembly = "JR " + TableCC[GetField(Field.qq, b)] + ", " + ((sbyte)instr[1] + (baseAddress + 2)).ToString("X4");
                                        else
                                            disasm.Disassembly = "JR " + TableCC[GetField(Field.qq, b)] + ", " + ((sbyte)instr[1]).ToString("X2");
                                        break;
                                }
                                break;
                            case 1:
                                switch (GetField(Field.q, b))
                                {
                                    case 0:
                                        disasm.Length = 3;
                                        disasm.Disassembly = "LD " + TableRP[GetField(Field.p, b)] + ", " + (instr[1] | (instr[2] << 8)).ToString("X4");
                                        break;
                                    case 1:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "ADD HL, " + TableRP[GetField(Field.p, b)];
                                        break;
                                }
                                break;
                            case 2:
                                switch (GetField(Field.y, b))
                                {
                                    case 0:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "LD (BC), A";
                                        break;
                                    case 1:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "LD A, (BC)";
                                        break;
                                    case 2:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "LD (DE), A";
                                        break;
                                    case 3:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "LD A, (DE)";
                                        break;
                                    case 4:
                                        disasm.Length = 3;
                                        disasm.Disassembly = "LD (" + (instr[1] | (instr[2] << 8)).ToString("X4") + "), HL";
                                        break;
                                    case 5:
                                        disasm.Length = 3;
                                        disasm.Disassembly = "LD HL, (" + (instr[1] | (instr[2] << 8)).ToString("X4") + ")";
                                        break;
                                    case 6:
                                        disasm.Length = 3;
                                        disasm.Disassembly = "LD (" + (instr[1] | (instr[2] << 8)).ToString("X4") + "), A";
                                        break;
                                    case 7:
                                        disasm.Length = 3;
                                        disasm.Disassembly = "LD A, (" + (instr[1] | (instr[2] << 8)).ToString("X4") + ")";
                                        break;
                                }
                                break;
                            case 3:
                                disasm.Length = 1;
                                switch (GetField(Field.q, b))
                                {
                                    case 0:
                                        disasm.Disassembly = "INC " + TableRP[GetField(Field.p, b)];
                                        break;
                                    case 1:
                                        disasm.Disassembly = "DEC " + TableRP[GetField(Field.p, b)];
                                        break;
                                }
                                break;
                            case 4:
                                disasm.Length = 1;
                                disasm.Disassembly = "INC " + TableR[GetField(Field.y, b)];
                                break;
                            case 5:
                                disasm.Length = 1;
                                disasm.Disassembly = "DEC " + TableR[GetField(Field.y, b)];
                                break;
                            case 6:
                                disasm.Length = 2;
                                disasm.Disassembly = "LD " + TableR[GetField(Field.y, b)] + ", " + instr[1].ToString("X2");
                                break;
                            case 7:
                                disasm.Length = 1;
                                switch (GetField(Field.y, b))
                                {
                                    case 0:
                                        disasm.Disassembly = "RLCA";
                                        break;
                                    case 1:
                                        disasm.Disassembly = "RRCA";
                                        break;
                                    case 2:
                                        disasm.Disassembly = "RLA";
                                        break;
                                    case 3:
                                        disasm.Disassembly = "RRA";
                                        break;
                                    case 4:
                                        disasm.Disassembly = "DAA";
                                        break;
                                    case 5:
                                        disasm.Disassembly = "CPL";
                                        break;
                                    case 6:
                                        disasm.Disassembly = "SCF";
                                        break;
                                    case 7:
                                        disasm.Disassembly = "CCF";
                                        break;
                                }
                                break;
                        }
                        break;
                    case 1:
                        disasm.Length = 1;
                        if (b == 0x76)
                            disasm.Disassembly = "HALT";
                        else
                            disasm.Disassembly = "LD " + TableR[GetField(Field.y, b)] + ", " + TableR[GetField(Field.z, b)];
                        break;
                    case 2:
                        disasm.Length = 1;
                        disasm.Disassembly = TableAlu[GetField(Field.y, b)] + TableR[GetField(Field.z, b)];
                        break;
                    case 3:
                        switch (GetField(Field.z, b))
                        {
                            case 0:
                                disasm.Length = 1;
                                disasm.Disassembly = "RET " + TableCC[GetField(Field.y, b)];
                                break;
                            case 1:
                                disasm.Length = 1;
                                switch (GetField(Field.q, b))
                                {
                                    case 0:
                                        disasm.Disassembly = "POP " + TableRP2[GetField(Field.p, b)];
                                        break;
                                    case 1:
                                        switch (GetField(Field.p, b))
                                        {
                                            case 0:
                                                disasm.Disassembly = "RET";
                                                break;
                                            case 1:
                                                disasm.Disassembly = "EXX";
                                                break;
                                            case 2:
                                                disasm.Disassembly = "JP (HL)";
                                                break;
                                            case 3:
                                                disasm.Disassembly = "LD SP, HL";
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case 2:
                                disasm.Length = 3;
                                disasm.Disassembly = "JP " + TableCC[GetField(Field.y, b)] + ", " + (instr[1] | (instr[2] << 8)).ToString("X4");
                                break;
                            case 3:
                                switch (GetField(Field.y, b))
                                {
                                    case 0:
                                        disasm.Length = 3;
                                        disasm.Disassembly = "JP " + (instr[1] | (instr[2] << 8)).ToString("X4");
                                        break;
                                    case 1: // CB Prefix
                                        DoCBPrefix(ref instr, ref disasm);
                                        break;
                                    case 2:
                                        disasm.Length = 2;
                                        disasm.Disassembly = "OUT (" + instr[1].ToString("X2") + "), A";
                                        break;
                                    case 3:
                                        disasm.Length = 2;
                                        disasm.Disassembly = "IN A, (" + instr[1].ToString("X2") + ")";
                                        break;
                                    case 4:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "EX (SP), HL";
                                        break;
                                    case 5:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "EX DE, HL";
                                        break;
                                    case 6:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "DI";
                                        break;
                                    case 7:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "EI";
                                        break;
                                }
                                break;
                            case 4:
                                disasm.Length = 3;
                                disasm.Disassembly = "CALL " + TableCC[GetField(Field.y, b)] + ", " + (instr[1] | (instr[2] << 8)).ToString("X4");
                                break;
                            case 5:
                                switch (GetField(Field.q, b))
                                {
                                    case 0:
                                        disasm.Length = 1;
                                        disasm.Disassembly = "PUSH " + TableRP2[GetField(Field.p, b)];
                                        break;
                                    case 1:
                                        switch (b)
                                        {
                                            case 0xCD:
                                                disasm.Length = 3;
                                                disasm.Disassembly = "CALL " + (instr[1] | (instr[2] << 8)).ToString("X4");
                                                break;
                                            case 0xDD:
                                                DoIndexPrefix(ref instr, ref disasm, "IX");
                                                break;
                                            case 0xED:
                                                DoEDPrefix(ref instr, ref disasm);
                                                break;
                                            case 0xFD:
                                                DoIndexPrefix(ref instr, ref disasm, "IY");
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case 6:
                                disasm.Length = 2;
                                disasm.Disassembly = TableAlu[GetField(Field.y, b)] + instr[1].ToString("X2");
                                break;
                            case 7:
                                disasm.Length = 1;
                                disasm.Disassembly = "RST " + (GetField(Field.y, b) * 8).ToString("X2") + "h";
                                break;
                        }
                        break;
                }
                return disasm;
            }
        }

        private static void DoCBPrefix(ref byte[] instr, ref DisassembledInstruction disasm)
        {
            byte b = instr[1];
            disasm.Length = 2;
            switch (GetField(Field.x, b))
            {
                case 0:
                    disasm.Disassembly = TableRot[GetField(Field.y, b)] + TableR[GetField(Field.z, b)];
                    break;
                case 1:
                    disasm.Disassembly = "BIT " + GetField(Field.y, b).ToString() + ", " + TableR[GetField(Field.z, b)];
                    break;
                case 2:
                    disasm.Disassembly = "RES " + GetField(Field.y, b).ToString() + ", " + TableR[GetField(Field.z, b)];
                    break;
                case 3:
                    disasm.Disassembly = "SET " + GetField(Field.y, b).ToString() + ", " + TableR[GetField(Field.z, b)];
                    break;
            }
        }


        private static void DoEDPrefix(ref byte[] instr, ref DisassembledInstruction disasm)
        {
            byte b = instr[1];
            switch (GetField(Field.x, b))
            {
                case 0:
                case 3:
                    disasm.Length = 2;
                    disasm.Disassembly = "NONI \\ NOP";
                    break;
                case 1:
                    disasm.Length = 2;
                    switch (GetField(Field.z, b))
                    {
                        case 0:
                            if (b != 0x70)
                                disasm.Disassembly = "IN " + TableR[GetField(Field.y, b)] + ", (C)";
                            else
                                disasm.Disassembly = "IN (C)";
                            break;
                        case 1:
                            if (b != 0x70)
                                disasm.Disassembly = "OUT (C), " + TableR[GetField(Field.y, b)];
                            else
                                disasm.Disassembly = "OUT (C), 0";
                            break;
                        case 2:
                            disasm.Length = 2;
                            if (GetField(Field.q, b) == 0)
                                disasm.Disassembly = "SBC HL, " + TableRP[GetField(Field.p, b)];
                            else
                                disasm.Disassembly = "ADC HL, " + TableRP[GetField(Field.p, b)];
                            break;
                        case 3:
                            disasm.Length = 4;
                            if (GetField(Field.q, b) == 0)
                                disasm.Disassembly = "LD (" + (instr[2] | (instr[3] << 8)).ToString("X4") + "), " + TableRP[GetField(Field.p, b)];
                            else
                                disasm.Disassembly = "LD " + TableRP[GetField(Field.p, b)] + ", (" + (instr[2] | (instr[3] << 8)).ToString("X4") + ")";
                            break;
                        case 4:
                            disasm.Length = 2;
                            disasm.Disassembly = "NEG";
                            break;
                        case 5:
                            disasm.Length = 2;
                            if (GetField(Field.y, b) != 1)
                                disasm.Disassembly = "RETN";
                            else
                                disasm.Disassembly = "RETI";
                            break;
                        case 6:
                            disasm.Length = 2;
                            disasm.Disassembly = "IM " + TableIM[GetField(Field.y, b)];
                            break;
                        case 7:
                            disasm.Length = 2;
                            switch (GetField(Field.y, b))
                            {
                                case 0:
                                    disasm.Disassembly = "LD I, A";
                                    break;
                                case 1:
                                    disasm.Disassembly = "LD R, A";
                                    break;
                                case 2:
                                    disasm.Disassembly = "LD A, I";
                                    break;
                                case 3:
                                    disasm.Disassembly = "LD A, R";
                                    break;
                                case 4:
                                    disasm.Disassembly = "RRD";
                                    break;
                                case 5:
                                    disasm.Disassembly = "RLD";
                                    break;
                                case 6:
                                case 7:
                                    disasm.Disassembly = "NOP";
                                    break;
                            }
                            break;
                    }
                    break;
                case 2:
                    disasm.Length = 2;
                    switch (b)
                    {
                        case 0xA0:
                            disasm.Disassembly = "LDI";
                            break;
                        case 0xA1:
                            disasm.Disassembly = "CPI";
                            break;
                        case 0xA2:
                            disasm.Disassembly = "INI";
                            break;
                        case 0xA3:
                            disasm.Disassembly = "OUTI";
                            break;
                        case 0xA8:
                            disasm.Disassembly = "LDD";
                            break;
                        case 0xA9:
                            disasm.Disassembly = "CPD";
                            break;
                        case 0xAA:
                            disasm.Disassembly = "IND";
                            break;
                        case 0xAB:
                            disasm.Disassembly = "OUTD";
                            break;
                        case 0xB0:
                            disasm.Disassembly = "LDIR";
                            break;
                        case 0xB1:
                            disasm.Disassembly = "CPIR";
                            break;
                        case 0xB2:
                            disasm.Disassembly = "INIR";
                            break;
                        case 0xB3:
                            disasm.Disassembly = "OUIR";
                            break;
                        case 0xB8:
                            disasm.Disassembly = "LDDR";
                            break;
                        case 0xB9:
                            disasm.Disassembly = "CPDR";
                            break;
                        case 0xBA:
                            disasm.Disassembly = "INDR";
                            break;
                        case 0xBC:
                            disasm.Disassembly = "OTDR";
                            break;
                        default:
                            disasm.Disassembly = "NONI \\ NOP";
                            break;
                    }
                    break;
            }
        }
        private static void DoIndexPrefix(ref byte[] instr, ref DisassembledInstruction disasm, string indexRegister)
        {
            unchecked
            {
                byte b = instr[1];
                switch (b)
                {
                    case 0x21:
                        disasm.Length = 4;
                        disasm.Disassembly = "LD " + indexRegister + ", " + (instr[2] | (instr[3] << 8)).ToString("X4");
                        break;
                    case 0x22:
                        disasm.Length = 4;
                        disasm.Disassembly = "LD (" + (instr[2] | (instr[3] << 8)).ToString("X4") + "), " + indexRegister;
                        break;
                    case 0x2A:
                        disasm.Length = 4;
                        disasm.Disassembly = "LD " + indexRegister + ", (" + (instr[2] | (instr[3] << 8)).ToString("X4") + ")";
                        break;
                    case 0x23:
                        disasm.Length = 2;
                        disasm.Disassembly = "INC " + indexRegister;
                        break;
                    case 0x2B:
                        disasm.Length = 2;
                        disasm.Disassembly = "DEC " + indexRegister;
                        break;
                    case 0x24:
                        disasm.Length = 2;
                        disasm.Disassembly = "INC " + indexRegister + "H";
                        break;
                    case 0x2C:
                        disasm.Length = 2;
                        disasm.Disassembly = "INC " + indexRegister + "L";
                        break;
                    case 0x34:
                        disasm.Length = 3;
                        disasm.Disassembly = "INC (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                        break;
                    case 0x25:
                        disasm.Length = 2;
                        disasm.Disassembly = "DEC " + indexRegister + "H";
                        break;
                    case 0x2D:
                        disasm.Length = 2;
                        disasm.Disassembly = "DEC " + indexRegister + "L";
                        break;
                    case 0x35:
                        disasm.Length = 3;
                        disasm.Disassembly = "DEC (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                        break;
                    case 0x26:
                        disasm.Length = 3;
                        disasm.Disassembly = "LD " + indexRegister + "H, " + ((sbyte)instr[2]).ToString("X2");
                        break;
                    case 0x2E:
                        disasm.Length = 3;
                        disasm.Disassembly = "LD " + indexRegister + "L, " + ((sbyte)instr[2]).ToString("X2");
                        break;
                    case 0x36:
                        disasm.Length = 4;
                        disasm.Disassembly = "LD (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + "), " + instr[3].ToString("X2");
                        break;
                    case 0x09:
                        disasm.Length = 2;
                        disasm.Disassembly = "ADD " + indexRegister + ", BC";
                        break;
                    case 0x19:
                        disasm.Length = 2;
                        disasm.Disassembly = "ADD " + indexRegister + ", DE";
                        break;
                    case 0x29:
                        disasm.Length = 2;
                        disasm.Disassembly = "ADD " + indexRegister + ", " + indexRegister;
                        break;
                    case 0x39:
                        disasm.Length = 2;
                        disasm.Disassembly = "ADD " + indexRegister + ", SP";
                        break;
                    case 0x64:
                    case 0x65:
                    case 0x6C:
                    case 0x6D:
                        disasm.Length = 2;
                        disasm.Disassembly = "LD " + indexRegister + TableR[GetField(Field.y, b)] + ", " + indexRegister + TableR[GetField(Field.z, b)];
                        break;
                    case 0x70:
                    case 0x71:
                    case 0x72:
                    case 0x73:
                    case 0x74:
                    case 0x75:
                    case 0x77:
                        disasm.Length = 3;
                        disasm.Disassembly = "LD (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + "), " + TableR[GetField(Field.z, b)];
                        break;
                    case 0x46:
                    case 0x4E:
                    case 0x56:
                    case 0x5E:
                    case 0x66:
                    case 0x6E:
                    case 0x7E:
                        disasm.Length = 3;
                        disasm.Disassembly = "LD " + TableR[GetField(Field.y, b)] + ", (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                        break;
                    case 0x84:
                    case 0x85:
                    case 0x8C:
                    case 0x8D:
                    case 0x94:
                    case 0x95:
                    case 0x9C:
                    case 0x9D:
                    case 0xA4:
                    case 0xA5:
                    case 0xAC:
                    case 0xAD:
                    case 0xB4:
                    case 0xB5:
                    case 0xBC:
                    case 0xBD:
                        disasm.Length = 2;
                        disasm.Disassembly = TableAlu[GetField(Field.y, b)] + indexRegister + TableR[GetField(Field.z, b)];
                        break;
                    case 0x86:
                    case 0x8E:
                    case 0x96:
                    case 0x9E:
                    case 0xA6:
                    case 0xAE:
                    case 0xB6:
                    case 0xBE:
                        disasm.Length = 3;
                        disasm.Disassembly = TableAlu[GetField(Field.y, b)] + "(" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                        break;
                    case 0xE1:
                        disasm.Length = 2;
                        disasm.Disassembly = "POP " + indexRegister;
                        break;
                    case 0xE9:
                        disasm.Length = 2;
                        disasm.Disassembly = "JP (" + indexRegister + ")";
                        break;
                    case 0xE3:
                        disasm.Length = 2;
                        disasm.Disassembly = "EX (SP), " + indexRegister;
                        break;
                    case 0xE5:
                        disasm.Length = 2;
                        disasm.Disassembly = "PUSH " + indexRegister;
                        break;
                    case 0xCB:
                        b = instr[3];
                        disasm.Length = 4;
                        switch (GetField(Field.x, b))
                        {
                            case 0:
                                if (GetField(Field.z, b) != 6)
                                    disasm.Disassembly = TableRot[GetField(Field.y, b)] + TableR[GetField(Field.z, b)] + ", (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                                else
                                    disasm.Disassembly = TableRot[GetField(Field.y, b)] + "(" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                                break;
                            case 1:
                                disasm.Disassembly = "BIT " + GetField(Field.y, b).ToString() + ", (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                                break;
                            case 2:
                                if (GetField(Field.z, b) != 6)
                                    disasm.Disassembly = "RES " + TableR[GetField(Field.z, b)] + ", " + GetField(Field.y, b).ToString() + ", (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                                else
                                    disasm.Disassembly = "RES " + GetField(Field.y, b).ToString() + ", (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                                break;
                            case 3:
                                if (GetField(Field.z, b) != 6)
                                    disasm.Disassembly = "SET " + TableR[GetField(Field.z, b)] + ", " + GetField(Field.y, b).ToString() + ", (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                                else
                                    disasm.Disassembly = "SET " + GetField(Field.y, b).ToString() + ", (" + indexRegister + " + " + ((sbyte)instr[2]).ToString("X2") + ")";
                                break;
                        }
                        break;
                    case 0xF9:
                        disasm.Length = 2;
                        disasm.Disassembly = "LD SP, " + indexRegister;
                        break;
                    // No case ED; index registers forbidden on ED block.
                    // No case DD or FD: Only last prefix matters.
                    default:
                        disasm.Length = 1;
                        disasm.Disassembly = "NONI";
                        break;
                }
            }
        }



        private static readonly string[] TableR = new string[]
        {
            "B",
            "C",
            "D",
            "E",
            "H",
            "L",
            "(HL)",
            "A",
        };

        private static readonly string[] TableRP = new string[]
        {
            "BC",
            "DE",
            "HL",
            "SP",
        };

        private static readonly string[] TableRP2 = new string[]
        {
            "BC",
            "DE",
            "HL",
            "AF",
        };

        private static readonly string[] TableCC = new string[]
        {
            "NZ",
            "Z",
            "NC",
            "C",
            "PO",
            "PE",
            "P",
            "M",
        };

        private static readonly string[] TableAlu = new string[]
        {
            "ADD A, ",
            "ADC A, ",
            "SUB ",
            "SBC A, ",
            "AND ",
            "XOR ",
            "OR ",
            "CP ",
        };

        private static readonly string[] TableRot = new string[]
        {
            "RLC ",
            "RRC ",
            "RL ",
            "RR ",
            "SLA ",
            "SRA ",
            "SLL ",
            "SRL ",
        };

        private static readonly string[] TableIM = new string[]
        {
            "0",
            "?",
            "1",
            "2",
            "0",
            "?",
            "1",
            "2",
        };

        

        private enum Field
        {
            x,
            y,
            z,
            p,
            q,
            pp,
            qq,
        };

        private static int GetField(Field f, int b)
        {
            switch (f)
            {
                case Field.x:
                    return b >> 6;
                case Field.y:
                    return (b >> 3) & 7;
                case Field.z:
                    return b & 7;
                case Field.p:
                    return (b >> 4) & 3;
                case Field.q:
                    return (b >> 3) & 1;
                case Field.pp:
                    return (b >> 5) & 1;
                case Field.qq:
                    return (b >> 3) & 3;
            }
            throw new ArgumentOutOfRangeException();
        }


    }
}
