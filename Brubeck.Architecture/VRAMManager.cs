using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        public int VRAMStartIndex
        {
            get => VRAMStartIndex;
            set
            {
                if (value >= RAM.RamCeiling) throw new SystemOutOfMemoryException("Insufficient VRAM can be allocated.");
            }
        }
    }
}
