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

            Rows = ScreenHeight / Char.Height;
            Columns = ScreenWidth / Char.Width;

            TotalChars = Rows * Columns;
            CharSize = (Char.Width * Char.Height) / 3;

            VRAMStartIndex = RAM.RamCeiling - 1 - (TotalChars * CharSize);
        }
        private int VRAMCharIndex = 0;

        private void IncVRAMCharIndex() => VRAMCharIndex = ++VRAMCharIndex == TotalChars ? 0 : VRAMCharIndex;
        private void DecVRAMCharIndex() => VRAMCharIndex = VRAMCharIndex-- == 0 ? TotalChars - 1 : VRAMCharIndex;

        public void WriteCharToVRAM(Qit[] map, ref RAM DataMem)
        {
            WriteCharToVRAM(map, VRAMCharIndex, ref DataMem);
        }

        public void WriteCharToVRAM(Qit[] map, int index, ref RAM DataMem)
        {
            int charline;
            try { charline = index / Columns; } catch { charline = 0; }
            int charcolumn = index - (charline * Columns);

            Qyte[] data = Char.ConvertCharQitArrayToQyteArray(map);
            int idx = 0;
            for(int y = 0; y < Char.Height * Columns; y += Columns*(Char.Width/3))
            {
                for(int x = 0; x < Char.Width / 3; x++)
                {
                    DataMem.QyteAtIndex(VRAMStartIndex + (charline * ScreenWidth) + (charcolumn * Char.Width) + x + y) = data[idx];
                    idx++;
                }
            }
            IncVRAMCharIndex();

            App.MonitorInstance.Display(DataMem.Memory[VRAMStartIndex..]);
        }

        public Qyte[] GetVRAM(ref RAM DataMem) => DataMem.Memory[VRAMStartIndex..];
    }
}
