using System;
using System.Linq;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        public void Run(ref RAM Memory)
        {
            while (Cycle(ref Memory) == ExecutionState.OK) ;
        }

        private ExecutionState Cycle(ref RAM Memory)
        {
            Qyte opcode = GetNextQyte(ref Memory);
            Qyte options = GetNextQyte(ref Memory);
            Qyte[] operands;
            switch(opcode.ToString())
            {
                case "AAA": //ADD
                    switch(options.QitAtIndex(0))
                    {
                        case Qit.A: //Register
                            operands = GetNextQytes(2, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Add(Register.GetRegisterFromQyte(operands[1]));
                            break;

                        case Qit.E: //Mem Loc
                            operands = GetNextQytes(4, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Add(Memory.QyteAtIndex(
                                QitConverter.GetIntFromQitArray(
                                    new Qit[] { operands[0].QitAtIndex(0) }
                                    .Concat(operands[1].Qits)
                                    .Concat(operands[2].Qits)
                                    .Concat(operands[3].Qits)
                                    .ToArray())
                                ));
                            break;

                        case Qit.I: //Const
                            operands = GetNextQytes(2, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Add(operands[1]);
                            break;

                        default:
                            return ExecutionState.ERR;
                    }
                    break;

                case "AAE": //SUB
                    switch(options.QitAtIndex(0))
                    {
                        case Qit.A: //Register
                            operands = GetNextQytes(2, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Sub(Register.GetRegisterFromQyte(operands[1]));
                            break;

                        case Qit.E: //Mem Loc
                            operands = GetNextQytes(4, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Sub(Memory.QyteAtIndex(
                                QitConverter.GetIntFromQitArray(
                                    new Qit[] { operands[0].QitAtIndex(0) }
                                    .Concat(operands[1].Qits)
                                    .Concat(operands[2].Qits)
                                    .Concat(operands[3].Qits)
                                    .ToArray())
                                ));
                            break;

                        case Qit.I: //Const
                            operands = GetNextQytes(2, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Add(operands[1]);
                            break;

                        default:
                            return ExecutionState.ERR;
                    }
                    break;

                case "AEA": //MOV
                    switch(options.QitAtIndex(0))
                    {
                        case Qit.A: //Register
                            operands = GetNextQytes(2, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Qits = Register.GetRegisterFromQyte(operands[1]).Qits;
                            break;

                        case Qit.E: //Mem Loc
                            operands = GetNextQytes(4, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Qits = Memory.QyteAtIndex(
                                QitConverter.GetIntFromQitArray(
                                    new Qit[] { operands[0].QitAtIndex(0) }
                                    .Concat(operands[1].Qits)
                                    .Concat(operands[2].Qits)
                                    .Concat(operands[3].Qits)
                                    .ToArray())
                                ).Qits;
                            break;

                        case Qit.I: //Const
                            operands = GetNextQytes(2, ref Memory);
                            Register.GetRegisterFromQyte(operands[0]).Qits = operands[1].Qits;
                            break;

                        default:
                            return ExecutionState.ERR;
                    }
                    break;

                default:
                    return ExecutionState.ERR;
            }
            return ExecutionState.OK;
        }
    }
}
