using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brubeck.Assembler
{
	/// <summary>
	/// Thrown when assembly code cannot be parsed into legal machine code.
	/// </summary>
	public class AssemblySegmentationFault : Exception
	{
		public AssemblySegmentationFault() : base() { }
		public AssemblySegmentationFault(string message) : base(message) { }
		public AssemblySegmentationFault(string message, Exception inner) : base(message, inner) { }
	}

	public class DirectiveFault : Exception
    {
		public DirectiveFault() : base() { }
		public DirectiveFault(string message) : base(message) { }
		public DirectiveFault(string message, Exception inner) : base(message, inner) { }
    }
}
