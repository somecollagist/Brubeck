using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Brubeck.Core;

namespace Brubeck.Peripheral
{
    public partial class Monitor : Window
    {
        public Qyte interrupt = new();

        public void KeyPress(object sender, KeyEventArgs e)
        {
            
        }

        private Dictionary<Key, Qyte> InterruptPairs = new()
        {
            { Key.A, new("IIO") },
            { Key.B, new("IIU") },
            { Key.C, new("IOA") },
            { Key.D, new("IOE") },
            { Key.E, new("IOI") },
            { Key.F, new("IOO") },
            { Key.G, new("IOU") },
            { Key.H, new("IUA") },
            { Key.I, new("IUE") },
            { Key.J, new("IUI") },
            { Key.K, new("IUO") },
            { Key.L, new("IUU") },
            { Key.M, new("OAA") },
            { Key.N, new("OAE") },
            { Key.O, new("OAI") },
            { Key.P, new("OAO") },
            { Key.Q, new("OAU") },
            { Key.R, new("OEA") },
            { Key.S, new("OEE") },
            { Key.T, new("OEI") },
            { Key.U, new("OEO") },
            { Key.V, new("OEU") },
            { Key.W, new("OIA") },
            { Key.X, new("OIE") },
            { Key.Y, new("OII") },
            { Key.Z, new("OIO") },

            { Key.D0, new("OOO") },
            { Key.D1, new("OOU") },
            { Key.D2, new("OUA") },
            { Key.D3, new("OUE") },
            { Key.D4, new("OUI") },
            { Key.D5, new("OUO") },
            { Key.D6, new("OUU") },
            { Key.D7, new("UAA") },
            { Key.D8, new("UAE") },
            { Key.D9, new("UAI") },
        };
    }
}
