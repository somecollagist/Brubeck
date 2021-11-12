﻿using System;
using System.IO;
using System.Linq;

using Brubeck.Architecture;
using Brubeck.Assembler;
using Brubeck.Core;

namespace Brubeck
{
	public class Program
	{
		/// <summary>
		/// Entry point for the emulator, this method is independent of emulated hardware. 
		/// </summary>
		/// <param name="args"></param>
		public static void Main(params string[] args)
		{
			Console.WriteLine(string.Join(" ", args));
			if(args[0] == "-r") //Run mode
			{
				RAM Memory = new();     //Create a memory instance
				CPU ProcUnit = new();   //Create a cpu instance

				if (args.Length > 1)
				{
					Qyte[] state = new Qyte[RAM.RamCeiling];    //Create a null qyte array to flash to (blank slate)
					using (StreamReader sr = new(args[1]))      //Reader to file with path to .brbk5 file (file type is not a filter)
					{
						string rawstate = sr.ReadToEnd();       //Read machine code
						//If the number of Qits (represented as chars) isn't divisible by 3 or exceeds the number of qits that can be stored in RAM, throw a segmentation fault
						if (rawstate.Length % 3 != 0 || rawstate.Length > RAM.RamCeiling) throw new SegmentationFaultException($"RAM Flash State size is {rawstate.Length} Qits ({(float)rawstate.Length / 3} Qytes). Ram ceiling size is {RAM.RamCeiling} Qytes.");
						//Split the machine code into groups of 3 chars and generate qytes accordingly, push these to the state array
						state = Enumerable.Range(0, rawstate.Length / 3)
							.Select(t => new Qyte(rawstate.Substring(t * 3, 3)))
							.ToArray();
					}
					Memory.FlashRAMState(state);    //Flash the memory state array onto RAM
				}

				CPU.ExecutionState es = ProcUnit.Run(ref Memory);   //Start CPU execution with the current RAM state and store the final execution state
				Console.WriteLine($"Program completed with execution state {es}");
			}

			else if(args[0] == "-a")    //Assemble mode
			{
				bool verbose = false;
				if (args.Length > 2) verbose = args[2] == "-v"; //Run in verbose mode
				Assembler.Assembler.Run(args[1], verbose);
			}
		}
	}
}
