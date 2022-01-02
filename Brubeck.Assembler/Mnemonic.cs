using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Brubeck.Architecture;

namespace Brubeck.Assembler
{
	public partial class Assembler
	{
		internal sealed class Mnemonic
		{
			/// <summary>
			/// The assembly command given.
			/// </summary>
			public string Alias { get; private set; }
			/// <summary>
			/// The basic opcode of the command.
			/// </summary>
			/// <remarks>Excludes the flag Qit</remarks>
			public string Opcode { get; private set; }
			/// <summary>
			/// The flag Qit used by the command.
			/// </summary>
			public char Adverb { get; private set; }
			/// <summary>
			/// Arguments of an instruction.
			/// </summary>
			public string[] Args { get; private set; }
			/// <summary>
			/// Stores an address to point to if the instruction jumps.
			/// </summary>
			public string AddressPointer { get; private set; }

			/// <summary>
			/// Machine code equivalent of the command.
			/// </summary>
			public string MachineCode { get; set; }
			/// <summary>
			/// The original source code of the command.
			/// </summary>
			public string SourceCode { get; private set; }

			/// <summary>
			/// Constructor for the Mnemonic class.
			/// </summary>
			/// <param name="cmd">The command containing the assembly instruction.</param>
			public Mnemonic(string cmd)
			{
				SourceCode = cmd;

				Alias = Regex.Match(cmd, @"[A-Za-z]+").Value;
				Opcode = CommandOpcodePairs[Alias];

				Mnemonic mnn = Parser[Alias.ToUpper()](cmd);
				Args = mnn.Args;
				Adverb = mnn.Adverb;
				AddressPointer = mnn.AddressPointer;
			}

			/// <summary>
			/// Null constructor, used for internal parse methods only.
			/// </summary>
			internal Mnemonic() { }

			/// <summary>
			/// Finds the arguments of a command.
			/// </summary>
			/// <param name="cmd">Command containing arguments.</param>
			/// <returns>An array containing arguments.</returns>
			private static string[] SplitArgs(string cmd)
			{
				return cmd[cmd.IndexOf(' ')..]	//Remove the mnemonic
					.Split(',')					//Split across commas (they separate args)
					.Select(t => t.Trim())		//Remove any trailing whitespace from args
					.ToArray();					//Cast back to array
			}

			/// <summary>
			/// Computes the adverb used by the command.
			/// </summary>
			/// <param name="args">The arguments of the command.</param>
			/// <returns>The adverb/flag used by the command.</returns>
			/// <exception cref="AssemblySegmentationFault">Thrown if an adverb cannot be assigned.</exception>
			private static char GetAdverbByArgs(string[] args)
			{
				char Adverb = '\0'; //Use a null character as a checker.

				if (args[1][0] == '%')
				{
					//The second argument is a register or a memory location

					//It's a register
					if (Regex.IsMatch(args[1], @"^%[0-9]$")) Adverb = 'A';
					//It's a memory location
					else if (Regex.IsMatch(args[1], @"^%[AEIOU]{10}$")) Adverb = 'E';
				}
				//It's a constant
				else if (Regex.IsMatch(args[1], @"^[AEIOU]{3}$")) Adverb = 'O';
				
				//Either an adverb cannot be assigned, or return the adverb we found.
				if (Adverb == '\0') throw new AssemblySegmentationFault();
				return Adverb;
			}

			/// <summary>
			/// Parses a standard format variable-flag instruction.
			/// </summary>
			/// <param name="cmd">The command to parse.</param>
			/// <returns>Some data about the command.</returns>
			private static Mnemonic ParseStandard(string cmd)
			{
				string[] args = SplitArgs(cmd);
				char adverb = GetAdverbByArgs(args);

				return new Mnemonic()
				{
					Args = args,
					Adverb = adverb,
					AddressPointer = null
				};
			}

			/// <summary>
			/// Parses an instruction targeting only a register.
			/// </summary>
			/// <param name="cmd">The command to parse.</param>
			/// <returns>Some data about the command.</returns>
			private static Mnemonic ParseRegister(string cmd)
			{
				string[] args = SplitArgs(cmd);

				return new Mnemonic()
				{
					Args = args,
					Adverb = 'U',
					AddressPointer = null
				};
			}

			/// <summary>
			/// Parses a jump instruction.
			/// </summary>
			/// <param name="cmd">The command to parse.</param>
			/// <returns>Some data about the command.</returns>
			private static Mnemonic ParseJump(string cmd)
			{
				string[] args = SplitArgs(cmd);

				return new Mnemonic()
				{
					Args = args,
					Adverb = 'U',
					AddressPointer = args[0]
				};
			}

