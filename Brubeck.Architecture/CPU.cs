using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    /// <summary>
    /// Base CPU Class
    /// </summary>
    public partial class CPU
    {
        public AddressRegister InstMemAddr { get; private set; } = new();
        public AddressRegister DataMemAddr { get; private set; } = new();

        /// <summary>
        /// Returns the value of the next Qyte in Memory.
        /// </summary>
        /// <param name="InstMem">Reference to Instruction Memory.</param>
        public Qyte GetNextQyte(ref RAM InstMem)
        {
            Qyte ret = InstMem.QyteAtIndex(InstMemAddr.GetAddr());
            InstMemAddr.IncAddr();
            return ret;
        }

        /// <summary>
        /// Returns the values of the next number of Qytes in memory;
        /// </summary>
        /// <param name="count">Number of qytes to read.</param>
        /// <param name="InstMem">Reference to Instruction Memory.</param>
        public Qyte[] GetNextQytes(int count, ref RAM InstMem)
        {
            Qyte[] qytes = new Qyte[count];
            for (int x = 0; x < count; x++) qytes[x] = GetNextQyte(ref InstMem);
            return qytes;
        }

#pragma warning disable CA2211
        /// <summary>
        /// A general purpose register.
        /// </summary>
        public static Register R0, R1, R2, R3, R4, R5, R6, R7, R8, R9;
#pragma warning restore CA2211

        public enum ExecutionState
        {
            /// <summary>
            /// No CPU fault, execution was successful.
            /// </summary>
            OK,
            /// <summary>
            /// CPU encountered an error, execution was unsuccessful.
            /// </summary>
            ERR,
            /// <summary>
            /// CPU was instructed to stop.
            /// </summary>
            HLT
        }

        /// <summary>
        /// Creates an instance of a CPU.
        /// </summary>
        public CPU()
        {
            R0 = new();
            R1 = new();
            R2 = new();
            R3 = new();
            R4 = new();
            R5 = new();
            R6 = new();
            R7 = new();
            R8 = new();
            R9 = new();
        }

        /// <summary>
        /// Sets the CPU's state to match the provided parameters.
        /// </summary>
        public Task FlashCPUState((int, int, Register[], int) state)
        {
            InstMemAddr.SetAddr(state.Item1);
            DataMemAddr.SetAddr(state.Item2);
            if (state.Item3.Length != 10) throw new ComponentNonExistentException($"{state.Item3.Length} register states provided, should only be 10.");
            R0 = state.Item3[0];
            R1 = state.Item3[1];
            R2 = state.Item3[2];
            R3 = state.Item3[3];
            R4 = state.Item3[4];
            R5 = state.Item3[5];
            R6 = state.Item3[6];
            R7 = state.Item3[7];
            R8 = state.Item3[8];
            R9 = state.Item3[9];
            VRAMCharIndex = state.Item4;
            return Task.CompletedTask;
        }
    }
}
