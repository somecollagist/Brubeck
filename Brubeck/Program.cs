using System;
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
                RAM Memory = new();
                CPU ProcUnit = new();

                if (args.Length > 0)
                {
                    Qyte[] state = new Qyte[RAM.RamCeiling];
                    using (StreamReader sr = new(args[1]))
                    {
                        string rawstate = sr.ReadToEnd();
                        //If the number of Qits (represented as chars) isn't divisible by 3 or exceeds the number of qits that can be stored in RAM, throw a segmentation fault
                        if (rawstate.Length % 3 != 0 || rawstate.Length > RAM.RamCeiling) throw new SegmentationFaultException($"RAM Flash State size is {rawstate.Length} Qits ({(float)rawstate.Length / 3} Qytes). Ram ceiling size is {RAM.RamCeiling} Qytes.");
                        state = Enumerable.Range(0, rawstate.Length / 3)
                            .Select(t => new Qyte(rawstate.Substring(t * 3, 3)))
                            .ToArray();
                    }
                    Memory.FlashRAMState(state);
                }

                ProcUnit.Run(ref Memory);
            }

            else if(args[0] == "-a")    //Assemble mode
            {
                bool verbose = false;
                if (args.Length > 2) verbose = args[2] == "-v"; //Run in verbose mode
                Brubeck.Assembler.Program.Assemble(args[1], verbose);
            }
        }
    }
}
