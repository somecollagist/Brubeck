using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brubeck.Assembler
{
    public class AssemblySegmentationFault : Exception
    {
        public AssemblySegmentationFault() : base() { }
        public AssemblySegmentationFault(string message) : base(message) { }
        public AssemblySegmentationFault(string message, Exception inner) : base(message, inner) { }
    }
}
