using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Threading;

using Brubeck.Architecture;
using Brubeck.Assembler;
using Brubeck.Core;
using Brubeck.Peripheral;

namespace Brubeck
{
	public partial class Emulator
	{
		public static async void RunEmulator(string[] args)
		{
			try
            {
				RAM InstMem = new();    //Create a memory instance for instructions
				RAM DataMem = new();    //Create a memory instance for data
				CPU ProcUnit = new();   //Create a cpu instance

				if (args.Length > 1)
				{
					Console.WriteLine($"Reading instruction memory from {args[1]}...");
					await InstMem.FlashRAMState(ReadQuinaryFromFile(args[1]));          //Flash instructions

					if (args.Length > 2)
					{
						Console.WriteLine($"Reading data memory from {args[2]}...");
						await DataMem.FlashRAMState(ReadQuinaryFromFile(args[2]));      //Flash data

						if (args.Length > 3)
						{
							Console.WriteLine($"Reading CPU state from {args[3]}...");
							await ProcUnit.FlashCPUState(ReadCPUState(args[3]));        //Flash CPU state
						}
					}
				}
				else
				{
					//If no file arguments are passed, restore the previous execution state

					Console.WriteLine("Reading last instruction memory state...");
					await InstMem.FlashRAMState(ReadQuinaryFromFile("instmemlast.brbk5"));
					Console.WriteLine("Reading last data memory state...");
					await DataMem.FlashRAMState(ReadQuinaryFromFile("datamemlast.brbk5"));
					Console.WriteLine("Reading last CPU state...");
					await ProcUnit.FlashCPUState(ReadCPUState("cpulast.brbkcpu"));
				}

				Thread MonitorThread = new(new ThreadStart(App.Start)); //Run the monitor on a separate thread so the CPU isn't blocked
				MonitorThread.SetApartmentState(ApartmentState.STA);    //Monitor must be created on STAThread because reasons
				MonitorThread.Start();

				ProcUnit.AllocVRAM(Peripheral.Monitor.ResHeight, Peripheral.Monitor.ResWidth);  //Allocate VRAM based off peripheral's resolution
				ProcUnit.AllocStack(ProcUnit.VRAMStartAddr - 1, 189000, ref DataMem);                       // ~2% of total memory.

				CPU.ExecutionState es = ProcUnit.Run(ref InstMem, ref DataMem, ref App.MonitorInstance.CachedVideoFeed); //Start CPU execution with the current RAM state and store the final execution state
				Console.WriteLine($"Program completed with execution state {es}");

				Console.WriteLine("Writing state logs...");
				WriteQuinaryToFile("instmemlast.brbk5", InstMem.Memory);
				Console.WriteLine("Instruction memory written");
				WriteQuinaryToFile("datamemlast.brbk5", DataMem.Memory);
				Console.WriteLine("Data memory written");
				WriteCPUState("cpulast.brbkcpu", ProcUnit);
				Console.WriteLine("CPU state written");
				Console.WriteLine("All state logs written");
			}
			catch(Exception ex)
			{
                Console.WriteLine($"\tUnhandled Exception of type: {ex.GetType()}");
                Console.WriteLine($"\tMessage: {ex.Message}");
                Console.WriteLine($"\tStack Trace:\n\t\t{string.Join("\n\t\t", ex.StackTrace.Split('\n').Select(t => t.Trim()))}");
			}
		}
	}
}
