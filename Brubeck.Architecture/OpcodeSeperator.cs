using System;
using System.Linq;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        private class CommandDefinition
        {
            public Qyte Opcode { get; private set; }
            public Qyte[] Operands { get; private set; }

            public Qit[] GetOperandsAsQitStream()
            {
                return string.Join(
                    string.Empty, 
                    Operands.Select(t => t.ToString()))
                    .Select(t => QitConverter.GetQitFromChar(t))
                    .ToArray();
            }
        }
    }
}
