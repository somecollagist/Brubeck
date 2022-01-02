using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck.Architecture;

namespace Brubeck.Assembler
{
    public partial class Assembler
    {
        private static string TranslateStandard(Mnemonic mnn)
        {
            string push = $"{mnn.Adverb}{mnn.Opcode}";
            push += $"{Utils.GetRegisterAlias(int.Parse(mnn.Args[0][1..]))}";
            push += GetOperandByAdverb(mnn, 1);

            return push;
        }

        private static string TranslateRegister(Mnemonic mnn)
        {
            return $"{mnn.Adverb}{mnn.Opcode}{Utils.GetRegisterAlias(int.Parse(mnn.Args[0][1..]))}";
        }

        private static string TranslateJump(Mnemonic mnn)
        {
            return $"{mnn.Adverb}{mnn.Opcode}{mnn.AddressPointer}";
        }

        private static string FixedFlagMnemonicOnly(Mnemonic mnn)
		{
            return $"{mnn.Adverb}{mnn.Opcode}";
		}
        
        private static string GetOperandByAdverb(Mnemonic mnn, int idx)
		{
            return mnn.Adverb switch
            {
                'A' => Utils.GetRegisterAlias(int.Parse(mnn.Args[idx][1..])),
                'E' => $"II{mnn.Args[idx][1..11]}",
                'O' => mnn.Args[idx][..3],
                _ => throw new AssemblySegmentationFault()
            };
        }

        private static readonly Dictionary<string, Func<Mnemonic, string>> Translator = new()
        {
            //Standard format variable-flag instructions
            { "ADD", TranslateStandard },
            { "SUB", TranslateStandard },
            { "MUL", TranslateStandard },
            { "DIV", TranslateStandard },
            { "MOD", TranslateStandard },
            { "MOV", TranslateStandard },
            { "AND", TranslateStandard },
            { "OR", TranslateStandard },
            { "XOR", TranslateStandard },
            { "CMP", TranslateStandard },
            { "SHIFT", TranslateStandard },
            { "ROTATE", TranslateStandard },

            //Non-standard format variable-flag instructions
            {
                "MOVLOC",
                new Func<Mnemonic, string>(mnn =>
                {
                    string push = $"{mnn.Adverb}{mnn.Opcode}";
                    push += $"II{mnn.Args[0][1..11]}";
                    push += GetOperandByAdverb(mnn, 1);

                    return push;
                })
            },
            {
                "VRAMADD",
                new Func<Mnemonic, string>(mnn =>
                {
                    string push = $"{mnn.Adverb}{mnn.Opcode}";
                    if (mnn.Adverb == 'O') push += BIEn.Decode(mnn.Args[0][1]).ToString();
                    else push += GetOperandByAdverb(mnn, 0);
                    return push;
                })
            },
            {
                "VRAMSUB",
                new Func<Mnemonic, string>(mnn =>
                {
                    return $"{mnn.Adverb}{mnn.Opcode}{GetOperandByAdverb(mnn, 0)}";
                })
            },

            //Data Memory pointer instructions
            { "DRUN", FixedFlagMnemonicOnly },
            {
                "DWRITE",
                new Func<Mnemonic, string>(mnn =>
                {
                    return $"{mnn.Adverb}{mnn.Opcode}{Utils.GetRegisterAlias(int.Parse(mnn.Args[0][1..]))}";
                })
            },
            {
                "DREAD",
                new Func<Mnemonic, string>(mnn =>
                {
                    return $"{mnn.Adverb}{mnn.Opcode}{Utils.GetRegisterAlias(int.Parse(mnn.Args[0][1..]))}";
                })
            },
            {
                "DPSET",
                new Func<Mnemonic, string>(mnn =>
                {
                    return $"{mnn.Adverb}{mnn.Opcode}II{mnn.Args[0][1..11]}";
                })
            },
            { "DPINC", FixedFlagMnemonicOnly },
            { "DPDEC", FixedFlagMnemonicOnly },

            //Jump instructions
            { "JEQ", TranslateJump },
            { "JNEQ", TranslateJump },
            { "JMAGEQ", TranslateJump },
            { "JMAGNEQ", TranslateJump },
            { "JLT", TranslateJump },
            { "JGT", TranslateJump },
            { "JLTEQ", TranslateJump },
            { "JGTEQ", TranslateJump },
            { "JZ", TranslateJump },
            { "JNZ", TranslateJump },
            { "JU", TranslateJump },

            //Fixed-flag instructions
            { "NOT", TranslateRegister },
            { "PUSH", TranslateRegister },
            { "PUSHALL", FixedFlagMnemonicOnly },
            { "POP", TranslateRegister },
            { "POPALL", FixedFlagMnemonicOnly },
            { "CLEARFLAGS", FixedFlagMnemonicOnly },
            { "INC", TranslateRegister },
            { "DEC", TranslateRegister },
            { "LABEL", FixedFlagMnemonicOnly },
            { "SUBR", FixedFlagMnemonicOnly },
            { "RETURN", FixedFlagMnemonicOnly },
            { "CALL", TranslateJump }, //Technically a jump instruction but I'm keeping it here because that's where its opcode is
            { "HALT", FixedFlagMnemonicOnly },
            { "INT", TranslateRegister }
        };
    }
}