			/// <summary>
			/// Parses a fixed-flag instruction.
			/// </summary>
			/// <param name="cmd">The command to parse.</param>
			/// <returns>Some data about the command.</returns>
			private static Mnemonic ParseFixedFlag(string cmd)
			{
				return new Mnemonic()
				{
					Args = null,
					Adverb = 'U',
					AddressPointer = null
				};
			}

			/// <summary>
			/// Converts a named label stored in address pointer to the memory location it should point to.
			/// </summary>
			/// <exception cref="AssemblySegmentationFault">Thrown if the named label does not exist.</exception>
			public void ResolvePointer()
			{
				if (!NamedLocations.TryGetValue(AddressPointer, out string ptr)) throw new AssemblySegmentationFault($"No named label {AddressPointer} exists.");
				AddressPointer = ptr;
			}

			/// <summary>
			/// Maps mnemonic aliases to functions that parse them.
			/// </summary>
			private readonly Dictionary<string, Func<string, Mnemonic>> Parser = new()
			{
				//Standard format variable-flag instructions
				{ "ADD", ParseStandard },
				{ "SUB", ParseStandard },
				{ "MUL", ParseStandard },
				{ "DIV", ParseStandard },
				{ "MOD", ParseStandard },
				{ "MOVLOC", ParseStandard },
				{ "MOV", ParseStandard },
				{ "AND", ParseStandard },
				{ "OR", ParseStandard },
				{ "XOR", ParseStandard },
				{ "CMP", ParseStandard },
				{
					"VRAMADD",
					new Func<string, Mnemonic>(cmd =>
					{
						//This instruction is really awkward, so we have a custom implementation of the adverb checker.

						string[] args = SplitArgs(cmd);
						char adverb = '\0'; //Use a null character as a checker.

						if (args[0][0] == '%')
						{
							//The second argument is a register or a memory location

							//It's a register
							if (Regex.IsMatch(args[0], @"^%[0-9]$"))
							{
								adverb = 'A';
							}
							//It's a memory location
							else if (Regex.IsMatch(args[0], @"^%[AEIOU]{10}$"))
							{
								adverb = 'E';
							}
						}
						//It's a constant
						else if (Regex.IsMatch(args[0], @"^'.'$"))
						{
							//ConvertToCharCode has its own method of validating characters, so don't bother making this regex check for BIEn encoded characters, it's not worth doing
							adverb = 'O';
						}

						//Either an adverb cannot be assigned, or return the adverb we found.
						if (adverb == '\0') throw new AssemblySegmentationFault();

						return new Mnemonic()
						{
							Args = args,
							Adverb = adverb,
							AddressPointer = null
						};
					})
				},
				{
					"VRAMSUB",
					new Func<string, Mnemonic>(cmd =>
                    {
						string[] args = SplitArgs(cmd);
						char adverb = '\0'; //Use a null character as a checker.

						if (args[0][0] == '%')
						{
							//The second argument is a register or a memory location

							//It's a register
							if (Regex.IsMatch(args[0], @"^%[0-9]$"))
							{
								adverb = 'A';
							}
							//It's a memory location
							else if (Regex.IsMatch(args[0], @"^%[AEIOU]{10}$"))
							{
								adverb = 'E';
							}
						}
						//It's a constant
						else if (Regex.IsMatch(args[0], @"^[AEIOU]{3}$"))
						{
							adverb = 'O';
						}

						//Either an adverb cannot be assigned, or return the adverb we found.
						if (adverb == '\0') throw new AssemblySegmentationFault();

						return new Mnemonic()
						{
							Args = args,
							Adverb = adverb,
							AddressPointer = null
						};
					})
				},
				{ "SHIFT", ParseStandard },
				{ "ROTATE", ParseStandard },

                {
					"DRUN",
					new Func<string, Mnemonic>(cmd =>
                    {
						Mnemonic mnn = ParseFixedFlag(cmd);
						mnn.Adverb = 'E';
						return mnn;
                    })
                },

				{
					"DWRITE",
					new Func<string, Mnemonic>(cmd =>
					{
						return new Mnemonic()
						{
							Args = SplitArgs(cmd),
							Adverb = 'O',
							AddressPointer = null
						};
					})
                },
                {
					"DREAD",
					new Func<string, Mnemonic>(cmd =>
					{
						return new Mnemonic()
						{
							Args = SplitArgs(cmd),
							Adverb = 'O',
							AddressPointer = null
						};
					})
				},
                {
					"DPSET",
					new Func<string, Mnemonic>(cmd =>
					{
						return new Mnemonic()
						{
							Args = SplitArgs(cmd),
							Adverb = 'O',
							AddressPointer = null
						};
					})
				},
                {
					"DPINC",
					new Func<string, Mnemonic>(cmd =>
					{
						return new Mnemonic()
						{
							Args = null,
							Adverb = 'O',
							AddressPointer = null
						};
					})
				},
				{
					"DPDEC",
					new Func<string, Mnemonic>(cmd =>
					{
						return new Mnemonic()
						{
							Args = null,
							Adverb = 'O',
							AddressPointer = null
						};
					})
				},

				{ "JEQ", ParseJump },
				{ "JNEQ", ParseJump },
				{ "JMAGEQ", ParseJump },
				{ "JMAGNEQ", ParseJump },
				{ "JLT", ParseJump },
				{ "JGT", ParseJump },
				{ "JLTEQ", ParseJump },
				{ "JGTEQ", ParseJump },
				{ "JZ", ParseJump },
				{ "JNZ", ParseJump },
				{ "JU", ParseJump },

				{ "NOT", ParseRegister },
				{ "PUSH", ParseRegister },
				{ "PUSHALL", ParseFixedFlag },
				{ "POP", ParseRegister },
				{ "POPALL", ParseFixedFlag },
				{ "CLEARFLAGS", ParseFixedFlag },
				{ "INC", ParseRegister },
				{ "DEC", ParseRegister },
				{
					"LABEL",
					new Func<string, Mnemonic>(cmd =>
					{
						//Needs to bind a new location

						string[] args = SplitArgs(cmd);
						NamedLocations.Add(args[0], Utils.GetAddress(Offset));
						return ParseFixedFlag(cmd);
					})
				},
				{
					"SUBR",
					new Func<string, Mnemonic>(cmd =>
					{
						//Needs to bind a new location

						string[] args = SplitArgs(cmd);
						NamedLocations.Add(args[0], Utils.GetAddress(Offset));
						return ParseFixedFlag(cmd);
					})
				},
				{ "RETURN", ParseFixedFlag },
				{ "CALL", ParseJump },
				{ "HALT", ParseFixedFlag },
				{ "INT", ParseRegister }
			};

