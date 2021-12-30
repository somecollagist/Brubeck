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
			Console.WriteLine(Environment.CurrentDirectory);

			try
			{
				string[] code;																				//Store each of the mnemonics of the source code in an array
				using (StreamReader sr = new(Path.Combine(Environment.CurrentDirectory, path)))	//Reader to assembly source file
				{
					Console.WriteLine($"Full path: {Path.GetFullPath(path)}");
					/* Process for getting code:
					 *  - Read the file
					 *  - Split across newlines (not carraige returns for our linux friends)
					 *  - Remove trailing and leading whitespace
					 *  - Make everything uppercase
					 *  - Remove any items that contain no data (i.e. paragraph lines)
					 *  - Remove any comments
					 *  - Cast to array
					 */
					code = sr
						.ReadToEnd()
						.Split("\n")
						.Select(t => t.Trim())
						.Select(t => Utils.UppercaseByRegex(t, new(@"(.(?!')|(?<!').)+")))
						.Where(t => t != "")
						.Where(t => t[0] != ';')
						.ToArray();
				}

				string newpath = Path.ChangeExtension(path, "brbk5");   //The assembled machine code will be identical to the source, bar its new extension, brbk5
				Console.WriteLine($"Machine code path: {newpath}");

				List<Mnemonic> commands = new();
				bool requiresaddressresolve = false;

				foreach(string cmd in code)
				{
					Console.WriteLine($"Instruction: {cmd}");
					
					Mnemonic mnn = new(cmd);
					//Print out information about the mnemonic.
					Console.WriteLine($"\tAlias:   {mnn.Alias}");
					Console.WriteLine($"\tOpcode:  {mnn.Opcode}");
					Console.WriteLine($"\tAdverb:  {mnn.Adverb}");
					try { Console.WriteLine($"\tArgs:    {string.Join("' ", mnn.Args)}"); }
					catch { Console.WriteLine($"\tArgs:    <<NOT APPLICABLE>>"); }
					Console.WriteLine($"\tAddrPtr: {mnn.AddressPointer}");

					if (mnn.AddressPointer == null)
					{
						//If it doesn't point to an instruction, just translate and update offset.
						mnn.MachineCode = Translator[mnn.Alias.ToUpper()](mnn);
						Offset += mnn.MachineCode.Length / 3;
					}
					else
					{
						/* The address pointer will be resolved to 4 Qytes, 
						 * so the total length of the instruction will be 5
						 * Qytes. We need to account for this, so just add
						 * 5 to the offset and it'll be reet.
						 */
						requiresaddressresolve = true;
						Offset += 5;
					}

					commands.Add(mnn);
				}

				if(requiresaddressresolve)
				{
					Console.WriteLine("\nNamed Locations:");
					foreach(KeyValuePair<string, string> kvp in NamedLocations)
					{
						Console.WriteLine($"\t{kvp.Key} --> {kvp.Value[2..]}"); //Trim off the leading II from any addresses
					}

					//If there are named labels to resolve, then resolve them and translate the necessary mnemonics.
					foreach(Mnemonic pointer in commands.Where(t => t.AddressPointer != null))
					{
						pointer.ResolvePointer();
						pointer.MachineCode = Translator[pointer.Alias.ToUpper()](pointer);
					}

					Console.WriteLine("Pointers resolved.");
				}

				using StreamWriter sw = new(newpath);
				Console.WriteLine("Starting write process...");
				foreach(Mnemonic mnn in commands)
				{
					Console.WriteLine($"\t{mnn.SourceCode} decoded to machine code:");
					Console.WriteLine($"\t\t{mnn.MachineCode}");
					sw.Write(mnn.MachineCode); //Write the generated machine code to the assembly file
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

					default:
						throw;
				}
			}

			//This prints regardless of verbosity
			Console.SetOut(Console.Out);
			Console.WriteLine("Assembly Complete!");
		}
	}
}
