using System;
using System.IO;
using System.Linq;

using Brubeck.Architecture;
using Brubeck.Core;

namespace Brubeck
{
    class Program
    {
        /// <summary>
        /// Entry point for the emulator, this method is independent of emulated hardware. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(params string[] args)
        {
            RAM Memory = new();
            CPU ProcUnit = new();

            if(args.Length > 0)
            {
                Qyte[] state = new Qyte[RAM.RamCeiling];
                using(StreamReader sr = new(args[0]))
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
    }
}