			/// <summary>
			/// Maps mnemonic aliases to opcodes, not including adverbs.
			/// </summary>
			private static readonly Dictionary<string, string> CommandOpcodePairs = new()
			{
				//Variable-flag instructions
				{ "ADD", "AA" },
				{ "SUB", "AE" },
				{ "MUL", "AI" },
				{ "DIV", "AO" },
				{ "MOD", "AU" },
				{ "MOVLOC", "EA" },
				{ "MOV", "EE" },
				{ "AND", "EI" },
				{ "OR", "EO" },
				{ "XOR", "EU" },
				{ "CMP", "IA" },
				{ "VRAMADD", "IE" },
				{ "VRAMSUB", "II" },
				{ "SHIFT", "IO" },
				{ "ROTATE", "IU" },

				{ "DRUN", "UU" },
				{ "DWRITE", "UA" },
				{ "DREAD", "UE" },
				{ "DPSET", "UI" },
				{ "DPINC", "UO" },
				{ "DPDEC", "UU" },

				//Fixed-flag instructions
				{ "JEQ", "AA" },
				{ "JNEQ", "AE" },
				{ "JMAGEQ", "AI" },
				{ "JMAGNEQ", "AO" },
				{ "JLT", "AU" },
				{ "JGT", "EA" },
				{ "JLTEQ", "EE" },
				{ "JGTEQ", "EI" },
				{ "JZ", "EO" },
				{ "JNZ", "EU" },
				{ "JU", "IA" },
				{ "NOT", "IE" },
				{ "PUSH", "II" },
				{ "PUSHALL", "IO" },
				{ "POP", "IU" },
				{ "POPALL", "OA" },
				{ "CLEARFLAGS", "OE" },
				{ "INC", "OI" },
				{ "DEC", "OO" },
				{ "LABEL", "OU" },
				{ "SUBR", "UA" },
				{ "RETURN", "UE" },
				{ "CALL", "UI" },
				{ "HALT", "UO" },
				{ "INT", "UU" }
			};
		}

		#pragma warning disable IDE0044
		/// <summary>
		/// Maps named locations to memory addresses.
		/// </summary>
		private static Dictionary<string, string> NamedLocations = new();

		/// <summary>
		/// Amount of Qytes currently generated by the assembler.
		/// </summary>
		private static int Offset = 0;
		#pragma warning restore IDE0044
	}
}
