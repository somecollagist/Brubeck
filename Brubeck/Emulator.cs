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
			RAM InstMem = new();    //Create a memory instance for instructions
			RAM DataMem = new();    //Create a memory instance for data
			CPU ProcUnit = new();   //Create a cpu instance

			if (args.Length > 1)
			{
				InstMem.FlashRAMState(ReadQuinaryFromFile(args[1]));        //Flash instructions

				if (args.Length > 2)
				{
					DataMem.FlashRAMState(ReadQuinaryFromFile(args[2]));    //Flash data

					if (args.Length > 3)
					{
						ProcUnit.FlashCPUState(ReadCPUState(args[3]));      //Flash CPU state
					}
				}
			}
			else
			{
				InstMem.FlashRAMState(ReadQuinaryFromFile("instmemlast.brbk5"));
				DataMem.FlashRAMState(ReadQuinaryFromFile("datamemlast.brbk5"));
				ProcUnit.FlashCPUState(ReadCPUState("cpulast.brbkcpu"));
			}

            Thread MonitorThread = new(new ThreadStart(App.Start));
            MonitorThread.SetApartmentState(ApartmentState.STA);
            MonitorThread.Start();
            //await App.Start();

            ProcUnit.AllocVRAM(184, 240);

			//Timer MonitorRefresher = new(
			//	e =>
			//	{
			//		//ProcUnit.GetVRAM();
			//	}, null, TimeSpan.Zero, TimeSpan.FromSeconds(0.2));
			CPU.ExecutionState es = ProcUnit.Run(ref InstMem, ref DataMem); //Start CPU execution with the current RAM state and store the final execution state
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
	}
}
