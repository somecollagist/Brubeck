using System;
using System.Linq;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        public ExecutionState Run(ref RAM InstMem, ref RAM DataMem)
        {
            WriteCharToVRAM(Char.A, ref DataMem);
            ExecutionState es;
            while ((es = Cycle(ref InstMem, ref DataMem)) == ExecutionState.OK) ;
            return es;
        }

        private ExecutionState Cycle(ref RAM InstMem, ref RAM DataMem)
        {
            Qyte opcode = GetNextQyte(ref InstMem);
            (Qyte, Qyte) ops;

            //Raw opcodes
            if (opcode.QitAtIndex(0) == Qit.U)
            {
                switch(new string(opcode.Qits[1..3].Select(t => QitConverter.GetCharFromQit(t)).ToArray()))
                {
                    case "II": //HALT
                        return ExecutionState.HLT;

                    default:
                        return ExecutionState.ERR;
                }
            }
            else
            {
                switch (new string(opcode.Qits[1..3].Select(t => QitConverter.GetCharFromQit(t)).ToArray()))
                {
                    case "AA": //ADD
                        ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
                        Register.GetRegisterFromQyte(ops.Item1).Add(ops.Item2);
                        break;

                    case "AE": //SUB
                        ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
                        Register.GetRegisterFromQyte(ops.Item1).Sub(ops.Item2);
                        break;

                    case "AI": //MUL
                        ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
                        Register.GetRegisterFromQyte(ops.Item1).Mul(ops.Item2);
                        break;

                    case "AO": //DIV
                        ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
                        Register.GetRegisterFromQyte(ops.Item1).Div(ops.Item2);
                        break;

                    case "AU": //MOD
                        ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
                        Register.GetRegisterFromQyte(ops.Item1).Mod(ops.Item2);
                        break;

                    case "IA": //MOV
                        ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
                        Register.GetRegisterFromQyte(ops.Item1).Qits = ops.Item2.Qits;
                        break;

                    case "II":
                        if (opcode.QitAtIndex(0) == Qit.I) return ExecutionState.OK;    //Allow continuation if qyte is null
                        else return ExecutionState.ERR;                                 //Otherwise it's a segmentation fault

                    default:
                        return ExecutionState.ERR;
                }
            }
            return ExecutionState.OK;
        }

        /// <summary>
        /// Obtains the two operands required for most operations.
        /// </summary>
        /// <param name="OptionFlag">First Qit of the opcode, which specifies if a register, memory location, or constant is used.</param>
        /// <param name="Memory">Reference to current memory.</param>
        /// <returns>Qyte alias of target register, and the value of the second operand.</returns>
        private (Qyte, Qyte) GetOperands(Qit OptionFlag, ref RAM InstMem, ref RAM DataMem)
        {
            Qyte[] operands;
            switch(OptionFlag)
            {
                case Qit.A: //Register
                    operands = GetNextQytes(2, ref InstMem);
                    return (operands[0], Register.GetRegisterFromQyte(operands[1]));

                case Qit.E: //Mem Loc
                    operands = GetNextQytes(4, ref InstMem);
                    return (operands[0],
                            DataMem.QyteAtIndex(
                                    QitConverter.GetIntFromQitArray(
                                        new Qit[] { operands[0].QitAtIndex(2) }
                                        .Concat(operands[1].Qits)
                                        .Concat(operands[2].Qits)
                                        .Concat(operands[3].Qits)
                                        .ToArray())
                                    ));

                case Qit.O: //Const
                    operands = GetNextQytes(2, ref InstMem);
                    return (operands[0], operands[1]);

                default:
                    return (null, null);
            }
        }
    }
}
