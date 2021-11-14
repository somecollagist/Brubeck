using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Brubeck.Assembler
{
	public partial class Assembler
	{
		/// <summary>
		/// Assembles assembly code from the given path.
		/// </summary>
		/// <param name="path">Path to assemble from (should be a .brbkasm file).</param>
		/// <param name="verbose">Show debug information.</param>
		public static void Run(string path, bool verbose = false)
		{
			/* If not verbose, then send the text to a null device.
			 * This means that we can use Console.WriteLine() commands
			 * that will make their content visible if running in
			 * verbose mode and no ouptut is given if not.
			 */
			if (!verbose) Console.SetOut(TextWriter.Null);

			try
			{
				string[] code;                      //Store each of the mnemonics of the source code in an array
				using (StreamReader sr = new(path)) //Reader to assembly source file
				{
					Console.WriteLine($"Full path: {Path.GetFullPath(path)}");
					/* Process for getting code:
					 *  - Read the file
					 *  - Split across newlines (not carraige returns for our linux friends)
					 *  - Remove trailing and leading whitespace
					 *  - Make everything uppercase
					 *  - Remove any items that contain no data (i.e. paragraph lines)
					 *  - Cast to array
					 */
					code = sr
						.ReadToEnd()
						.Split("\n")
						.Select(t => t.Trim().ToUpper())
						.Where(t => t != "")
						.ToArray();
				}

				string newpath = Path.ChangeExtension(path, "brbk5");   //The assembled machine code will be identical to the source, bar its new extension, brbk5
				Console.WriteLine($"Machine code path: {newpath}");

				using StreamWriter sw = new(newpath);                  //Reader to assembled machine code file
				foreach (string cmd in code)
				{
					Console.WriteLine($"Instruction: {cmd}");

					Mnemonic mnn = new(cmd);    //Create a mnemonic object for each command
					string push = "";           //This will store machine code for each command

					//Adverbial opcodes - only run this if the adverb is not null (i.e. has an adverb)
					if (mnn.Adverb != '\0')
					{
						push = $"{mnn.Adverb}{mnn.Opcode}";                                     //Part 1 - Adverb of the command followed by the opcode
						push += Utils.GetRegisterAlias(int.Parse(mnn.Args[0][1..]));            //Part 2 - This should be a register, so decode %x

						push += mnn.Adverb switch                                               //Part 3 - Decide this based off the adverb
						{
							//Register
							'A' => Utils.GetRegisterAlias(int.Parse(mnn.Args[1][1..])),			//Part 3 is a register, so decode %y
							//Memory Location
							'E' => $"{mnn.Args[1][1..11]}II",									//Part 3 is a 10-qit memory location, so get the first 10 qits and add II to the end (4 qytes, easier to parse)
							//Constant
							'O' => mnn.Args[1][..3],											//Part 3 is a constant, so just push the given constant (trimmed to length 3, just in case)
							_ => throw new AssemblySegmentationFault(),
						};
					}

					//Raw opcodes - we don't need to do anything special to them (yet)
					else
					{
						/* It is critical that adverbs be ignored in this context. Since
						 * raw opcodes are given the adverb \0 (null), they will not be
						 * displayed to the user BUT WILL STILL COUNT TOWARDS THE LENGTH
						 * OF THE MACHINE CODE. Therefore, any machine code assembled
						 * with null adverbs included may throw a segmentation fault.
						 */
						push = mnn.Opcode;
					}

					Console.WriteLine($"Decoded to machine code instruction {push}");
					sw.Write(push); //Write the generated machine code to the assembly file
				}
			}
			catch (Exception e)
			{
				switch (e)
				{
					//Assembly source file does not exist
					case FileNotFoundException ex:
						Console.WriteLine($"No file {ex.FileName} could be found.");
						return;

					//Mnemonic is not recognised
					case KeyNotFoundException ex:
						string mnemonic = Regex.Match(ex.Message, @"(?<=\')[A-Z]+(?=\')").Value;
						Console.WriteLine($"Opcode mnemonic {mnemonic} is not recognised.");
						return;
				}
			}

			//This prints regardless of verbosity
			Console.SetOut(Console.Out);
			Console.WriteLine("Assembly Complete!");
		}
	}
}
