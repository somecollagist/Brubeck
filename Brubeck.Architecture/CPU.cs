﻿using System;
using System.Collections.Generic;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    /// <summary>
    /// Base CPU Class
    /// </summary>
    public partial class CPU
    {
        /// <summary>
        /// Current Memory Address of RAM being read.
        /// </summary>
        private int InstMemAddr;

        /// <summary>
        /// Getter for Current Memory Address.
        /// </summary>
        public int GetInstMemAddr() => InstMemAddr;
        /// <summary>
        /// Setter for Current Memory Address.
        /// </summary>
        public void SetInstMemAddr(int value) => InstMemAddr = value is < 0 or >= RAM.RamCeiling
            ? throw new IllegalIndexException($"Memory Address cannot be set to value {value} because it is either less than 0 or greater than the established RAM Ceiling.")
            : value;

        /// <summary>
        /// Increments Current Memory Address. Exceeding the RAM Ceiling sets the address to 0.
        /// </summary>
        public void IncInstMemAddr() => InstMemAddr = ++InstMemAddr == RAM.RamCeiling ? 0 : InstMemAddr;
        /// <summary>
        /// Decrements Current Memory Address. Going below 0 sets the address to the RAM Ceiling.
        /// </summary>
        public void DecInstMemAddr() => InstMemAddr = InstMemAddr-- == 0 ? RAM.RamCeiling - 1 : InstMemAddr;

        /// <summary>
        /// Returns the value of the next Qyte in Memory.
        /// </summary>
        /// <param name="Memory">Reference to the current memory being used.</param>
        public Qyte GetNextQyte(ref RAM Memory)
        {
            Qyte ret = Memory.QyteAtIndex(InstMemAddr);
            IncInstMemAddr();
            return ret;
        }

        /// <summary>
        /// Returns the values of the next number of Qytes in memory;
        /// </summary>
        /// <param name="count">Number of qytes to read.</param>
        /// <param name="Memory">Reference to the current memory being used.</param>
        public Qyte[] GetNextQytes(int count, ref RAM Memory)
        {
            Qyte[] qytes = new Qyte[count];
            for (int x = 0; x < count; x++) qytes[x] = GetNextQyte(ref Memory);
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
        public void FlashCPUState((int, Register[]) state)
        {
            InstMemAddr = state.Item1;
            if (state.Item2.Length != 10) throw new ComponentNonExistentException($"{state.Item2.Length} register states provided, should only be 10.");
            R0 = state.Item2[0];
            R1 = state.Item2[1];
            R2 = state.Item2[2];
            R3 = state.Item2[3];
            R4 = state.Item2[4];
            R5 = state.Item2[5];
            R6 = state.Item2[6];
            R7 = state.Item2[7];
            R8 = state.Item2[8];
            R9 = state.Item2[9];
        }
    }
}
