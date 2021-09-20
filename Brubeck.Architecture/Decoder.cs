using System;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        //public void GetOperands(int num, ref RAM Memory, ref Qyte[] Operands)
        //{
        //    Operands = new Qyte[num];
        //    for(int x = 0; x < num; x++) Operands[x] = GetNextQyte(ref Memory);
        //}

        //public int Cycle(ref RAM Memory)
        //{
        //    Qyte Opcode, Options;
        //    Qyte[] Operands = Array.Empty<Qyte>();

        //    Opcode = GetNextQyte(ref Memory);
        //    Options = GetNextQyte(ref Memory);

        //    switch(Opcode.ToString())
        //    {
        //        case "AAA": //Add
        //            switch(Options.Qits[0])
        //            {
        //                case Qit.A: //Register
        //                    GetOperands(2, ref Memory, ref Operands);
        //                        /* Operand 1 : Target Register
        //                         * Operand 2 : Source Register */
        //                    break;

        //                case Qit.E: //Memory Location (Options qit 2 is part of memory location)
        //                    GetOperands(4, ref Memory, ref Operands);
        //                        /* Operand 1 : Target Register
        //                         * Operands 2-4 : Qit stream of memory address location (Option[2] is first qit of stream) */
        //                    break;

        //                case Qit.I: //Numeric Constant
        //                    GetOperands(2, ref Memory, ref Operands);
        //                    /* Operand 1 : Target Register
        //                     * Operand 2 : Constant */
        //                    break;
        //            }
        //            break;
        //    }

        //    return 0;
        //}
    }
}
