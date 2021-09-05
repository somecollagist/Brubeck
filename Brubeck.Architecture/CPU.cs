using System;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        public int MemAddr
        {
            get => MemAddr;
            set => MemAddr = value is < 0 or >= RAM.RamCeiling
                    ? throw new IllegalIndexException($"Memory Address cannot be set to value {value} because it is either less than 0 or greater than the established RAM Ceiling.")
                    : value;
        }

        public void IncMemAddr() => MemAddr = ++MemAddr == RAM.RamCeiling ? 0 : MemAddr;
        public void DecMemAddr() => MemAddr = MemAddr-- == 0 ? RAM.RamCeiling - 1 : MemAddr;

        public Qyte GetNextQyte(ref RAM Memory)
        {
            IncMemAddr();
            return Memory.QyteAtIndex(MemAddr - 1);
        }

        public static Register R0, R1, R2, R3, R4, R5, R6, R7, R8, R9;
    }
}
