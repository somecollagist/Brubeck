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
            return;
        }
    }
}
