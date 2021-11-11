using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brubeck.Assembler
{
    public partial class Assembler
    {
        /// <summary>
        /// Describes a typical assembly instruction.
        /// </summary>
        private class Mnemonic
        {
            /// <summary>
            /// The friendly name of an instruction.
            /// </summary>
            /// <example>IA</example>
            public string Alias { get; private set; }
            /// <summary>
            /// The basic opcode of an instruction.
            /// </summary>
            /// <example>A</example>
            public string Opcode { get; private set; }
            /// <summary>
            /// The leading qit determining what the type of the second arguement is (if applicable).
            /// </summary>
            /// <example>A</example>
            public char Adverb { get; private set; }
            /// <summary>
            /// Arguments of an instruction.
            /// </summary>
            /// <example>%0, IIO</example>
            public string[] Args { get; private set; }

            /// <summary>
            /// Constructor for the Mnemonic class.
            /// </summary>
            /// <param name="cmd">The command containing the assembly instruction.</param>
            public Mnemonic(string cmd)
            {
                Alias = cmd.Substring(0, cmd.IndexOf(' '));
                Console.WriteLine($"\tAlias: {Alias}");
                Opcode = CommandOpcodePairs[Alias];
                Console.WriteLine($"\tOpcode: {Opcode}");

                Args = cmd.Substring(cmd.IndexOf(' '))
                    .Split(',')
                    .Select(t => t.Trim())
                    .ToArray();
                Console.Write("\tArgs: ");
                foreach (string arg in Args) Console.Write($"{arg} ");
                Console.Write("\n");

                if (Opcode.Length == 3) Adverb = '\0';
                else
                {
                    //Valid location marker
                    if (Args[1][0] == '%')
                    {
                        //Register
                        if (Args[1].Length == 2 && new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.Contains(Args[1][1]))
                        {
                            Adverb = 'A';
                        }

                        //Memory Location
                        else if (Args[1].Length == 11 && Utils.IsVowelRestricted(Args[1][1..]))
                        {
                            Adverb = 'E';
                        }
                    }

                    //Constant
                    else if (Args[1].Length == 3 && Utils.IsVowelRestricted(Args[1]))
                    {
                        Adverb = 'O';
                    }

                    else throw new AssemblySegmentationFault();
                }

                Console.WriteLine($"\tAdverb: {Adverb}");
            }

            /// <summary>
            /// Maps assembly mnemonics to opcodes.
            /// </summary>
            private static Dictionary<string, string> CommandOpcodePairs = new()
            {
                { "ADD", "AA" },
                { "SUB", "AE" },
                { "MUL", "AI" },
                { "DIV", "AO" },
                { "MOD", "AU" },

                { "NOT", "EA" },
                { "AND", "EE" },
                { "OR", "EI" },
                { "XOR", "EO" },

                { "MOV", "IA" },
                { "PUSH", "IE" },
                { "PUSHALL", "II" },
                { "POP", "IO" },
                { "POPALL", "IU" },

                { "CMP", "OA" },
                { "LABEL", "OE" },
                { "SUBR", "OI" },

                { "LSHIFT", "OO" },
                { "RSHIFT", "OU" },

                { "INC", "UA" },
                { "DEC", "UE" },
                { "CALL", "UI" },
                { "INT", "UO" },
                { "HALT", "UU" }
            };
        }
    }
}
