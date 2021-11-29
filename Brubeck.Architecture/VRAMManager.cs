using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck.Core;
using Brubeck.Peripheral;

namespace Brubeck.Architecture
{
	public partial class CPU
	{
		private int ScreenHeight = 0;
		private int ScreenWidth = 0;

		private int Rows = 0;
		private int Columns = 0;

		private int TotalChars = 0;
		private int CharSize = 0;

		private int VRAMStartIndex = 0;

		public void AllocVRAM(int screenheight, int screenwidth)
		{
			ScreenHeight = screenheight;
			ScreenWidth = screenwidth;

			Rows = ScreenHeight / QChar.Height;
			Columns = ScreenWidth / QChar.Width;

			TotalChars = Rows * Columns;
			CharSize = (QChar.Width * QChar.Height) / 3;

			VRAMStartIndex = RAM.RamCeiling - (TotalChars * CharSize);
		}

		public int VRAMCharIndex { get; private set; } = 0;

		private void IncVRAMCharIndex() => VRAMCharIndex = ++VRAMCharIndex == TotalChars ? 0 : VRAMCharIndex;
		private void DecVRAMCharIndex() => VRAMCharIndex = VRAMCharIndex-- == 0 ? TotalChars - 1 : VRAMCharIndex;

		public void WriteCharToVRAM(Qit[] map, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			WriteCharToVRAM(map, VRAMCharIndex, ref DataMem, ref VideoFeed);
			IncVRAMCharIndex();
		}

		public void WriteCharToVRAM(Qit[] map, int index, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			int charline;
			try { charline = index / Columns; } catch { charline = 0; }
			int charcolumn = index - (charline * Columns);

			Qyte[] data = QChar.ConvertCharQitArrayToQyteArray(map);
			int idx = 0;
			for(int y = 0; y < QChar.Height * QChar.Width * Columns / 3; y += Columns * (QChar.Width / 3))
			{
				for(int x = 0; x < QChar.Width / 3; x++)
				{
					DataMem.QyteAtIndex(VRAMStartIndex + (charline * ScreenWidth * (QChar.Height / 3)) + (charcolumn * QChar.Width / 3) + x + y + (charline * ScreenWidth)) = data[idx];
					idx++;
				}
			}

			VideoFeed = DataMem.Memory[VRAMStartIndex..];
		}

		public void RemoveCharFromVRAM(ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			DecVRAMCharIndex();
			RemoveCharFromVRAM(VRAMCharIndex, ref DataMem, ref VideoFeed);
		}

		public void RemoveCharFromVRAM(int index, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			WriteCharToVRAM(QChar.SPC, index, ref DataMem, ref VideoFeed);
		}

		public Qyte[] GetVRAM(ref RAM DataMem) => DataMem.Memory[VRAMStartIndex..];
	}
}
