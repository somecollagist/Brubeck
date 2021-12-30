using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Brubeck.Assembler.Assembler;

namespace Brubeck.Assembler
{
    internal static class InterpreterCommands
    {
        public static Mnemonic[] RunCommand(Assembler assembler, string cmd)
        {
            cmd = cmd[1..];

            string[] tokens = cmd.Trim().Split(' ');

            switch(tokens[0])
            {
                case "INCLUDE":
                    Assembler subassembler = new Assembler();
                    return subassembler.Run(tokens[1], assembler.AsmVerbose, true);

                default:
                    throw new DirectiveFault();
            }
        }
    }
}
