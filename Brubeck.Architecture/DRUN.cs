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
		public void DRUN(ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			AddressRegister im = InstMemAddr;
			AddressRegister dm = DataMemAddr;

			InstMemAddr = dm;
			Cycle(ref DataMem, ref DataMem, ref VideoFeed);
			DataMemAddr = InstMemAddr;
			InstMemAddr = im;
		}
	}
}
