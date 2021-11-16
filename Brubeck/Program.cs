using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
			if (args.Length == 0 || new string[] { "-h, --help" }.Contains(args[0]))
			{
				using StreamReader sr = new(Assembly.GetExecutingAssembly().GetManifestResourceStream("Brubeck.helpme.txt"));
				Console.WriteLine(sr.ReadToEnd());
			}

			else if(new string[] {"-r", "--run"}.Contains(args[0])) //Run mode
			{
				RAM InstMem = new();    //Create a memory instance for instructions
				RAM DataMem = new();	//Create a memory instance for data
				CPU ProcUnit = new();   //Create a cpu instance

				if (args.Length > 1)
				{
					InstMem.FlashRAMState(ReadQuinaryFromFile(args[1]));        //Flash instructions

					if (args.Length > 2)
					{
						DataMem.FlashRAMState(ReadQuinaryFromFile(args[2]));    //Flash data

						if(args.Length > 3)
						{
							ProcUnit.FlashCPUState(ReadCPUState(args[3]));		//Flash CPU state
						}
					}
				}
				else
				{
					InstMem.FlashRAMState(ReadQuinaryFromFile("instmemlast.brbk5"));
					DataMem.FlashRAMState(ReadQuinaryFromFile("datamemlast.brbk5"));
					ProcUnit.FlashCPUState(ReadCPUState("cpulast.brbkcpu"));
				}

				CPU.ExecutionState es = ProcUnit.Run(ref InstMem, ref DataMem); //Start CPU execution with the current RAM state and store the final execution state
				WriteQuinaryToFile("instmemlast.brbk5", InstMem.Memory);
				WriteQuinaryToFile("datamemlast.brbk5", DataMem.Memory);
				WriteCPUState("cpulast.brbkcpu", ProcUnit);
				Console.WriteLine($"Program completed with execution state {es}");
			}

			else if(new string[] {"-a", "--assemble"}.Contains(args[0]))    //Assemble mode
			{
				bool verbose = false;
				if (args.Length > 2) verbose = args[2] == "-v"; //Run in verbose mode
				Assembler.Assembler.Run(args[1], verbose);
			}
		}
		
		/// <summary>
		/// Reads a quianry state from a file.
		/// </summary>
		/// <param name="path">Path to read from.</param>
		/// <returns>A quinary state.</returns>
		private static Qyte[] ReadQuinaryFromFile(string path)
		{
			using StreamReader sr = new(path);			//Reader to file with path to .brbk5 file (file type is not a filter)
			string rawstate = sr.ReadToEnd();           //Read Quinary
			//If the number of Qits (represented as chars) isn't divisible by 3 or exceeds the number of qits that can be stored in RAM, throw a segmentation fault
			if (rawstate.Length % 3 != 0 || rawstate.Length / 3 > RAM.RamCeiling) throw new SegmentationFaultException($"Instruction RAM Flash State size is {rawstate.Length} Qits ({(float)rawstate.Length / 3} Qytes). Ram ceiling size is {RAM.RamCeiling} Qytes.");
			//Split the machine code into groups of 3 chars and generate qytes accordingly, push these to the state array
			return Enumerable.Range(0, rawstate.Length / 3)
				.Select(t => new Qyte(rawstate.Substring(t * 3, 3)))
				.ToArray();
		}

		/// <summary>
		/// Writes a quinary state to a file.
		/// </summary>
		/// <param name="path">Path to write to.</param>
		/// <param name="state">State to write.</param>
		private static void WriteQuinaryToFile(string path, Qyte[] state)
		{
			using StreamWriter sw = new(path);

			bool shrunk = false;
			int index = 0;
			while(!shrunk)
            {
				if (state[index].Equals(new Qyte())) shrunk = state[index..].All(t => t.Equals(new Qyte()));
				if (!shrunk) index++;
			}
			state = state[..index];

			foreach(Qyte s in state)
            {
				sw.Write(s.ToString());	//Write each qyte to the file in its string form.
            }
		}

		/// <summary>
		/// Returns the CPU state stored in a given file.
		/// </summary>
		/// <param name="path">File to read from.</param>
		/// <returns>The instruction memory address and the state of the registers.</returns>
		private static (int, CPU.Register[]) ReadCPUState(string path)
		{
			using StreamReader sr = new(path);
			int instmemaddr = int.Parse(sr.ReadLine());	//first line is the instruction memory address
			CPU.Register[] registerstates = new CPU.Register[10];
			for(int x = 0; x < 10; x++)					//following 10 lines are the register states
            {
				string rawstate = sr.ReadLine().Trim();
				if (rawstate.Length % 3 != 0) throw new SegmentationFaultException($"Provided register flash state for R{x} cannot be marshalled.");
				registerstates[x] = new CPU.Register(new Qyte());
            }

			return (instmemaddr, registerstates);
		}
		
		/// <summary>
		/// Writes the CPU state to a given file.
		/// </summary>
		/// <param name="path">File to write to.</param>
		/// <param name="cpu">CPU from which the state will be copied.</param>
		private static void WriteCPUState(string path, CPU cpu)
        {
			using StreamWriter sw = new(path);
			sw.WriteLine(cpu.GetInstMemAddr());
			sw.WriteLine(CPU.R0.ToString());
			sw.WriteLine(CPU.R1.ToString());
			sw.WriteLine(CPU.R2.ToString());
			sw.WriteLine(CPU.R3.ToString());
			sw.WriteLine(CPU.R4.ToString());
			sw.WriteLine(CPU.R5.ToString());
			sw.WriteLine(CPU.R6.ToString());
			sw.WriteLine(CPU.R7.ToString());
			sw.WriteLine(CPU.R8.ToString());
			sw.WriteLine(CPU.R9.ToString());
		}
	}
}
