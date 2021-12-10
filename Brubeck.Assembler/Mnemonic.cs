using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
				Alias = Regex.Match(cmd, @"[A-Za-z]+").Value;   //Get the first bit of text in a command (this is independent of spaces)
				Console.WriteLine($"\tAlias: {Alias}");
				Opcode = CommandOpcodePairs[Alias];             //Get the opcode with the corresponding mnemonic
				Console.WriteLine($"\tOpcode: {Opcode}");

				Args = null;

				if (Opcode.Length == 3)                         //If the opcode has no adverb (i.e. is a raw opcode) then set the adverb to null.
				{
					Adverb = '\0';
				}
				else                                            //These opcodes aren't raw and have adverbs that must be considered.
				{
					//Since we're operating with adverbs, arguments are guaranteed. Split by commas, cut the whitespace ends, and cast to array.
					Args = cmd[cmd.IndexOf(' ')..]
					.Split(',')
					.Select(t => t.Trim())
					.ToArray();

					//One argument
					if (CommandOpcodePairs["VRAMADD"] == Opcode)
					{
						Adverb = 'O';
						Args = new string[] { Utils.ConvertCharToCode(cmd[cmd.IndexOf('\'')..][1]) };
					}

					//At least 2 arguments

					//Valid location marker
					else if (Args[1][0] == '%')
					{
						//Register marker of style %x
						if (Args[1].Length == 2 && new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.Contains(Args[1][1]))
						{
							Adverb = 'A';
						}

						//Memory Location of style %abcdefghij
						else if (Args[1].Length == 11 && Utils.IsVowelRestricted(Args[1][1..]))
						{
							Adverb = 'E';
						}
					}

					//Constant of style XYZ
					else if (Args[1].Length == 3 && Utils.IsVowelRestricted(Args[1]))
					{
						Adverb = 'O';
					}

					//We've got no idea what they're talking about, so throw a seg. fault.
					else throw new AssemblySegmentationFault();
				}

				Console.Write("\tArgs: ");
				if (Args != null) foreach (string arg in Args) Console.Write($"{arg} ");  //Arrays can't be printed, so loop through to display everything.
				else Console.Write("<<NON-EXISTENT>>");
				Console.Write("\n");
				Console.WriteLine($"\tAdverb: {Adverb}");
			}

			/// <summary>
			/// Maps assembly mnemonics to opcodes.
			/// </summary>
			internal static readonly Dictionary<string, string> CommandOpcodePairs = new()
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

				{ "VRAMADD", "OO" },
				{ "VRAMSUB", "OU" },

				{ "INC", "UA" },
				{ "DEC", "UE" },
				{ "CALL", "UI" },
				{ "INT", "UO" },

				{ "SHIFT", "UU" },

				//Raw opcodes from here on - these don't require adverbial qits.
				{ "HALT", "UII" },

				{ "JEQ", "UAA" },
				{ "JNEQ", "UUU" },
				{ "JGT", "UAE" },
				{ "JLT", "UUO" },
				{ "JGTEQ", "UAI" },
				{ "JLTEQ", "UUI" }
			};
		}
	}
}
