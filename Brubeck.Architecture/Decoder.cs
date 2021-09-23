using System;

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
            Qyte[] operands = GetOperands(opcode, ref Memory);
            return ExecutionState.OK;
        }

        private Qyte[] GetOperands(Qyte opcode, ref RAM Memory)
        {
            return null;
        }


    }
}
