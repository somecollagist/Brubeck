using System;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public class RAM
    {
        public const int RamCeiling = 9765625;

        private Qyte[] Memory = new Qyte[RamCeiling];

        public RAM()
        {
            Array.Fill(Memory, new());
        }

        public ref Qyte QyteAtIndex(int index)
        {
            if (index < 0 || index > RamCeiling) throw new IllegalIndexException($"No such RAM index '{index}' exists");
            return ref Memory[index];
        }
    }
}
