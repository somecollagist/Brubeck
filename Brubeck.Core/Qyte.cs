using System;
using System.Linq;

namespace Brubeck.Core
{
    public class Qyte
    {
        public Qit[] Qits { get; set; } = new Qit[3];

        public Qyte()
        {
            Array.Fill(Qits, new());
        }

        public Qyte(Qit[] qits)
        {
            Array.Copy(qits, Qits, 3);
        }

        public Qyte(string gen)
        {
            Qits = gen.ToCharArray().Select(t => QitConverter.GetQitFromChar(t)).ToArray();
        }

        public override string ToString()
        {
            return new string(Qits.Select(t => QitConverter.GetCharFromQit(t)).ToArray());
        }

        public ref Qit QitAtIndex(int index)
        {
            if (index < 0 || index > 2) throw new IllegalIndexException($"Cannot access Qit at index {index}");
            return ref Qits[index];
        }
    }
}
